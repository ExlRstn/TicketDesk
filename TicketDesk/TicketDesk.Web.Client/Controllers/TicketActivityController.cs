﻿// TicketDesk - Attribution notice
// Contributor(s):
//
//      Stephen Redd (https://github.com/stephenredd)
//
// This file is distributed under the terms of the Microsoft Public 
// License (Ms-PL). See http://opensource.org/licenses/MS-PL
// for the complete terms of use. 
//
// For any distribution that contains code from this file, this notice of 
// attribution must remain intact, and a copy of the license must be 
// provided to the recipient.

using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketDesk.Domain;
using TicketDesk.Domain.Model;
using TicketDesk.IO;
using TicketDesk.Localization.Controllers;
using TicketDesk.Web.Client.Models;
using TicketDesk.Web.Identity;
using TicketDesk.Web.Identity.Model;
using TicketDesk.Web.Identity.Properties;
using System.Net;
using System.Net.Mail;

namespace TicketDesk.Web.Client.Controllers
{
    [RoutePrefix("ticket-activity")]
    [Route("{action}")]
    [TdAuthorize(Roles = "TdInternalUsers,TdHelpDeskUsers,TdAdministrators")]
    [ValidateInput(false)]
    public class TicketActivityController : BaseController
    {
        private TdDomainContext Context { get; set; }
        public TicketActivityController(TdDomainContext context)
        {
            Context = context;
        }

        [Route("load-activity")]
        public async Task<ActionResult> LoadActivity(TicketActivity activity, int ticketId, Guid? tempId)
        {
            var ticket = await Context.Tickets.FindAsync(ticketId);
            Context.TicketActions.IsTicketActivityValid(ticket, activity);
            ViewBag.CommentRequired = activity.IsCommentRequired();
            ViewBag.Activity = activity;
            ViewBag.TempId = tempId ?? Guid.NewGuid();
            ViewBag.IsEditorDefaultHtml = Context.TicketDeskSettings.ClientSettings.GetDefaultTextEditorType() == "summernote";
            if (activity == TicketActivity.EditTicketInfo)
            {
                await SetProjectInfoForModelAsync(ticket);
            }
            return PartialView("_ActivityForm", ticket);
        }

        private async Task SetProjectInfoForModelAsync(Ticket ticket)
        {
            var projectCount = await Context.Projects.CountAsync();
            var isMulti = (projectCount > 1);
            ViewBag.IsMultiProject = isMulti;
        }

        [Route("activity-buttons")]
        public ActionResult ActivityButtons(int ticketId)
        {
            //WARNING! This is also used as a child action and cannot be made async in MVC 5
            var ticket = Context.Tickets.Find(ticketId);
            var activities = Context.TicketActions.GetValidTicketActivities(ticket);
            return PartialView("_ActivityButtons", activities);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("add-comment")]
        public async Task<ActionResult> AddComment(int ticketId, [ModelBinder(typeof(SummernoteModelBinder))] string comment)
        {
            var activityFn = Context.TicketActions.AddComment(comment);
            return await PerformTicketAction(ticketId, activityFn, TicketActivity.AddComment, comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("assign")]
        public async Task<ActionResult> Assign(int ticketId, [ModelBinder(typeof(SummernoteModelBinder))] string comment, string assignedTo, string priority)
        {
            var activityFn = Context.TicketActions.Assign(comment, assignedTo, priority);
            return await PerformTicketAction(ticketId, activityFn, TicketActivity.Assign, "");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("cancel-more-info")]
        public async Task<ActionResult> CancelMoreInfo(int ticketId, [ModelBinder(typeof(SummernoteModelBinder))] string comment)
        {
            var activityFn = Context.TicketActions.CancelMoreInfo(comment);
            return await PerformTicketAction(ticketId, activityFn, TicketActivity.CancelMoreInfo, "");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("close")]
        public async Task<ActionResult> Close(int ticketId, [ModelBinder(typeof(SummernoteModelBinder))] string comment)
        {
            var activityFn = Context.TicketActions.Close(comment);
            return await PerformTicketAction(ticketId, activityFn, TicketActivity.Close, "");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit-ticket-info")]
        public async Task<ActionResult> EditTicketInfo(
            int ticketId,
            int projectId,
            [ModelBinder(typeof(SummernoteModelBinder))] string comment,
            string title,
            string details,
            string priority,
            string ticketType,
            string category,
            string owner,
            string tagList,
            bool affectsCustomer,
            bool onlineSupport)
        {
            details = details.StripHtmlWhenEmpty();
            var projectName = await Context.Projects.Where(p => p.ProjectId == projectId).Select(s => s.ProjectName).FirstOrDefaultAsync();
            var activityFn = Context.TicketActions.EditTicketInfo(comment, projectId, projectName, title, details, priority, ticketType, category, owner, tagList, Context.TicketDeskSettings, affectsCustomer, onlineSupport);
            return await PerformTicketAction(ticketId, activityFn, TicketActivity.EditTicketInfo, "");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("force-close")]
        public async Task<ActionResult> ForceClose(int ticketId, [ModelBinder(typeof(SummernoteModelBinder))] string comment)
        {
            var activityFn = Context.TicketActions.ForceClose(comment);
            return await PerformTicketAction(ticketId, activityFn, TicketActivity.ForceClose, "");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("give-up")]
        public async Task<ActionResult> GiveUp(int ticketId, [ModelBinder(typeof(SummernoteModelBinder))] string comment)
        {
            var activityFn = Context.TicketActions.GiveUp(comment);
            return await PerformTicketAction(ticketId, activityFn, TicketActivity.GiveUp, "");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("modify-attachments")]
        public async Task<ActionResult> ModifyAttachments(int ticketId, [ModelBinder(typeof(SummernoteModelBinder))] string comment, Guid tempId, string deleteFiles)
        {
            var demoMode = (ConfigurationManager.AppSettings["ticketdesk:DemoModeEnabled"] ?? "false").Equals("true", StringComparison.InvariantCultureIgnoreCase);
            if (demoMode)
            {
                return new EmptyResult();
            }
            //most of this action is performed directly against the storage provider, outside the business domain's control. 
            //  All the business domain has to do is record the activity log and comments
            Action<Ticket> activityFn = ticket =>
           {
               //TODO: it might make sense to move the string building part of this over to the TicketDeskFileStore too?
               var sb = new StringBuilder(comment);
               if (!string.IsNullOrEmpty(deleteFiles))
               {
                   sb.AppendLine();
                   sb.AppendLine("<dl><dt>");
                   sb.AppendLine(Strings_sq.RemovedFiles);
                   sb.AppendLine("</dt>");


                   var files = deleteFiles.Split(',');
                   foreach (var file in files)
                   {
                       TicketDeskFileStore.DeleteAttachment(file, ticketId.ToString(CultureInfo.InvariantCulture), false);
                       sb.AppendLine(string.Format("<dd>    {0}</dd>", file));
                   }
                   sb.AppendLine("</dl>");
               }
               var filesAdded = ticket.CommitPendingAttachments(tempId).ToArray();
               if (filesAdded.Any())
               {
                   sb.AppendLine();
                   sb.AppendLine("<dl><dt>");
                   sb.AppendLine(Strings_sq.NewFiles);
                   sb.AppendLine("</dt>");

                   foreach (var file in filesAdded)
                   {
                       sb.AppendLine(string.Format("<dd>    {0}</dd>", file));
                   }
                   sb.AppendLine("</dl>");
               }
               comment = sb.ToString();

               //perform the simple business domain functions
               var domainActivityFn = Context.TicketActions.ModifyAttachments(comment);
               domainActivityFn(ticket);
           };

            return await PerformTicketAction(ticketId, activityFn, TicketActivity.ModifyAttachments, "");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("pass")]
        public async Task<ActionResult> Pass(int ticketId, [ModelBinder(typeof(SummernoteModelBinder))] string comment, string assignedTo, string priority)
        {
            var activityFn = Context.TicketActions.Pass(comment, assignedTo, priority);
            return await PerformTicketAction(ticketId, activityFn, TicketActivity.Pass, "");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("reassign")]
        public async Task<ActionResult> ReAssign(int ticketId, [ModelBinder(typeof(SummernoteModelBinder))] string comment, string assignedTo, string priority)
        {
            var activityFn = Context.TicketActions.ReAssign(comment, assignedTo, priority);
            return await PerformTicketAction(ticketId, activityFn, TicketActivity.ReAssign, "");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("request-more-info")]
        public async Task<ActionResult> RequestMoreInfo(int ticketId, [ModelBinder(typeof(SummernoteModelBinder))] string comment)
        {
            var activityFn = Context.TicketActions.RequestMoreInfo(comment);
            return await PerformTicketAction(ticketId, activityFn, TicketActivity.RequestMoreInfo, "");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("reopen")]
        public async Task<ActionResult> ReOpen(int ticketId, [ModelBinder(typeof(SummernoteModelBinder))] string comment, bool assignToMe = false)
        {
            var activityFn = Context.TicketActions.ReOpen(comment, assignToMe);
            return await PerformTicketAction(ticketId, activityFn, TicketActivity.ReOpen, "");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("resolve")]
        public async Task<ActionResult> Resolve(int ticketId, [ModelBinder(typeof(SummernoteModelBinder))] string comment)
        {
            var activityFn = Context.TicketActions.Resolve(comment);
            return await PerformTicketAction(ticketId, activityFn, TicketActivity.ReOpen, comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("supply-more-info")]
        public async Task<ActionResult> SupplyMoreInfo(int ticketId, [ModelBinder(typeof(SummernoteModelBinder))] string comment, bool reactivate = false)
        {
            var activityFn = Context.TicketActions.SupplyMoreInfo(comment, reactivate);
            return await PerformTicketAction(ticketId, activityFn, TicketActivity.SupplyMoreInfo, "");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("take-over")]
        public async Task<ActionResult> TakeOver(
            int ticketId,
            [ModelBinder(typeof(SummernoteModelBinder))] string comment,
            string priority)
        {
            var activityFn = Context.TicketActions.TakeOver(comment, priority);
            return await PerformTicketAction(ticketId, activityFn, TicketActivity.TakeOver, "");
        }


        private async Task<ActionResult> PerformTicketAction(int ticketId, Action<Ticket> activityFn, TicketActivity activity, string comment)
        {
            var ticket = await Context.Tickets.FindAsync(ticketId);
            TryValidateModel(ticket);
            if (ModelState.IsValid)
            {
                try
                {
                    ticket.PerformAction(activityFn);

                    if (ticket.TicketStatus.ToString().ToLower() == "Resolved".ToLower())
                    {
                        //EL: add logic to send email to client when a ticket is resolved
                        //var ticket = Context.Tickets.Include(t => t.TicketTags).First(t => t.TicketId == ticketId);
                        //send email if assigned to email is not empty
                       PrepareEmailForResolved(ticket,comment);
                    }
                    if (activity.ToString().ToLower() == "addcomment".ToLower())
                    {
                        //EL: add logic to send email to client when a ticket is resolved
                        //var ticket = Context.Tickets.Include(t => t.TicketTags).First(t => t.TicketId == ticketId);
                        //send email if assigned to email is not empty
                        PrepareEmailForAddComment(ticket, comment);
                    }
                }
                catch (SecurityException ex)
                {
                    ModelState.AddModelError("Security", ex);
                }
                var result = await Context.SaveChangesAsync(); //save changes catches lastupdatedby and date automatically
                if (result > 0)
                {
                    return new EmptyResult();//standard success case
                }
            }
            //fail case, return the view and let the client/view sort out the errors
            ViewBag.CommentRequired = activity.IsCommentRequired();
            ViewBag.Activity = activity;
            ViewBag.IsEditorDefaultHtml = Context.TicketDeskSettings.ClientSettings.GetDefaultTextEditorType() == "summernote";
            return PartialView("_ActivityForm", ticket);
        }

        private void PrepareEmailForAddComment(Ticket ticket, string comment)
        {
            if (!String.IsNullOrWhiteSpace(comment))
            {
                var user = ticket.GetLastUpdatedByInfo();
                string body = "Përshëndetje,"
                       + "<br/> <br/>Një koment është shtuar në kërkesen:  \"<b>" + ticket.Title + "</b>\""
                       + "<br/>Përmbajtja e komentit: <br/>" + HtmlHelperExtensions.HtmlToPlainText(comment).Trim()
                       + "<br/><br/>Komenti u shtua nga: " + user.DisplayName + "(" + user.Email + ")";

                try
                {
                    EmailHelper sendEmail = new EmailHelper();
                   
                    sendEmail.SendEmailToArfa("Koment i ri në kërkesen: \"" + ticket.Title + "\"", body);
                }

                catch (Exception e)
                {
                    //
                }
            }

        }

        private void PrepareEmailForResolved(Ticket ticket, string comment)
        {
            
            if (!String.IsNullOrWhiteSpace(ticket.Project.Email))
            {
                //var root = Context.TicketDeskSettings.ClientSettings.GetDefaultSiteRootUrl();

                string body = "";
                //this.RenderViewToString(ControllerContext, "~/Views/Emails/Ticket.Html.cshtml", new TicketEmail()
                //{
                //    Ticket = ticket,
                //    SiteRootUrl = root,
                //    IsMultiProject = false
                //});
                var support = ticket.OnlineSupport ? "Online" : "Hardware Support";
                body = "I nderuar Klient."
                               + "<br/>Kerkesa e krijuar nga ju per " + "<b>" + ticket.Project.ProjectName + "</b>" + " eshte mbyllur."
                               + "<br/><br/>Subjekti: " + ticket.Title
                               + "<br/>Pershkrimi i Problemit: " + HtmlHelperExtensions.HtmlToPlainText(ticket.Details)
                               + "<br/><br/>Specialisti qe asistoi: " + ticket.GetAssignedToInfo().DisplayName
                               + "<br/>Lloji i Asistences: " + support.ToString()
                               + "<br/>Pershkrimi i Sherbimit te kryer: " + HtmlHelperExtensions.HtmlToPlainText(comment).Trim();

                try
                {
                    EmailHelper sendEmail = new EmailHelper();
                    sendEmail.SendEmail(ticket.Project.Email, "Kërkesa juaj: " + ticket.Title + " është zgjidhur.",
                        body);
                }

                catch(Exception e)
                {
                    //
                }
            }

        }
    }
}
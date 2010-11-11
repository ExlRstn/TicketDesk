// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace TicketDesk.Web.Client.Controllers {
    public partial class TicketEditorController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected TicketEditorController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ContentResult MarkdownPreview() {
            return new T4MVC_ContentResult(Area, Name, ActionNames.MarkdownPreview);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Display() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Display);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult RefreshHistory() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.RefreshHistory);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult RefreshStats() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.RefreshStats);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult RefreshDetails() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.RefreshDetails);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult RefreshAttachments() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.RefreshAttachments);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult AddComment() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.AddComment);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Resolve() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Resolve);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult TakeOver() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.TakeOver);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Assign() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Assign);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Close() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Close);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ForceClose() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ForceClose);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult GiveUp() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.GiveUp);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ReOpen() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ReOpen);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult RequestMoreInfo() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.RequestMoreInfo);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult SupplyMoreInfo() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.SupplyMoreInfo);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult CancelMoreInfo() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.CancelMoreInfo);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ModifyAttachments() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ModifyAttachments);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult EditTicketInfo() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.EditTicketInfo);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public TicketEditorController Actions { get { return MVC.TicketEditor; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "TicketEditor";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string MarkdownPreview = "MarkdownPreview";
            public readonly string Display = "Display";
            public readonly string RefreshHistory = "RefreshHistory";
            public readonly string RefreshStats = "RefreshStats";
            public readonly string RefreshDetails = "RefreshDetails";
            public readonly string RefreshAttachments = "RefreshAttachments";
            public readonly string AddComment = "AddComment";
            public readonly string Resolve = "Resolve";
            public readonly string TakeOver = "TakeOver";
            public readonly string Assign = "Assign";
            public readonly string Close = "Close";
            public readonly string ForceClose = "ForceClose";
            public readonly string GiveUp = "GiveUp";
            public readonly string ReOpen = "ReOpen";
            public readonly string RequestMoreInfo = "RequestMoreInfo";
            public readonly string SupplyMoreInfo = "SupplyMoreInfo";
            public readonly string CancelMoreInfo = "CancelMoreInfo";
            public readonly string ModifyAttachments = "ModifyAttachments";
            public readonly string EditTicketInfo = "EditTicketInfo";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string Display = "~/Views/TicketEditor/Display.aspx";
            static readonly _Controls s_Controls = new _Controls();
            public _Controls Controls { get { return s_Controls; } }
            public partial class _Controls{
                public readonly string ActivityButtons = "~/Views/TicketEditor/Controls/ActivityButtons.ascx";
                public readonly string ActivityHistory = "~/Views/TicketEditor/Controls/ActivityHistory.ascx";
                public readonly string AddComment = "~/Views/TicketEditor/Controls/AddComment.ascx";
                public readonly string Assign = "~/Views/TicketEditor/Controls/Assign.ascx";
                public readonly string Attachments = "~/Views/TicketEditor/Controls/Attachments.ascx";
                public readonly string CancelMoreInfo = "~/Views/TicketEditor/Controls/CancelMoreInfo.ascx";
                public readonly string Close = "~/Views/TicketEditor/Controls/Close.ascx";
                public readonly string Details = "~/Views/TicketEditor/Controls/Details.ascx";
                public readonly string EditTicketInfo = "~/Views/TicketEditor/Controls/EditTicketInfo.ascx";
                public readonly string ForceClose = "~/Views/TicketEditor/Controls/ForceClose.ascx";
                public readonly string GiveUp = "~/Views/TicketEditor/Controls/GiveUp.ascx";
                public readonly string ModifyAttachments = "~/Views/TicketEditor/Controls/ModifyAttachments.ascx";
                public readonly string ReOpen = "~/Views/TicketEditor/Controls/ReOpen.ascx";
                public readonly string RequestMoreInfo = "~/Views/TicketEditor/Controls/RequestMoreInfo.ascx";
                public readonly string Resolve = "~/Views/TicketEditor/Controls/Resolve.ascx";
                public readonly string SupplyMoreInfo = "~/Views/TicketEditor/Controls/SupplyMoreInfo.ascx";
                public readonly string TakeOver = "~/Views/TicketEditor/Controls/TakeOver.ascx";
                public readonly string TicketStats = "~/Views/TicketEditor/Controls/TicketStats.ascx";
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_TicketEditorController: TicketDesk.Web.Client.Controllers.TicketEditorController {
        public T4MVC_TicketEditorController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ContentResult MarkdownPreview(string data) {
            var callInfo = new T4MVC_ContentResult(Area, Name, ActionNames.MarkdownPreview);
            callInfo.RouteValueDictionary.Add("data", data);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Display(int id, string activity) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Display);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("activity", activity);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult RefreshHistory(int id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.RefreshHistory);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult RefreshStats(int id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.RefreshStats);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult RefreshDetails(int id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.RefreshDetails);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult RefreshAttachments(int id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.RefreshAttachments);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult AddComment(int id, string comment, bool? resolve) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.AddComment);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("comment", comment);
            callInfo.RouteValueDictionary.Add("resolve", resolve);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Resolve(int id, string comment) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Resolve);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("comment", comment);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult TakeOver(int id, string comment, string priority) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.TakeOver);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("comment", comment);
            callInfo.RouteValueDictionary.Add("priority", priority);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Assign(int id, string comment, string assignedTo, string priority) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Assign);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("comment", comment);
            callInfo.RouteValueDictionary.Add("assignedTo", assignedTo);
            callInfo.RouteValueDictionary.Add("priority", priority);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Close(int id, string comment) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Close);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("comment", comment);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ForceClose(int id, string comment) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ForceClose);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("comment", comment);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult GiveUp(int id, string comment) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.GiveUp);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("comment", comment);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ReOpen(int id, string comment, bool? assignToMe, bool? ownedByMe) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ReOpen);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("comment", comment);
            callInfo.RouteValueDictionary.Add("assignToMe", assignToMe);
            callInfo.RouteValueDictionary.Add("ownedByMe", ownedByMe);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult RequestMoreInfo(int id, string comment) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.RequestMoreInfo);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("comment", comment);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult SupplyMoreInfo(int id, string comment, bool? markActive) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.SupplyMoreInfo);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("comment", comment);
            callInfo.RouteValueDictionary.Add("markActive", markActive);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult CancelMoreInfo(int id, string comment) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.CancelMoreInfo);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("comment", comment);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ModifyAttachments(int id, string comment, System.Collections.Generic.List<TicketDesk.Domain.Models.TicketAttachment> attachments) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ModifyAttachments);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("comment", comment);
            callInfo.RouteValueDictionary.Add("attachments", attachments);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult EditTicketInfo(int id, string comment) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.EditTicketInfo);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("comment", comment);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
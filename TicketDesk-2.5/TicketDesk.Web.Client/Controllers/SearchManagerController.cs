﻿// TicketDesk - Attribution notice
// Contributor(s):
//
//      Stephen Redd (stephen@reddnet.net, http://www.reddnet.net)
//
// This file is distributed under the terms of the Microsoft Public 
// License (Ms-PL). See http://opensource.org/licenses/MS-PL
// for the complete terms of use. 
//
// For any distribution that contains code from this file, this notice of 
// attribution must remain intact, and a copy of the license must be 
// provided to the recipient.

using System.Threading.Tasks;
using System.Web.Mvc;
using TicketDesk.Domain;
using TicketDesk.Domain.Model;
using TicketDesk.Domain.Search;

namespace TicketDesk.Web.Client.Controllers
{
    [RoutePrefix("admin/search-manager")]
    [Route("{action=index}")]
    [Authorize(Roles = "TdAdministrators")]
    public class SearchManagerController : Controller
    {
        
        private TicketDeskContext Context { get; set; }
        public SearchManagerController(TicketDeskContext context)
        {
          
            Context = context;
        }

        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("remove-index")]
        public async Task<ActionResult> RemoveIndex()
        {
            await TicketDeskSearchManager.Current.RemoveIndexAsync();
            ViewBag.IndexRemoved = true;
            return View("Index");
        }
       
        [Route("populate-index")]
        public async Task<ActionResult> PopulateIndex()
        {
            await TicketDeskSearchManager.Current.InitializeSearchAsync();
            var queueItems = Context.Tickets.Include("TicketEvents").ToSeachQueueItems();
            await TicketDeskSearchManager.Current.QueueItemsForIndexingAsync(queueItems);
            ViewBag.IndexPopulated = true;
            return View("Index");
        }
    }
}
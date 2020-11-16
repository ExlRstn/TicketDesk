using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketDesk.Domain.Model;

namespace TicketDesk.Web.Client.Models
{
    public class ReportTicketModel
    {
        public Ticket Ticket { get; set; }

        public ICollection<TicketEvent> TicketEvents { get; set; }


    }
}
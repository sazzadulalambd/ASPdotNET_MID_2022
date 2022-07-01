using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BePartner_App_Mid.Models
{
    public class InvestorMessage
    {
        public int MsgId { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string Ttime { get; set; }

        public string SenderName { get; set; }
        public string SenderOccupation { get; set; }
        public string SenderCompany { get; set; }
        public string SenderImg { get; set; }
        public string SenderPhone { get; set; }


        public string ReceiverName { get; set; }
        public string ReceiverOccupation { get; set; }
        public string ReceiverCompany { get; set; }
        public string ReceiverImg { get; set; }
        public string ReceiverPhone { get; set; }
    }
}
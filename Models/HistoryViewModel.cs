using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsolidationSystem.Models
{
    public class HistoryViewModel
    {
        public Guid Id { get; set; }
        public string BatchNo { get; set; }
        public string InvFileName { get; set; }
        public string CRMFileName { get; set; }
        public string CreatedDateTime { get; set; }
        public string CreatedUserName { get; set; }
        public string Type { get; set; }
    }
}
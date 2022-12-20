using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsolidationSystem.Models
{
    public class ProcessResultInvView
    {
        
        public string Id { get; set; }
        public string BatchNo { get; set; }
        public string InternalRef { get; set; }
        public DateTime DocDate { get; set; }
        public string Description { get; set; }
        public string DocNo { get; set; }
        public string ItemNo { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }       
        public string CRM_RefCode { get; set; }
        public string CRM_PartnerRefNo { get; set; }
        public string CRM_BookingStatus { get; set; }
        public string CRM_PropertyName { get; set; }
        public string CRM_Property_Country { get; set; }
        public decimal CRM_Amount { get; set; }
        public decimal AmountDiff { get; set; }
        public string Type { get; set; }
        public string CreatedDateTime { get; set; }
        public string CreatedUserName { get; set; }
        public bool IsEnabled { get; set; }
    }
   
}
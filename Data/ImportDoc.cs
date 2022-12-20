using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolidationSystem.Data
{
    public class ImportDoc
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       
        public Guid Id { get; set; }
        public string InternalReference { get; set; }
        public DateTime DocDate { get; set; }

        public string Description { get; set; }
        public string DocNo { get; set; }
        public string ItemNo { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUserName { get; set; }
        public bool IsEnabled { get; set; }
    }
}
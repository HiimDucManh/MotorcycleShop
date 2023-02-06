using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace FinalProject.Model
{
    public class MaintenanceModel
    {
        public string No { get; set; }
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string ProductId { get; set; }
        public string CustomerId { get; set; }
        public string StaffId { get; set; }
        public string Price { get; set; }
        public bool Status { get; set; } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Model
{
    public class OrderModel
    {
        public string No { get; set; }
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string ProductId { get; set; }
        public string CustomerId { get; set; }
        public string StaffId { get; set; }
        public string Price { get; set; }
        public string Status { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace TestTask.Models
{
    public partial class Sale
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int SalesPointId { get; set; }
        public int? BuyerId { get; set; }
        public int SalesData { get; set; }
        public int TotalAmount { get; set; }
    }
}

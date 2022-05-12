using System;
using System.Collections.Generic;

#nullable disable

namespace TestTask.Models
{
    public partial class SalesDatum
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductIdAmount { get; set; }
    }
}

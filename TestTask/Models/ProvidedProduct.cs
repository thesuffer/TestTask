using System;
using System.Collections.Generic;

#nullable disable

namespace TestTask.Models
{
    public partial class ProvidedProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
    }
}

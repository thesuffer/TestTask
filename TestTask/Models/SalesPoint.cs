using System;
using System.Collections.Generic;

#nullable disable

namespace TestTask.Models
{
    public partial class SalesPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProvidedProducts { get; set; }
    }
}

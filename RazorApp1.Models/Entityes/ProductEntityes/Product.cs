using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfases;

namespace RazorApp1.Models.Entityes
{
    public record Product : IProduct
    {
        public int ProductId { get; init; }
        public string? ProductName { get; init; }
        public decimal Prise { get; set; }
    }
}

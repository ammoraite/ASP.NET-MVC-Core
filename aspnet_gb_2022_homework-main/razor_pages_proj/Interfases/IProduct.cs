using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfases
{
    public interface IProduct
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Prise { get; set; }
    }
}

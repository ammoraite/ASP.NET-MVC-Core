using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AmmoraiteCollections;

namespace Interfases
{
    public interface IProductCategory<IProduct>: ICategory
    {
        public int ProductCatergoryId { get; set; }
        public string? ProductCatergoryName { get; set; }
        public ConcurrentList<IProduct> Products { get; set; }
    }
}

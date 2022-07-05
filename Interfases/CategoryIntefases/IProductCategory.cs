<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

=======
﻿
>>>>>>> Update_Collection
using AmmoraiteCollections;

namespace Interfases
{
<<<<<<< HEAD
    public interface IProductCategory<IProduct>: ICategory
=======
    public interface IProductCategory : ICategory<IProduct>
>>>>>>> Update_Collection
    {
        public int ProductCatergoryId { get; set; }
        public string? ProductCatergoryName { get; set; }
        public ConcurrentList<IProduct> Products { get; set; }
    }
}

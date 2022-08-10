using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    // her kategorinin birden fazla product'u vardır. 
    public class Category : BaseEntity
    {

        public string? Name { get; set; }

        //navigation property
        public ICollection<Product> Products { get; set; }
    }
}

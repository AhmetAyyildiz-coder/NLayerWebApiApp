using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    public class ProductFeature
    {
        public int Id{ get; set; }
        public System.Drawing.Color Color { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        //navigationProperty
        public int ProductId { get; set; }
        
        public Product? Product { get; set; }
    }
}

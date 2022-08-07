using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    //her product bir kategoriye bağlıdır. 
    public class Product : BaseEntity 
    {
        public string Name { get; set; }    
        public int Stock { get; set; }
        public decimal Price { get; set; }

        //ef core bu property'yi otomatik olarak ClassName+Id olarak okuyor ve bunu 
        //foreign key olarak belirliyor.
        //buna alternatif olarak da ForeignKey property'si verilmeli. 
        public int CategoryId { get; set; }

        //navigation property
        //[ForeignKey("CategoryId")] //Bu sekilde kullanıma aslında gerek yok , ama gerekirse yazarsın
        public Category Category { get; set; }

        public ProductFeature? ProductFeature { get; set; }


    }
}

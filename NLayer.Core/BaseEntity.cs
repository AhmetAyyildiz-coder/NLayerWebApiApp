using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    /*Class abstract olarak işaretledik cünkü bu sınıftan nesne örnegi alınsın 
     * istemiyoruz.
     */
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } 
    }
}

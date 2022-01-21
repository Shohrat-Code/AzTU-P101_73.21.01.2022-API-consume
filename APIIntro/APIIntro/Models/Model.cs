using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIIntro.Models
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Year { get; set; }
        public decimal Engine { get; set; }
        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        [ForeignKey("Color")]
        public int ColorId { get; set; }
        public Color Color { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIIntro.Models.DTOs
{
    public class DtoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Year { get; set; }
        public decimal Engine { get; set; }
        public int BrandId { get; set; }
        public DtoBrand Brand { get; set; }
        public int ColorId { get; set; }
        public DtoColor Color { get; set; }
    }
}

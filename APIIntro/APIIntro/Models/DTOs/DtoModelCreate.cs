using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIIntro.Models.DTOs
{
    public class DtoModelCreate
    {
        [MaxLength(150), Required]
        public string Name { get; set; }
        public string Image { get; set; }
        public string ImageBase64 { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public decimal Engine { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required]
        public int ColorId { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIConsume.ViewModels
{
    public class VmModelCreate
    {
        public int Id { get; set; }
        [MaxLength(150),MinLength(1), Required]
        public string Name { get; set; }
        [Required, Range(1915, 2022)]
        public int Year { get; set; }
        [Required]
        public decimal Engine { get; set; }
        public string EngineString { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required]
        public int ColorId { get; set; }
        public string Image { get; set; }
        public string ImageBase64 { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}

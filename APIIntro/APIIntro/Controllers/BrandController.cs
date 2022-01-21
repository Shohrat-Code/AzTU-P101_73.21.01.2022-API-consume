using APIIntro.Data;
using APIIntro.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIIntro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BrandController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetColors()
        {
            List<DtoBrand> model = _context.Brands.Select(c => new DtoBrand { Id = c.Id, Name = c.Name }).ToList();
            return Ok(model);
        }
    }
}

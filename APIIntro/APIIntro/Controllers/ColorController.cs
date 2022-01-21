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
    public class ColorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ColorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetColors()
        {
            List<DtoColor> model = _context.Colors.Select(c => new DtoColor { Id = c.Id, Name = c.Name }).ToList();
            return Ok(model);
        }
    }
}

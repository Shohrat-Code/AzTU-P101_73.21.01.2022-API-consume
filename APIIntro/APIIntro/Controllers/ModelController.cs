using APIIntro.Data;
using APIIntro.Models;
using APIIntro.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace APIIntro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ModelController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetModels()
        {
            List<DtoModel> model = _context.Models
                                                .Include(b => b.Brand)
                                                .Include(c => c.Color)
                                                .Select(m => new DtoModel
                                                {
                                                    Id = m.Id,
                                                    Name = m.Name,
                                                    Year = m.Year,
                                                    Engine = m.Engine,
                                                    BrandId = m.BrandId,
                                                    Brand = new DtoBrand { Id = m.BrandId, Name = m.Brand.Name },
                                                    ColorId = m.ColorId,
                                                    Color = new DtoColor { Id = m.ColorId, Name = m.Color.Name }
                                                })
                                                .ToList();

            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetModel(int id)
        {
            DtoModel model = _context.Models
                                                .Include(b => b.Brand)
                                                .Include(c => c.Color)
                                                .Select(m => new DtoModel
                                                {
                                                    Id = m.Id,
                                                    Name = m.Name,
                                                    Year = m.Year,
                                                    Engine = m.Engine,
                                                    BrandId = m.BrandId,
                                                    Brand = new DtoBrand { Id = m.BrandId, Name = m.Brand.Name },
                                                    ColorId = m.ColorId,
                                                    Color = new DtoColor { Id = m.ColorId, Name = m.Color.Name }
                                                })
                                                .FirstOrDefault(m => m.Id == id);

            return Ok(model);
        }

        [HttpPost]
        public IActionResult Create(DtoModelCreate model)
        {
            if (ModelState.IsValid)
            {
                byte[] fileBytes = Convert.FromBase64String(model.ImageBase64);
                MemoryStream ms = new MemoryStream(fileBytes);
                IFormFile imageFile = new FormFile(ms, 0, fileBytes.Length, model.Image, model.Image);

                string fileName = Guid.NewGuid() + "-" + imageFile.FileName;
                string filePath = Path.Combine("wwwroot", "Uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                Model newModel = new Model()
                {
                    Name = model.Name,
                    Year = model.Year,
                    Engine = model.Engine,
                    BrandId = model.BrandId,
                    ColorId = (int)model.ColorId,
                    Image = fileName
                };

                _context.Models.Add(newModel);
                _context.SaveChanges();
                return Ok(newModel);
            }

            ModelState.AddModelError("Test Error", "Model is not valid");
            //return BadRequest(model);
            return StatusCode(StatusCodes.Status404NotFound, "Model is not valid");
        }

        [HttpPatch]
        public IActionResult Update([FromHeader] int? id, [FromBody] DtoModelCreate dtoModel)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Model model = _context.Models.Find(id);
            if (model == null)
            {
                return BadRequest();
            }

            model.Name = dtoModel.Name;
            model.Year = dtoModel.Year;
            model.Engine = dtoModel.Engine;
            model.BrandId = dtoModel.BrandId;
            model.ColorId = dtoModel.ColorId;
            _context.Models.Update(model);
            _context.SaveChanges();

            return Ok(dtoModel);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Model model = _context.Models.Find(id);
            if (model == null)
            {
                return BadRequest();
            }
            _context.Models.Remove(model);
            _context.SaveChanges();

            return Ok(model);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using joloochusite.Model;
using joloochusite.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using joloochusite.Models;

namespace joolochu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _appEnvironment;
 
        public CarsController(ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars
                .Include(p => p.Mark)
                .Include(p => p.User)
                .ToListAsync();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars
                .Where(p => p.Id == id) 
                .Include(p => p.Mark)
                .Include(p => p.User)
                .FirstOrDefaultAsync();

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }


        //// GET: api/Cars/5
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<FileResult>> GetCarImage(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            byte[] mas = System.IO.File.ReadAllBytes(car.ImagePath);
            string file_type = "image/*";
            string file_name = car.ImageName;
            return File(mas, file_type, file_name);
        }

        //// GET: api/Cars/5
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> PostCarImage(int id, IFormFile formFile)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            if (formFile != null)
            {
                /*if (!Directory.Exists(_appEnvironment.WebRootPath + "/cars/" + car.UserId))
                {
                    Directory.CreateDirectory(_appEnvironment.WebRootPath + "/cars/" + car.UserId);
                }*/
                string fileName = "user_" + car.UserId + "_image_" + formFile.FileName;
                string path = "/cars/" + fileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                car.ImageName = formFile.FileName;
                car.ImagePath = _appEnvironment.WebRootPath + path;

                _context.Update(car);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cars
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var car = await _context.Cars
                .Where(p => p.Id == id)
                .Include(p => p.Mark)
                .Include(p => p.User)
                .FirstOrDefaultAsync();
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return car;
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}

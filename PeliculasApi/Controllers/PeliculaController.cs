using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.Data;
using PeliculasApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
        private readonly ApplicationDBContext _db;

        public PeliculaController(ApplicationDBContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetPeliculas()
        {
            var lista = await _db.Peliculas.OrderBy(p => p.NombrePelicula)
                .Include(p => p.Categoria).ToListAsync();

            return Ok(lista);

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPeliculas(int id)
        {
            var obj = await _db.Peliculas.Include(p=> p.Categoria).FirstOrDefaultAsync(p => p.Id == id);

            if (obj == null)
            {
                return NotFound();

            }

            return Ok(obj);

        }


        [HttpPost]
        public async Task<IActionResult> PostPeliculas([FromBody] Pelicula pelicula)
        {
            if (pelicula == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);

            }

            await _db.AddAsync(pelicula);
            await _db.SaveChangesAsync();

            return Ok();



        }



        
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRestUniversidad.Data;

namespace ApiRestUniversidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultiesController : ControllerBase
    {
        private readonly UniversidadContext _context;

        public FacultiesController(UniversidadContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           List <Faculty> data = await _context.Faculties.ToListAsync();
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Add(string name)
        {
            string nameF = name.Trim().ToUpper();
            var val = await _context.Faculties.Where(x => x.NameFaculty == nameF).SingleOrDefaultAsync();
            if (val== default)
            {
                Faculty newFa = new Faculty { NameFaculty = name.ToUpper() };
                await _context.AddAsync(newFa);
               await  _context.SaveChangesAsync();

                 return Ok("Informacion agregada con exito");
            }
           
            return StatusCode(404, "Ya existen registros con el mismo valor de la informacion introducidad");
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Faculty data)
        {
            var faculty = await _context.Faculties.Where(x=>x.IdFaculty==data.IdFaculty).SingleOrDefaultAsync();
            if (faculty != default)
            {
                faculty.NameFaculty = data.NameFaculty.ToUpper();
                //_context.SaveChangesAsync()
                await _context.SaveChangesAsync();
                return Ok("Registro actualizado con exito");
            }
            return StatusCode(404, "El id ingresado no coincide con ningun registro");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var faculty = await _context.Faculties.Where(x => x.IdFaculty == id).SingleOrDefaultAsync();
            if(faculty!= default)
            {
                _context.Faculties.Remove(faculty);
                await _context.SaveChangesAsync();
                return Ok("EL registro se ha eliminado con exito");
            }
            return StatusCode(404, "El id ingresado no coincide con ningun regisitro");
        }
    }
}

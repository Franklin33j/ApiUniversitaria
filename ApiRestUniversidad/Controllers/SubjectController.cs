
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRestUniversidad.Data;

namespace ApiRestUniversidad.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {

        private readonly UniversidadContext _context;

        public SubjectController(UniversidadContext context)
        {
            _context= context;
        }

        [HttpGet]
        public async  Task<IActionResult> GetAll()
        {
            var data = await _context.Subjects.ToListAsync();

            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] string name)
        {
            var subject =await _context.Subjects.Where(x => x.NameSubject == name).SingleOrDefaultAsync();
            if (subject != null)
                return BadRequest("Ya existe un registro para " + name.ToUpper());
            subject.NameSubject = name.ToUpper();
            await _context.AddAsync(subject);
            await _context.SaveChangesAsync();
            return Ok("Registro agregado con exito");
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody ]Subject subject)
        {
            var subjectExist = await _context.Subjects.FindAsync(subject.IdSubject);
            if(subject== null)
            {
                return BadRequest("El id ingresado no ha sido encontrado");
            }
            subjectExist.NameSubject = subject.NameSubject.ToUpper();
            await _context.SaveChangesAsync();
            return Ok("Registro actualizado con exito");
        }
        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            var subjectExist = await _context.Subjects.FindAsync(id);
            if (subjectExist == null)
                return BadRequest("El id ingresado no ha sido encontrado");
            _context.Remove(subjectExist);
            await _context.SaveChangesAsync();
            return Ok("Registro eliminado con exito");
        }
    }
}

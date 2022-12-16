
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
        /// <summary>
        /// Retorna la lista de todos las materias registrafas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async  Task<IActionResult> GetAll()
        {
            var data = await _context.Subjects.ToListAsync();

            return Ok(data);
        }

        /// <summary>
        /// Agrega una nueva materia
        /// </summary>
        /// <param name="subject">Datos de la materia</param>
        /// <returns>Mensaje de exito o error</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromHeader] string subject)
        {
            
            if ( await _context.Subjects.AnyAsync(z=>z.NameSubject.Equals(subject)))
                return BadRequest("Ya existe un registro para " + subject.ToUpper());

            Subject subjectExist = new  Subject{ NameSubject = subject.ToUpper() };

            await _context.AddAsync(subjectExist);
            await _context.SaveChangesAsync();
            return Ok("Registro agregado con exito");
        }
        /// <summary>
        /// Actualiza un registro de Materia
        /// </summary>
        /// <param name="subject">Datos de materia</param>
        /// <returns>Mensaje de exito o error</returns>
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
        /// <summary>
        /// Elimina un registro Materia a partir del id 
        /// </summary>
        /// <param name="id">Id del registro Materia </param>
        /// <returns></returns>
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

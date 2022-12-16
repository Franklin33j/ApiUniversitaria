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
        /// <summary>
        /// Regresa un listado de todas las facultades de la universidad
        /// </summary>
        /// <returns>lista de facultades</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll() =>  Ok(await _context.Faculties.OrderByDescending(x=>x.IdFaculty).ToListAsync());

        /// <summary>
        /// Retorna una faculta pero pide un argumento que es el identificador 
        /// </summary>
        /// <param name="id">indice de facultad</param>
        /// <returns>Datos de la facultad o mensaje de error</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return await _context.Faculties.FindAsync(id) is Faculty faculty ? Ok(faculty) :
                NotFound(new { message = "El id ingresado no ha sido encontrado" });
        }


        /// <summary>
        /// Anade una nueva facultad a la base de datos
        /// </summary>
        /// <param name="name">Nombre para una nueva facultad</param>
        /// <returns>Mensaje de exito o error</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> Add(string name)
        {
            string nameF = name.Trim().ToUpper();
            var val = await _context.Faculties.Where(x => x.NameFaculty == nameF).SingleOrDefaultAsync();
            if (val == default)
            {
                Faculty newFa = new Faculty { NameFaculty = name.ToUpper() };
                await _context.AddAsync(newFa);
                await _context.SaveChangesAsync();

                return Ok("Informacion agregada con exito");
            }

            return NotFound(new {message="Ya existen registros con el mismo valor de la informacion introducida"});
        }
        /// <summary>
        /// Actualiza la facultad apartir de los datos de la facultad
        /// </summary>
        /// <param name="data">Contiene los datos de la facultad</param>
        /// <returns>Mensaje de exito o error</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
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
            return NotFound(new { message = "El id ingresado no coincide con ningun registro"});
        }

        // Se desabilito porque debe existir un eliminacion en cascada por lo cual no va a ser posible eliminar registros de Faculy
        // por el momento
  
        //[HttpDelete]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var faculty = await _context.Faculties.Where(x => x.IdFaculty == id).SingleOrDefaultAsync();
        //    if (faculty != default)
        //    {
        //        _context.Faculties.Remove(faculty);
        //        await _context.SaveChangesAsync();
        //        return Ok("EL registro se ha eliminado con exito");
        //    }
        //    return StatusCode(404, "El id ingresado no coincide con ningun regisitro");
        //}
    }
}

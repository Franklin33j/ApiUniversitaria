using ApiRestUniversidad.Data;
using ApiRestUniversidad.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRestUniversidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly UniversidadContext _context;
        private readonly IMapper _mapper;

        public TeacherController(UniversidadContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Retorna la lista de maestros registrados
        /// </summary>
        /// <returns>Lista de maestros</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           return Ok(await _context.Teachers.ToListAsync());
        }
        /// <summary>
        /// Agrega un nuevo maestro
        /// </summary>
        /// <param name="teacher">Datos de maestro </param>
        /// <returns>Mensaje de exito o error</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TeacherDTO teacher)
        {
            var data = _mapper.Map<Teacher>(teacher);
            await _context.Teachers.AddAsync(data);
            await _context.SaveChangesAsync();
            return Ok("Registro agregado con exito");
        }
        
        /// <summary>
        /// Actualiza un registro de Maestro
        /// </summary>
        /// <param name="teacher">Datos a actualizar del maestro</param>
        /// <returns>Mensaje de exito o error</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody ] Teacher teacher)
        {
            if (! await _context.Teachers.AnyAsync(x=>x.IdTeacher== teacher.IdTeacher))
                return NotFound(new { message = "El registo no existe" });
            _context.Update(teacher);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Registro actualizado con exito" });
        }


        /// <summary>
        /// Elimina un registro de Maestro apartir de su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Mensaje de exito o error</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {

            var teacherExist = await _context.Teachers.FindAsync(id);
            if (teacherExist == null)
                return NotFound(new { message = "El registo no existe" });
            _context.Remove(teacherExist);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Registro eliminado con exito" });
        }
    }
}

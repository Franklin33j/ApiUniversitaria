using ApiRestUniversidad.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRestUniversidad.Data;


namespace ApiRestUniversidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly UniversidadContext _context;
        private readonly IMapper _mapper;

        public StudentController(UniversidadContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Retorna la lista completa de todos lo estudiantes registrados
        /// </summary>
        /// <returns>Lista de estudiantes</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            _context.Students.AsNoTracking().ToList();
            var data = from student in _context.Students
                        select new
                        {
                            id = student.IdStudent,
                            firstName = student.FirstNames,
                            lastNames = student.LastNames,
                            nameCareer = student.IdCareerNavigation.NameCareer,
                            cum = student.Cum,
                        };
            return Ok(data);
        }

        /// <summary>
        /// Crea un nuevo estudiante
        /// </summary>
        /// <param name="student">Datos del estudiante</param>
        /// <returns>Mensaje de Exito o Error</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student student)
        {
            if(!ModelState.IsValid) return BadRequest(new {message="Error al ingresar la informacion"});
            if ( !await _context.Careers.AnyAsync(x=>x.IdCareer== student.IdCareer))
            {
                return BadRequest(new {message= "El Id de la carrera no se encuentra registrado" });
            }
            student.LastNames.ToUpper();
            student.FirstNames.ToUpper();
            await _context.AddAsync(student);
            await _context.SaveChangesAsync();
            return Ok(new {message= "Registro agregado con exito" });
        }


        /// <summary>
        /// Actualiza un estudiante
        /// </summary>
        /// <param name="student">Datos a modificar del estudiante</param>
        /// <returns>Mensaje de exito o error</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]StudentDTO student)
        {
            try
            {
                if (!ModelState.IsValid) return NotFound();

                if (!_context.Students.Any(a => a.IdStudent == student.IdStudent)) 
                {
                    return BadRequest(new { message = "El Id ingresado de estudiante no se encuentra registrado" });
                }
                if (!_context.Students.Any(a => a.IdCareer == student.IdCareer))
                {
                    return BadRequest(new { message = "El Id ingresado de carrera no se encuentra registrado" });
                }
                student.FirstNames = student.FirstNames.ToUpper();
                student.LastNames = student.LastNames.ToUpper();
                Student data = _mapper.Map<Student>(student);

                _context.Update(data);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Registro actualizado con exito" });

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return BadRequest();
            }
        }


        /// <summary>
        /// Elimina un usuario apartir de su indice
        /// </summary>
        /// <param name="id">Indice del usuario</param>
        /// <returns>Mensaje de exito o error </returns> 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var infoStudent = await _context.Students.FindAsync(id);

            if(infoStudent!= null)
            {
                _context.Remove(infoStudent);
                await _context.SaveChangesAsync();
                return Ok("Registro Eliminado con exito");
            }
            return BadRequest("El id ingresado no se encuentra registrado");
        }
    }
    
}

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
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
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentDTO student)
        {
            
            var careerStudent =await _context.Students.Where(x => x.IdCareer == student.IdCareer).SingleOrDefaultAsync();
            if (careerStudent == null)
            {
                return BadRequest("El Id de la carrera no se encuentra registrado");
            }
            var data=_mapper.Map<Student>(student);
            await _context.AddAsync(data);
            await _context.SaveChangesAsync();
            return Ok("Registro agregado con exito");
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Student student)
        {
            var infoStudent =await _context.Students.FindAsync(student.IdStudent);

            if(infoStudent!=null)
            {
                infoStudent = student;
                await _context.SaveChangesAsync();
                return Ok("Registro actualizado con exito");
            }
            return BadRequest("El id ingresado no existe");
            
        }

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

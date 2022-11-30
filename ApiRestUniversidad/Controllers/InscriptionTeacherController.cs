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
    public class InscriptionTeacherController : ControllerBase
    {
        private readonly UniversidadContext _context;
        private readonly IMapper _mapper;
        public InscriptionTeacherController(UniversidadContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = from inscriptionTeacher in _context.Recordinscriptionteachers
                        join teacher in _context.Teachers on inscriptionTeacher.IdTeacher equals teacher.IdTeacher
                        join subject in _context.Subjects on inscriptionTeacher.IdSubject equals subject.IdSubject
                        select new
                        {
                            IdRecord = inscriptionTeacher.IdRecord,
                            NameSubject = subject.NameSubject,
                            NameTeacher = teacher.FirstNames + " " + teacher.LastNames
                            //se puede crear un viewModel

                        };
            return Ok(await query.ToListAsync());
        }
        [HttpGet("GetByIdSubject/{idSubject}")]
        public async Task<IActionResult> GetByIdSubject(int? idSubject)
        {
            var data = await _context.Subjects.FindAsync(idSubject);
            if (data == null)
                return NotFound(new { message = "El id de Materia no se encuentra registrado" });

            var query = from inscriptionTeacher in _context.Recordinscriptionteachers
                        join teacher in _context.Teachers on inscriptionTeacher.IdTeacher equals teacher.IdTeacher
                        join subject in _context.Subjects on inscriptionTeacher.IdSubject equals subject.IdSubject
                        where subject.IdSubject == idSubject
                        select new
                        {
                            IdRecord = inscriptionTeacher.IdRecord,
                            NameSubject = subject.NameSubject,
                            NameTeacher = teacher.FirstNames + " " + teacher.LastNames
                            //se puede crear un viewModel

                        };

            return Ok(await query.ToListAsync());
        }
        [HttpGet("GetByIdTeacher/{idTeacher}")]
        public async Task<IActionResult> GetByIdTeacher(int idTeacher)
        {
            var data = await _context.Teachers.FindAsync(idTeacher);
            if (data == null)
                return NotFound(new { message = "El id de Maestro no se encuentra registrado" });

            var query = from inscriptionTeacher in _context.Recordinscriptionteachers
                        join teacher in _context.Teachers on inscriptionTeacher.IdTeacher equals teacher.IdTeacher
                        join subject in _context.Subjects on inscriptionTeacher.IdSubject equals subject.IdSubject
                        where teacher.IdTeacher == idTeacher
                        select new
                        {
                            IdRecord = inscriptionTeacher.IdRecord,
                            NameSubject = subject.NameSubject,
                            NameTeacher = teacher.FirstNames + " " + teacher.LastNames
                            //se puede crear un viewModel

                        };
            return Ok(await query.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Recordinscriptionteacher recordTeacher)
        {
            //validar que id teacher e id materia sea valido

            var teacherExist = _context.Teachers.FindAsync(recordTeacher.IdTeacher);
            if (teacherExist == null)
                return NotFound(new { message = "El id de Maestro no ha sido encontrado" });
            var subjectExist = _context.Subjects.FindAsync(recordTeacher.IdSubject);
            if (subjectExist == null)
                return NotFound(new { message = "El id de la materia no ha sido encontrado" });

            await _context.AddAsync(recordTeacher);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registro agregado con exito" });
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] InscriptionTeacherDTO recordTeacher)
        {
            //validar que id teacher e id materia sea valido

            var teacherExist = _context.Teachers.FindAsync(recordTeacher.IdTeacher);
            if (teacherExist == null)
                return NotFound(new { message = "El id del maestro no ha sido encontrado" });
            var subjectExist = _context.Subjects.FindAsync(recordTeacher.IdSubject);
            if (subjectExist == null)
                return NotFound(new { message = "El id de la materia no ha sido encontrado" });

            var data = _mapper.Map<Recordinscriptionteacher>(recordTeacher);
            await _context.AddAsync(recordTeacher);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registro agregado con exito" });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var data = _context.Recordinscriptionteachers.FindAsync(id);
            if (data == null)
                return NotFound(new { message = "EL id ingresado no ha sido encontrado" });

            _context.Remove(data);
            return Ok(new { message = "Registro eliminado con exito" });
        }
    }
}

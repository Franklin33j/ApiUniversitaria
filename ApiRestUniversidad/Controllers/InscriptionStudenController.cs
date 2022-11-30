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
    public class ValuesController : ControllerBase
    {

        private readonly UniversidadContext _context;
        private readonly IMapper _mapper;

        public ValuesController(UniversidadContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = from inscriptionStudent in _context.Recordinscriptionstudents
                        join student in _context.Students on inscriptionStudent.IdStudent equals student.IdStudent
                        join subject in _context.Subjects on inscriptionStudent.IdSubject equals subject.IdSubject
                        select new
                        {
                            IdRecord = inscriptionStudent.IdRecord,
                            NameSubject = subject.NameSubject,
                            NameTeacher = student.FirstNames + " " + student.LastNames
                            //se puede crear un viewModel

                                            };
                                return Ok(await query.ToListAsync());
        }
        [HttpGet("GetByIdSubject/{idSubject}")]
        public async Task<IActionResult> GetByIdSubject(int? idSubject)
        {
            var data = await _context.Subjects.FindAsync(idSubject);
            if (data == null)
                return NotFound(new { message = "El id de Materia ingresada no se encuentra registrado" });

            var query = from inscriptionStudent in _context.Recordinscriptionstudents
                        join student in _context.Students on inscriptionStudent.IdStudent equals student.IdStudent
                        join subject in _context.Subjects on inscriptionStudent.IdSubject equals subject.IdSubject
                        where subject.IdSubject == idSubject
                        select new
                        {
                            IdRecord = inscriptionStudent.IdRecord,
                            NameSubject = subject.NameSubject,
                            NameTeacher = student.FirstNames + " " + student.LastNames
                            //se puede crear un viewModel

                        };

            return Ok(await query.ToListAsync());
        }
        [HttpGet("GetByIdSubject/{idStudent}")]
        public async Task<IActionResult> GetByIdStudent(int? idStudent)
        {
            var data = await _context.Subjects.FindAsync(idStudent);
            if (data == null)
                return NotFound(new { message = "El id del Estudiante ingresado no se encuentra registrado" });

            var query = from inscriptionStudent in _context.Recordinscriptionstudents
                        join student in _context.Students on inscriptionStudent.IdStudent equals student.IdStudent
                        join subject in _context.Subjects on inscriptionStudent.IdSubject equals subject.IdSubject
                        where subject.IdSubject == idStudent
                        select new
                        {
                            IdRecord = inscriptionStudent.IdRecord,
                            NameSubject = subject.NameSubject,
                            NameTeacher = student.FirstNames + " " + student.LastNames
                            //se puede crear un viewModel

                        };

            return Ok(await query.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Recordinscriptionstudent recordStudent)
        {
            //validar que id teacher e id materia sea valido

            var studentExist = _context.Students.FindAsync(recordStudent.IdStudent);
            if (studentExist == null)
                return NotFound(new { message = "El id de alumno no ha sido encontrado" });
            var subjectExist = _context.Subjects.FindAsync(recordStudent.IdSubject);
            if (subjectExist == null)
                return NotFound(new { message = "El id de la materia no ha sido encontrado" });


            await _context.AddAsync(recordStudent);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registro agregado con exito" });
        }



        [HttpPut]
        public async Task<IActionResult> Update([FromBody] InscriptionStudentDTO recordStudent)
        {
            //validar que id teacher e id materia sea valido

            var teacherExist = _context.Teachers.FindAsync(recordStudent.IdSubject);
            if (teacherExist == null)
                return NotFound(new { message = "El id de estudiante no ha sido encontrado" });
            var subjectExist = _context.Subjects.FindAsync(recordStudent.IdSubject);
            if (subjectExist == null)
                return NotFound(new { message = "El id de la materia no ha sido encontrado" });

            var data = _mapper.Map<Recordinscriptionstudent>(recordStudent);
            await _context.AddAsync(recordStudent);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registro agregado con exito" });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var data = _context.Recordinscriptionstudents.FindAsync(id);
            if (data == null)
                return NotFound(new { message = "El id ingresado no ha sido encontrado" });

            _context.Remove(data);
            return Ok(new { message = "Registro eliminado con exito" });
        }
    }
}

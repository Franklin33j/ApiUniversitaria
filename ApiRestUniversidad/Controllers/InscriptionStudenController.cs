
using ApiRestUniversidad.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ApiRestUniversidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscriptionStudentController : ControllerBase
    {

        private readonly UniversidadContext _context;
        private readonly IMapper _mapper;

        public InscriptionStudentController(UniversidadContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// obtiene toda la lista de inscripciones de estudiantes realizadas
        /// </summary>
        /// <returns>retorno una lisrta de Inscripciones</returns>
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

        /// <summary>
        /// Retorna  una lista de inscripciones de estudiantes apartir del id de una materia
        /// </summary>
        /// <param name="idSubject">id de una materia registrada</param>
        /// <returns>Lista de registros por materia ingresada</returns>
        [HttpGet(("GetByIdSubject/{idSubject:int}"))]

        public async Task<IActionResult> GetByIdSubject(int idSubject)
        {
         
            if (!await _context.Subjects.AnyAsync(x => x.IdSubject == idSubject)) 
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


        /// <summary>
        /// Retorna una lista de inscripciones de estudiantes por un id de estudiante
        /// </summary>
        /// <param name="idStudent">id del estudiante registrado</param>
        /// <returns>Una lista de inscripcion de un estudiante en especifico</returns>
        [HttpGet("GetByIdStudent/{idStudent:int}")]
        public async Task<IActionResult> GetByIdStudent(int idStudent)
        {

            if (!await _context.Students.AnyAsync(x => x.IdStudent == idStudent))
                return NotFound(new { message = "El id del Estudiante ingresado no se encuentra registrado" });

            var query = from inscriptionStudent in _context.Recordinscriptionstudents
                        join student in _context.Students on inscriptionStudent.IdStudent equals student.IdStudent
                        join subject in _context.Subjects on inscriptionStudent.IdSubject equals subject.IdSubject
                        where subject.IdSubject == idStudent
                        select new
                        {
                            IdRecord = inscriptionStudent.IdRecord,
                            NameSubject = subject.NameSubject,
                            NameTeacher = student.FirstNames + " " + student.LastNames,
                            Created_At = inscriptionStudent.Date
                            //se puede crear un viewModel

                        };

            return Ok(await query.ToListAsync());
        }
        /// <summary>
        /// Registra una nueva inscripcion de estudiante
        /// </summary>
        /// <param name="recordStudent">contiene informacion como el id de materia y el id del estudiante</param>
        /// <returns>Mensaje de exito o error</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Recordinscriptionstudent recordStudent)
        {
            //validar que id teacher e id materia sea valido

            if (! await _context.Students.AnyAsync(x=>x.IdStudent== recordStudent.IdStudent))
                return NotFound(new { message = "El id de alumno no ha sido encontrado" });
            if (!await _context.Subjects.AnyAsync(x=>x.IdSubject== recordStudent.IdSubject))
                return NotFound(new { message = "El id de la materia no ha sido encontrado" });

            var data = _mapper.Map<Recordinscriptionstudent>(recordStudent);
            await _context.AddAsync(data);
            await _context.AddAsync(recordStudent);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registro agregado con exito" });
        }

        /// <summary>
        /// Actualiza un registro de inscripcion de estudiantes
        /// </summary>
        /// <param name="recordStudent">Agrega informacion de la materia y el estudiante</param>
        /// <returns>Mensaje de exito o error</returns>

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] InscriptionStudentDTO recordStudent )
        {
            //validar que id teacher e id materia sea valido

            if (! await _context.Students.AnyAsync(x=>x.IdStudent.Equals(recordStudent.IdStudent)))
                return NotFound(new { message = "El id de estudiante no ha sido encontrado" });

            if (! await _context.Subjects.AnyAsync(x=>x.IdSubject.Equals(recordStudent.IdSubject)))
                return NotFound(new { message = "El id de la materia no ha sido encontrado" });

            var data = _mapper.Map<Recordinscriptionstudent>(recordStudent);
             _context.Update(data);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registro actualizado con exito" });
        }
        /// <summary>
        /// Elimina un registro de inscripcion de estudiante
        /// </summary>
        /// <param name="id">Representa el id del registro de inscripcion</param>
        /// <returns>Mensaje de exito o error</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromHeader] int id)
        {
            var data = _context.Recordinscriptionstudents.FindAsync(id);
            if (data == null)
                return NotFound(new { message = "El id ingresado no ha sido encontrado" });

            _context.Remove(data);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Registro eliminado con exito" });
        }
    }
}

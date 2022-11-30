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
    public class RatingController : ControllerBase
    {
        private readonly UniversidadContext _context;
        private readonly IMapper _mapper;
        public RatingController(UniversidadContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {

            var query = from rating in _context.Ratings
                        join student in _context.Students on rating.IdStudent equals student.IdStudent
                        join subject in _context.Subjects on rating.IdSubject equals subject.IdSubject
                        where student.IdStudent == id
                        select new
                        {
                            //se puede crear un viewModel
                            id = rating.IdRating,
                            note_1 = rating.Note1,
                            note_2 = rating.Note2,
                            note_3 = rating.Note3,
                            note_4 = rating.Note4,
                            nameStudent = student.FirstNames + " " + student.LastNames,
                            materia = subject.NameSubject
                        };


            var data = await query.ToListAsync();
            /*Existe un problema si las notas quedan como nulas, al mapear entity no puede convertir de
             null (se diseno de esa manera en mariadb) a double por lo que no puede sen nulas*/

            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]  Rating rating)
        {
            var student = _context.Students.FindAsync(rating.IdStudent);
            var subject = _context.Subjects.FindAsync(rating.IdSubject);

            if (student == null)
                return NotFound(new { message = "El Id de estudiante no ha sido encontrado" });
                
            if(subject!= null)
                return NotFound(new { message = "El Id de materia no ha sido encontrado" });
 
            await _context.AddAsync(rating);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Registro agregado con exito" });

        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RatingDTO rating)
        {
            var ratingExist = await _context.Ratings.FindAsync(rating.IdRating);
           if(ratingExist== null)
                return NotFound(new { message = "El Id del record no ha sido encontrado" });

            var student = _context.Students.FindAsync(rating.IdStudent);
            var subject = _context.Subjects.FindAsync(rating.IdSubject);
            if (student == null)
                return NotFound(new { message = "El Id de estudiante no ha sido encontrado" });
            if (subject != null)
                return NotFound(new { message = "El Id de materia no ha sido encontrado" });

            var entity = _mapper.Map<Rating>(rating);
            ratingExist= entity;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Registro actualizado con exito" });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var ratingExist = await _context.Ratings.FindAsync(id);
            if (ratingExist == null)
                return NotFound(new { message = "El Id del record no ha sido encontrado" });

            _context.Remove(ratingExist);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Registro eliminado con exito" });
        }
        
    }
}

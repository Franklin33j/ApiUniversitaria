using ApiRestUniversidad.Data;
using ApiRestUniversidad.DTO;
using ApiRestUniversidad.ViewModels;
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
        /// <summary>
        /// Retorna la lista de materias inscritas asi como las notas de cada materia
        /// el id debe de ser el de estudiante registrado
        /// </summary>
        /// <param name="id">Indice de un estudiante registrado</param>
        /// <returns>Retorno Un Modelo de:notas, nombre estudiante, id, nombre materia</returns>
        [ProducesResponseType(typeof(RatingVM), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {

            var query = from rating in _context.Ratings
                        join student in _context.Students on rating.IdStudent equals student.IdStudent
                        join subject in _context.Subjects on rating.IdSubject equals subject.IdSubject
                        where student.IdStudent == id
                        select new RatingVM
                        {
                            //se puede crear un viewModel
                            Id = rating.IdRating,
                            Note1 = rating.Note1,
                            Note2 = rating.Note2,
                            Note3 = rating.Note3,
                            Note4 = rating.Note4,
                            NameStudent = student.FirstNames + " " + student.LastNames,
                            Subject = subject.NameSubject
                        };


            /*Existe un problema si las notas quedan como nulas, al mapear entity no puede convertir de
             null (se diseno de esa manera en mariadb) a double por lo que no puede sen nulas*/

            return Ok(await query.ToListAsync());
        }
        /// <summary>
        /// Crea un nuevo record de notas respectivo para una materia y un estudiante
        /// </summary>
        /// <param name="rating">Informacion del registro de notas</param>
        /// <returns>Mensaje de exito o error</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]  Rating rating)
        {

            if (!await _context.Students.AnyAsync(x=>x.IdStudent== rating.IdStudent))
                return NotFound(new { message = "El Id de estudiante no ha sido encontrado" });
                
            if(! await _context.Subjects.AnyAsync(x=>x.IdSubject== rating.IdSubject))
                return NotFound(new { message = "El Id de materia no ha sido encontrado" });
 
            await _context.AddAsync(rating);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Registro agregado con exito" });

        }
        /// <summary>
        /// Actualiza la informacion de un record de notas
        /// </summary>
        /// <param name="rating">Informacion del registro de notas<</param>
        /// <returns>Mensaje de exito o error</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RatingDTO rating)
        {

            if (!ModelState.IsValid) return NotFound(new { message = "Error al ingresar los valores" });

            if(!_context.Ratings.Any(x => x.IdRating == rating.IdRating))
            {
                return NotFound(new { message = "El Id de estudiante no ha sido encontrado" });
            }
            if (!_context.Subjects.Any(x => x.IdSubject == rating.IdSubject))
            {
                return NotFound(new { message = "El Id de materia no ha sido encontrado" });
            }
            var entity = _mapper.Map<Rating>(rating);
            _context.Update(entity);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registro actualizado con exito" });

        }

        /// <summary>
        /// Elimina un registro del record de notas 
        /// </summary>
        /// <param name="id">Id del registro de notas</param>
        /// <returns>Mensaje de exito o error</returns>
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

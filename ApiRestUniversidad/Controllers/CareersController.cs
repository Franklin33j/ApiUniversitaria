using ApiRestUniversidad.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiRestUniversidad.Data;

namespace ApiRestUniversidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareersController : ControllerBase
    {
        //carreras por facultad
        private readonly UniversidadContext _context;
        private readonly IMapper _mapper;

        public CareersController(UniversidadContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todas las carreras de la universidad
        /// </summary>
        /// <returns>Listado de Libros</returns>
        [ProducesResponseType(typeof(List<Career>), 200)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //uso del join para traer el id de la Facultad Correspondiente a cada registro de Carrera
            var data = await _context.Careers.Join(_context.Faculties, career => career.IdFaculty, faculty => faculty.IdFaculty,
                (careers, faculties) => new
                {
                    careers, faculties
                }).ToListAsync();
            return Ok(data);
        }

        /// <summary>
        /// obtiene informacion de una carrera en especifico
        /// </summary>
        /// <param name="id">De tipo numerico</param>
        /// <returns>Una carrera</returns>
        [ProducesResponseType(typeof(Career), 200)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var data = await _context.Careers.FindAsync(id);
            if (data == null)
                return NotFound(new { message = "El id no ha sido encontrado " });

            var infoFaculty = await _context.Faculties.FindAsync(data.IdFaculty);

            return Ok(new
            {
                data,
                faculty = infoFaculty.NameFaculty
            });
        }



        /// <summary>
        /// Agrega una nueva carrera y esta asociada a una universidad, por lo que 
        /// es importante conocer el id de la Facultad
        /// </summary>
        /// <param name="career">Informacion de la carrera</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Career), 200)]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CareerDTO career)
        {
            //validar que el registro ya existe
            var validation = await _context.Careers.Where(x => x.NameCareer == career.NameCareer.ToUpper()).SingleOrDefaultAsync();

            if (validation == null)
            {
                //validar que el  id de la facultad existe
                var faculty =await _context.Faculties.FindAsync(career.IdFaculty);
                if (faculty != default)
                {
                    var data = _mapper.Map<Career>(career);
                    _context.Add(data);
                    await _context.SaveChangesAsync();
                    return Ok(new {data,message="registro agreado con exito"});
                }

                return NotFound(new { message= "El id de Facultad ingresado no existe " });

            }
            return BadRequest(new { message="El nombre de la carrera ya se encuentra registrado"});

        }

        /// <summary>
        /// Actualizar la informacion de carrera
        /// </summary>
        /// <param name="data">contiene la nueva informacion de la carrera</param>
        /// <param name="id">El indice del registro</param>
        /// <returns>mensaje de exito o error</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public  async Task  <IActionResult>  Update([FromBody]CareerDTO data, [FromQuery] int id)
        {
            var searchCareer=await _context.Careers.Where(x => x.IdCareer == id).SingleOrDefaultAsync();

            if (searchCareer==null)
            {
                return BadRequest(new { message = "El id ingresado no existe" });
            }
            searchCareer.NameCareer = data.NameCareer;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Registro actualizado con exito" });
            
        }


        /// <summary>
        /// Elimina un registro asociado al id ingresado, pero antes se valida si existe tal id
        /// </summary>
        /// <param name="id">Indice del registro</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int id){
            var career =await _context.Careers.Where(x=>x.IdCareer== id).SingleOrDefaultAsync();
            if (career != default)
            {
                _context.Remove(career);
                await _context.SaveChangesAsync();
                return Ok("registro eliminado con exito");
            }
            return BadRequest("El id ingresado no se encuentra registrado");
        }

    }
}

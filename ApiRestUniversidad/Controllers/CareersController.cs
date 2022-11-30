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
    public class CareersController : ControllerBase
    {
        private readonly UniversidadContext _context;
        private readonly IMapper _mapper;

        public CareersController(UniversidadContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var data = await _context.Careers.ToListAsync();
            //uso del join para traer el id de la Facultad Correspondiente a cada registro de Carrera
            var data = await _context.Careers.Join(_context.Faculties, career => career.IdFaculty, faculty => faculty.IdFaculty,
                (careers, faculties)=>new
                {
                    careers, faculties
                }).ToListAsync();
            return Ok(data);
        }
        //usar viewModels
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
                    return Ok("Registro agregado con exito");
                }

                return BadRequest("El id de Facultad ingresado no existe ");

            }
            return BadRequest("El nombre de la carrera ya se encuentra registrado");

        }

        [HttpPut]
        public  async Task  <IActionResult>  Update([FromBody]Career data)
        {
            var searchCareer=await _context.Careers.Where(x => x.IdCareer == data.IdCareer).SingleOrDefaultAsync();

            if (searchCareer==null)
            {
                searchCareer.NameCareer = data.NameCareer;
                await _context.SaveChangesAsync();
                return Ok("Registro Actualizado con exito");
            }
            return BadRequest("El id ingresado no existe");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id){
            var career =await _context.Careers.Where(x=>x.IdCareer== id).SingleOrDefaultAsync();
            if (career != default)
            {
                _context.Remove(career);
                return Ok("registro eliminado con exito");
            }
            return BadRequest("El id ingresado no se encuentra registrado");
        }

    }
}

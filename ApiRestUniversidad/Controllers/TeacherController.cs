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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           return Ok(await _context.Teachers.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TeacherDTO teacher)
        {
            var data = _mapper.Map<Teacher>(teacher);
            await _context.Teachers.AddAsync(data);
            await _context.SaveChangesAsync();
            return Ok("Registro agregado con exito");
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody ] Teacher teacher)
        {
            var teacherExist = await _context.Teachers.FindAsync(teacher.IdTeacher);
            if (teacherExist == null)
                return NotFound(new { message = "El registo no existe" });
            teacherExist = teacher;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Registro actualizado con exito" });
        }
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

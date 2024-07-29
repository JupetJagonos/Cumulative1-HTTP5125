using SchoolManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherDataController : ControllerBase
    {
        private readonly SchoolManagementSystemDbContext _context;

        public TeacherDataController()
        {
            _context = new SchoolManagementSystemDbContext();
        }

        [HttpGet]
        public IEnumerable<Teacher> GetTeachers()
        {
            return _context.GetAllTeachers();
        }

        [HttpGet("{id}")]
        public Teacher GetTeacher(int id)
        {
            return _context.GetTeacherById(id);
        }

        [HttpGet("search")]
        public IEnumerable<Teacher> SearchTeachers([FromQuery] string name, [FromQuery] DateTime? hireDate, [FromQuery] decimal? salary)
        {
            return _context.SearchTeachers(name, hireDate, salary);
        }

        [HttpPost]
        public IActionResult CreateTeacher([FromBody] Teacher teacher)
        {
            if (teacher == null) return BadRequest();
            _context.AddTeacher(teacher);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTeacher(int id, [FromBody] Teacher teacher)
        {
            if (teacher == null || teacher.TeacherId != id) return BadRequest();
            var existingTeacher = _context.GetTeacherById(id);
            if (existingTeacher == null) return NotFound();
            _context.UpdateTeacher(teacher);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeacher(int id)
        {
            var existingTeacher = _context.GetTeacherById(id);
            if (existingTeacher == null) return NotFound();
            _context.DeleteTeacher(id);
            return Ok();
        }
    }
}

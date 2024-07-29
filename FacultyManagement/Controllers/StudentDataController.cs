using SchoolManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentDataController : ControllerBase
    {
        private readonly SchoolManagementSystemDbContext _context;

        public StudentDataController()
        {
            _context = new SchoolManagementSystemDbContext();
        }

        [HttpGet]
        public IEnumerable<Student> GetStudents()
        {
            return _context.GetAllStudents();
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = _context.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }

        [HttpPost]
        public ActionResult<Student> PostStudent(Student student)
        {
            _context.AddStudent(student);
            return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
        }

        [HttpPut("{id}")]
        public IActionResult PutStudent(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return BadRequest();
            }
            _context.UpdateStudent(student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            _context.DeleteStudent(id);
            return NoContent();
        }
    }
}

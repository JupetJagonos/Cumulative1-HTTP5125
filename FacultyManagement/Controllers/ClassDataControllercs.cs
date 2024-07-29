using SchoolManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassDataController : ControllerBase
    {
        private readonly SchoolManagementSystemDbContext _context;

        public ClassDataController()
        {
            _context = new SchoolManagementSystemDbContext();
        }

        [HttpGet]
        public IEnumerable<Class> GetClasses()
        {
            return _context.GetAllClasses();
        }

        [HttpGet("{id}")]
        public ActionResult<Class> GetClass(int id)
        {
            var classData = _context.GetClassById(id);
            if (classData == null)
            {
                return NotFound();
            }
            return classData;
        }

        [HttpPost]
        public ActionResult<Class> PostClass(Class classData)
        {
            _context.AddClass(classData);
            return CreatedAtAction("GetClass", new { id = classData.ClassId }, classData);
        }

        [HttpPut("{id}")]
        public IActionResult PutClass(int id, Class classData)
        {
            if (id != classData.ClassId)
            {
                return BadRequest();
            }
            _context.UpdateClass(classData);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClass(int id)
        {
            var classData = _context.GetClassById(id);
            if (classData == null)
            {
                return NotFound();
            }
            _context.DeleteClass(id);
            return NoContent();
        }
    }
}

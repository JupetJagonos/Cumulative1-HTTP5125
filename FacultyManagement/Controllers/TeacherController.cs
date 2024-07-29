using SchoolManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        private readonly SchoolManagementSystemDbContext _context;

        public TeacherController()
        {
            _context = new SchoolManagementSystemDbContext();
        }

        public IActionResult Index()
        {
            var teachers = _context.GetAllTeachers();
            return View(teachers);
        }

        public IActionResult Details(int id)
        {
            var teacher = _context.GetTeacherById(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.AddTeacher(teacher);
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        public IActionResult Edit(int id)
        {
            var teacher = _context.GetTeacherById(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Teacher teacher)
        {
            if (id != teacher.TeacherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.UpdateTeacher(teacher);
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        public IActionResult Delete(int id)
        {
            var teacher = _context.GetTeacherById(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _context.DeleteTeacher(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Search(string searchString)
        {
            var teachers = from t in _context.GetAllTeachers()
                           select t;

            if (!string.IsNullOrEmpty(searchString))
            {
                teachers = teachers.Where(t => t.Name.Contains(searchString));
            }

            return View(teachers.ToList());
        }
    }
}

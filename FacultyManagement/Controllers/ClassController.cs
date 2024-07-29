using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class ClassController : Controller
    {
        private readonly SchoolManagementSystemDbContext _context;

        public ClassController()
        {
            _context = new SchoolManagementSystemDbContext();
        }

        public IActionResult Index()
        {
            var classes = _context.GetAllClasses();
            return View(classes);
        }

        public IActionResult Details(int id)
        {
            var classData = _context.GetClassById(id);
            if (classData == null)
            {
                return NotFound();
            }
            return View(classData);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Class classData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.AddClass(classData);
                    return RedirectToAction(nameof(Index));
                }

                return View(classData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }

        public IActionResult Edit(int id)
        {
            var classData = _context.GetClassById(id);
            if (classData == null)
            {
                return NotFound();
            }
            return View(classData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Class classData)
        {
            if (id != classData.ClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.UpdateClass(classData);
                return RedirectToAction(nameof(Index));
            }
            return View(classData);
        }

        public IActionResult Delete(int id)
        {
            var classData = _context.GetClassById(id);
            if (classData == null)
            {
                return NotFound();
            }
            return View(classData);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _context.DeleteClass(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

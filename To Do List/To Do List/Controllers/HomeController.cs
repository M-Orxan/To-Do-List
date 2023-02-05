using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using To_Do_List.DAL;
using To_Do_List.Models;

namespace To_Do_List.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<TaskModel> tasks = await _db.Tasks.Where(x=>x.IsDeactive==false).ToListAsync();
            return View(tasks);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Create(TaskModel task)
        {

            if (task.Title == null)
            {
                ModelState.AddModelError("Title","");
                return View(task);
            }


            bool isExists = await _db.Tasks.Where(x=>x.IsDeactive==false).AnyAsync(x => x.Title == task.Title);

            if (isExists)
            {
                ModelState.AddModelError("Title", "There is already note under this title. Please change the title");
                return View(task);
            }

            if (task.Deadline < DateTime.Now)
            {
                ModelState.AddModelError("Deadline", "Deadline can not be earlier than current time");
                return View(task);

            }

            task.Created = DateTime.Now;
            task.InProgress = true;
            task.IsDeactive = false;


            await _db.Tasks.AddAsync(task);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }





        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            TaskModel? task = await _db.Tasks.FirstOrDefaultAsync(x=>x.Id==id);
            if (task == null)
            {
                return BadRequest();
            }

            task.IsDeactive = true;

            await _db.SaveChangesAsync();


            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Detail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            TaskModel? task = await _db.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (task == null)
            {
                return BadRequest();
            }

            return View(task);
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
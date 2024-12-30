using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_Management_System.Models;
using Task_Management_System.ViewModels;

namespace Task_Management_System.Controllers
{
    public class TasksController : Controller
    {
        private readonly AppDbContext db;
        public TasksController(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> GetAll()
        {
            return View(await db.Tasks.Include(x => x.project).Include(x => x.teamMember).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var taskViewModel = new TaskViewModel
            {
                Projects = await db.Projects.ToListAsync(),
                TeamMembers = await db.TeamMembers.ToListAsync()
            };
            return View(taskViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TaskViewModel task, int id)
        {
            var rec = await db.Tasks.FindAsync(id);
            rec.Title = task.Title;
            rec.Description = task.Description;
            rec.ProjectId = task.ProjectId;
            rec.TeamMemberId = task.TeamMemberId;
            db.Tasks.Update(rec);
            await db.SaveChangesAsync();
            return RedirectToAction("GetAll");
        }

    }
}

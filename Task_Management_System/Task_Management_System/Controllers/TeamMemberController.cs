using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_Management_System.Models;

namespace Task_Management_System.Controllers
{
    public class TeamMemberController : Controller
    {
        private readonly AppDbContext db;
        public TeamMemberController(AppDbContext DB)
        {
            db = DB;
        }
        public async Task<IActionResult> GetAll()
        {
            return View(await db.TeamMembers.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var teamMember = await db.TeamMembers
                .Include(tm => tm.tasks)
                .FirstOrDefaultAsync(tm => tm.Id == id);

            if (teamMember == null)
            {
                return NotFound();
            }

            return View(teamMember);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TeamMember teamMember)
        {
            await db.TeamMembers.AddAsync(teamMember);
            await db.SaveChangesAsync();
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var rec = await db.TeamMembers.FindAsync(id);
            return View(rec);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TeamMember teamMember)
        {
            var rec = await db.TeamMembers.FirstOrDefaultAsync(x => x.Id == teamMember.Id);
            rec.Name = teamMember.Name;
            rec.Email = teamMember.Email;
            rec.Role = teamMember.Role;
            db.TeamMembers.Update(rec);
            await db.SaveChangesAsync();
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var rec = await db.TeamMembers.FindAsync(id);
            return View(rec);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(TeamMember teamMember)
        {
            var rec = await db.TeamMembers.FirstOrDefaultAsync(x => x.Id == teamMember.Id);
            db.TeamMembers.Remove(rec);
            await db.SaveChangesAsync();
            return RedirectToAction("GetAll");
        }
    }
}

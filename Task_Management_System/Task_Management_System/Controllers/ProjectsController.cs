using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Task_Management_System.Models;

namespace Task_Management_System.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly AppDbContext db;
        public ProjectsController(AppDbContext DB)
        {
            db = DB;
        }
        public async Task<IActionResult> GetAll()
        {
            return View(await db.Projects.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var project = await db.Projects
                .Include(p => p.tasks)
                .ThenInclude(t => t.teamMember)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Projects project)
        {
            await db.Projects.AddAsync(project);
            await db.SaveChangesAsync();
            return RedirectToAction("GetAll");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var rec = await db.Projects.FindAsync(id);
            return View(rec);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Projects project)
        {            
            var rec = await db.Projects.FirstOrDefaultAsync(x=> x.Id==project.Id);
            rec.Name= project.Name;
            rec.Description= project.Description;
            rec.StartDate= project.StartDate;
            rec.EndDate= project.EndDate;
            db.Projects.Update(rec);
            await db.SaveChangesAsync();
            return RedirectToAction("GetAll");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var rec = await db.Projects.FindAsync(id);
            return View(rec);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Projects project)
        {
            var rec = await db.Projects.FirstOrDefaultAsync(x=> x.Id == project.Id);
            db.Projects.Remove(rec);
            await db.SaveChangesAsync();
            return RedirectToAction("GetAll");
        }
    }
}

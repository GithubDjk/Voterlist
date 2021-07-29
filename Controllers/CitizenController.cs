using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoterListApp.Models;

namespace VoterListApp.Controllers
{

    public class CitizenController : Controller
    {


        private readonly ApplicationDbContext db;
        private readonly INotyfService _notyf;

        public CitizenController(ApplicationDbContext _db, INotyfService notyf)
        {
            _notyf = notyf;
            db = _db;
        }




        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Voters = await db.Citizens.ToListAsync();
            return Json(new { data = await db.Citizens.ToListAsync() });
        }

        [HttpGet]
        public async Task<IActionResult> CreateEdit(int? id)
        {
            var voter = await db.Citizens.FirstOrDefaultAsync(x => x.id == id);
            if (voter != null)
            {
                return View(voter);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEdit(Citizen Voter)
        {
            if (ModelState.IsValid)
            {
                var voter = await db.Citizens.FirstOrDefaultAsync(x => x.id == Voter.id);
                if (voter != null)
                {
                    voter.Name = Voter.Name;
                    voter.Address = Voter.Address;
                    voter.Age = Voter.Age;
                    voter.Sex = Voter.Sex;
                    voter.fatherName = Voter.fatherName;
                    db.Citizens.Update(voter);
                    _notyf.Success("Successfully Updated");
                }
                else
                {

                    await db.Citizens.AddAsync(Voter);
                    _notyf.Success("Successfully Added");
                }
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                _notyf.Error("Form Not Submitted");
                return View();
            }

        }

        [Authorize]
        public async Task<IActionResult> Detail(int id)
        {
            var voterInfo = await db.Citizens.FirstOrDefaultAsync(x => x.id == id);
            return View(voterInfo);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var Voter = await db.Citizens.FirstOrDefaultAsync(item => item.id == id);
            if (Voter != null)
            {
                db.Citizens.Remove(Voter);
            }
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}

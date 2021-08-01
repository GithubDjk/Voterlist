using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoterListApp.Models;

namespace VoterListApp.Controllers
{

    public class CitizenController : Controller
    {


        private readonly ApplicationDbContext db;
        private readonly INotyfService _notyf;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CitizenController(ApplicationDbContext _db, INotyfService notyf ,IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
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
                //Save image to wwwroot/image
                if (Voter.ImageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(Voter.ImageFile.FileName);
                    string extension = Path.GetExtension(Voter.ImageFile.FileName);
                    Voter.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/images/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await Voter.ImageFile.CopyToAsync(fileStream);
                    }
                }
                var voter = await db.Citizens.FirstOrDefaultAsync(x => x.id == Voter.id);
                if (voter != null)
                {
                    voter.Name = Voter.Name;
                    voter.Address = Voter.Address;
                    voter.Age = Voter.Age;
                    voter.Sex = Voter.Sex;
                    voter.fatherName = Voter.fatherName;
                    voter.ImageName = Voter.ImageName;
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

                //delete image from wwwroot/image
                var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", Voter.ImageName);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
                db.Citizens.Remove(Voter);
            }
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}

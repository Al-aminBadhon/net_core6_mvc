using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.Data;
using App.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using App.DAL.Utilities;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;
using App.Home.FileUploadService;

namespace App.Home.Controllers
{
    [Authorize]
    public class ExecutivesController : Controller
    {
        private readonly MHDBContext _context;
        private readonly AppUser _appUser;
        private readonly IFileUploadService _fileUploadService;

        public ExecutivesController(MHDBContext context, IFileUploadService fileUploadService, AppUser appUser)
        {
            _context = context;
            _appUser = appUser;
            _fileUploadService = fileUploadService;
        }

        // GET: Executives
        public async Task<IActionResult> ExecutiveIndex()
        {
            List<TblExecutive> model = await _context.TblExecutives.Where(x => x.IsActive != false).ToListAsync();
            return View(model);
        }

        // GET: Executives/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var tblExecutive = await _context.TblExecutives
        //        .FirstOrDefaultAsync(m => m.ExecutiveId == id);
        //    if (tblExecutive == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(tblExecutive);
        //}

        // GET: Executives/Create
        public async Task<IActionResult> CreateExecutive()
        {

            List<string> allUsers = new List<string>()
            {
                "Super admin" , "Executive"

            };
            ViewBag.allUser = allUsers;
            return View();
        }

        // POST: Executives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExecutive(TblExecutive tblExecutive)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ExistUser = true;
                var allUsers = await _context.TblUsers.Where(x => x.UserName == tblExecutive.Email).ToListAsync();
                if(allUsers.Count != 0)
                {
                    ViewBag.ExistUser = "Username - " + tblExecutive.Email + "already registered";
                    return View(tblExecutive);
                }


                tblExecutive.IsActive = true;
                tblExecutive.CreatedBy = HttpContext.Session.GetInt32("session_UserID");
                tblExecutive.CreatedDate = DateTime.Now;

                TblUser tblUser = new TblUser();
                tblUser.UserName = tblExecutive.Email;
                tblUser.UserPassword = tblExecutive.Password;
                tblUser.IsActive = true;
                tblUser.IsConfirmed = true;
                tblUser.UserRoleId = 4;
                if (tblExecutive.Usertype == "Super admin")
                {
                    tblUser.UserId = 1;
                }
                //else if(tblExecutive.Usertype == "Crew")
                //{
                //    tblUser.UserId = 2;
                //}
                //else if(tblExecutive.Usertype == "Company")
                //{
                //    tblUser.UserId = 3;
                //}
                else if (tblExecutive.Usertype == "Executive")
                {
                    tblUser.UserId = 4;
                }


                _context.Add(tblUser);
                await _context.SaveChangesAsync();


                var imagePath = "";
                //_CrewTrainingService.CreateCrewTraining(tblCrewTraining);
                if (tblExecutive.PhotoUpload != null)
                {
                    imagePath = await _fileUploadService.UploadImageExecutive(tblExecutive);
                    tblExecutive.Image = imagePath;
                }
                tblExecutive.UserId = tblUser.UserId;
                tblExecutive.UserRoleId = 4; //4=executive - user role
                _context.Add(tblExecutive);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ExecutiveIndex));
            }
            return View(tblExecutive);
        }

        // GET: Executives/Edit/5
        public async Task<IActionResult> EditExecutive(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblExecutive = await _context.TblExecutives.FindAsync(id);

            var user = await _context.TblUsers.FindAsync(id);
            tblExecutive.Password = user.UserPassword;

            if (tblExecutive == null)
            {
                return NotFound();
            }
            return View(tblExecutive);
        }

        // POST: Executives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExecutive(int id, /*[Bind("ExecutiveId,UserRoleId,UserId,ExFirstName,ExLastName,Designation,Image,Address,Phone1,Phone2,Email,IsActive,IsApproved,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")]*/ TblExecutive tblExecutive)
        {
            if (id != tblExecutive.ExecutiveId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tblExecutive.UpdatedBy = 1;
                    tblExecutive.UpdatedDate = DateTime.Now;
                    _context.Update(tblExecutive);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblExecutiveExists(tblExecutive.ExecutiveId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ExecutiveIndex));
            }
            return View(tblExecutive);
        }

        //GET: Directors/Delete/5
        public async Task<IActionResult> DeleteExecutive(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblExecutive = await _context.TblExecutives
                .FirstOrDefaultAsync(m => m.ExecutiveId == id);
            if (tblExecutive == null)
            {
                return NotFound();
            }
            else
            {
                await DeleteExecutive(tblExecutive.ExecutiveId);
            }

            return RedirectToAction(nameof(ExecutiveIndex));
        }

        // POST: Directors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExecutive(int id)
        {
            //var tblGalleryPhoto = await _context.TblDirectors.FindAsync(id);
            //_context.TblDirectors.Remove(tblGalleryPhoto);
            //await _context.SaveChangesAsync();


            var tblExecutive = await _context.TblExecutives.FindAsync(id);
            tblExecutive.IsActive = false;
            _context.Update(tblExecutive);
            await _context.SaveChangesAsync();



            return View(tblExecutive);
        }


        private bool TblExecutiveExists(int id)
        {
            return _context.TblExecutives.Any(e => e.ExecutiveId == id);
        }
    }
}

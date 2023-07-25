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
using Microsoft.EntityFrameworkCore.Internal;

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
            List<TblExecutive> tblExecutives = await (
                                     from e in _context.TblExecutives
                                     where e.IsActive != false
                                     join u in _context.TblUsers on e.UserId equals u.UserId
                                     select new TblExecutive
                                     {
                                         ExecutiveId = e.ExecutiveId,
                                         UserId = e.UserId,
                                         UserRoleId = e.UserRoleId,
                                         ExFirstName = e.ExFirstName,
                                         ExLastName = e.ExLastName,
                                         Designation = e.Designation,
                                         Image = e.Image,
                                         Address = e.Address,
                                         Phone1 = e.Phone1,
                                         Phone2 = e.Phone2,
                                         Email = e.Email,
                                         IsActive = e.IsActive,
                                         IsApproved = e.IsApproved,
                                         PhotoUpload = e.PhotoUpload,
                                         Usertype = e.Usertype,
                                         Password = u.UserPassword
                                     }
                                 ).ToListAsync();

            //string name = "Romana";

            //var query = tblExecutives.AsQueryable();

            //query = query.Where(a => a.ExFirstName.Contains(name));

            //tblExecutives = new List<TblExecutive>(query); 

            return View(tblExecutives);
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

                var allUsers = await _context.TblUsers.Where(x => x.UserName == tblExecutive.Email).ToListAsync();
                if (allUsers.Count != 0)
                {
                   
                    ViewData["ExistUser"] = "Username - " + tblExecutive.Email + " already registered";
                    List<string> Users = new List<string>()
                    {
                    "Super admin" , "Executive"

                     };
                    ViewBag.allUser = Users;
                    return View(tblExecutive);
                }


                tblExecutive.IsActive = true;
                tblExecutive.CreatedBy = HttpContext.Session.GetInt32("session_UserID");
                tblExecutive.CreatedDate = DateTime.Now;

                TblUser tblUser = new TblUser();
                tblUser.UserName = tblExecutive.Email.ToLower();
                tblUser.UserPassword = tblExecutive.Password;
                tblUser.IsActive = true;
                tblUser.IsConfirmed = true;
                //tblUser.UserRoleId = 4;

                tblUser.CreatedBy = HttpContext.Session.GetInt32("session_UserID");
                tblUser.CreatedDate = DateTime.Now;
                if (tblExecutive.Usertype == "Super admin")
                {
                    tblUser.UserRoleId = 1;
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
                    tblUser.UserRoleId = 4;
                }

                if(tblUser.UserRoleId == 0)
                {
                    tblUser.UserRoleId=4; //Executive
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
                tblExecutive.UserRoleId = tblUser.UserRoleId;
                tblExecutive.Email = tblExecutive.Email.ToLower();
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

            var user = await _context.TblUsers.FindAsync(tblExecutive.UserId);
            tblExecutive.Password = user.UserPassword;
            if (tblExecutive.UserRoleId == 1)
            {
                tblExecutive.Usertype = "Super admin";

            }
            else if (tblExecutive.UserRoleId == 4)
            {
                tblExecutive.Usertype = "Executive";

            }
            List<string> allUsers = new List<string>()
            {
                "Super admin" , "Executive"

            };
            ViewBag.allUser = allUsers;
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
        public async Task<IActionResult> EditExecutive(int id, TblExecutive tblExecutive)
        {
            if (id != tblExecutive.ExecutiveId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TblUser tblUser = await _context.TblUsers.FindAsync(tblExecutive.UserId);
                    tblUser.UserName = tblExecutive.Email.ToLower();
                    tblUser.UserPassword = tblExecutive.Password;
                    tblUser.UpdatedBy = HttpContext.Session.GetInt32("session_UserID");
                    tblUser.UpdatedDate = DateTime.Now;
                    if (tblExecutive.Usertype == "Super admin")
                    {
                        tblUser.UserRoleId = 1;
                    }
                    else if (tblExecutive.Usertype == "Executive")
                    {
                        tblUser.UserRoleId = 4;
                    }

                    _context.Update(tblUser);
                    await _context.SaveChangesAsync();


                    var imagePath = "";
                    //_CrewTrainingService.CreateCrewTraining(tblCrewTraining);
                    if (tblExecutive.PhotoUpload != null)
                    {
                        imagePath = await _fileUploadService.UploadImageExecutive(tblExecutive);
                        tblExecutive.Image = imagePath;
                    }


                    tblExecutive.UpdatedBy = HttpContext.Session.GetInt32("session_UserID");
                    tblExecutive.UpdatedDate = DateTime.Now;
                    tblExecutive.UserRoleId = tblUser.UserRoleId;

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

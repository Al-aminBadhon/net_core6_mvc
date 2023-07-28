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

namespace App.Home.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly MHDBContext _context;
        private readonly AppUser _appUser;

        public CompanyController(MHDBContext context, AppUser appUser)
        {
            _context = context;
            _appUser = appUser;
        }

        // GET: Company
        public async Task<IActionResult> CompanyIndex()
        {
            return View(await _context.TblCompanies.Where(x=> x.IsActive == true).ToListAsync());
        }

        // GET: Company/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var tblCompany = await _context.TblCompanies
        //        .FirstOrDefaultAsync(m => m.CompanyId == id);
        //    if (tblCompany == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(tblCompany);
        //}

        // GET: Company/Create
        [AllowAnonymous]
        public IActionResult RegisterCompany()
        {
            ViewBag.EmailExist = "";
            return View();
        }

        // POST: Company/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public async Task<IActionResult> RegisterCompany(TblCompany tblCompany)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var alreadyExistEmail = _context.TblCompanies.FirstOrDefault(x => x.Email == tblCompany.Email);
                    ViewBag.EmailExist = "";
                    if (alreadyExistEmail != null)
                    {
                        ViewBag.EmailExist = "This \""+ tblCompany.Email + "\" email address is already registered";
                        return View(tblCompany);
                        
                    }
                    tblCompany.IsActive = true;
                    tblCompany.CreatedBy = HttpContext.Session.GetInt32("session_UserID");
                    tblCompany.CreatedDate = DateTime.Now;

                    TblUser tblUser = new TblUser();
                    tblUser.UserName = tblCompany.Email ?? "";
                    tblUser.UserPassword = tblCompany.Password ?? "123456";
                    var userRoleID = _context.TblUserRoles.FirstOrDefault(x => x.UserRoleName == "Company").UserRoleId;
                    tblUser.UserRoleId = userRoleID;
                    tblUser.IsActive = true;
                    tblUser.IsConfirmed = true;
                    _context.Add(tblUser);
                    await _context.SaveChangesAsync();

                    tblCompany.UserId = tblUser.UserId;
                    tblCompany.UserRoleId = userRoleID;
                    tblCompany.IsActive = true;
                    tblCompany.IsAprroved = true;
                    _context.Add(tblCompany);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(CompanyIndex), new { id = tblCompany.CompanyId});

                }
                catch (Exception ex)
                {
                    var error = ex;
                    throw error;
                }
               
            }
            return View(tblCompany);
        }

        // GET: Company/Edit/5
        public async Task<IActionResult> EditCompany(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCompany = await _context.TblCompanies.FindAsync(id);
            if (tblCompany == null)
            {
                return NotFound();
            }
            return View(tblCompany);
        }

        // POST: Company/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCompany(int id, /*[Bind("ExecutiveId,UserRoleId,UserId,ExFirstName,ExLastName,Designation,Image,Address,Phone1,Phone2,Email,IsActive,IsApproved,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")]*/ TblCompany tblCompany)
        {
            if (id != tblCompany.CompanyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tblCompany.UpdatedBy = 1;
                    tblCompany.UpdatedDate = DateTime.Now;
                    _context.Update(tblCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCompanyExists(tblCompany.CompanyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CompanyIndex));
            }
            return View(tblCompany);
        }

        //GET: Directors/Delete/5
        public async Task<IActionResult> DeleteCompany(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCompany = await _context.TblCompanies
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (tblCompany == null)
            {
                return NotFound();
            }
            else
            {
                await DeleteCompany(tblCompany.CompanyId);
            }

            return RedirectToAction(nameof(CompanyIndex));
        }

        // POST: Directors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            //var tblGalleryPhoto = await _context.TblDirectors.FindAsync(id);
            //_context.TblDirectors.Remove(tblGalleryPhoto);
            //await _context.SaveChangesAsync();


            var tblCompany = await _context.TblCompanies.FindAsync(id);
            tblCompany.IsActive = false;
            _context.Update(tblCompany);
            await _context.SaveChangesAsync();



            return View(tblCompany);
        }


        private bool TblCompanyExists(int id)
        {
            return _context.TblCompanies.Any(e => e.CompanyId == id);
        }
    }
}

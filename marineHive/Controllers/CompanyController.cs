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
        public IActionResult CreateCompany()
        {
            return View();
        }

        // POST: Company/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCompany(TblCompany tblCompany)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tblCompany.IsActive = true;
                    tblCompany.CreatedBy = HttpContext.Session.GetInt32("session_UserID");
                    tblCompany.CreatedDate = DateTime.Now;

                    TblUser tblUser = new TblUser();
                    tblUser.UserName = tblCompany.Email ?? "";
                    tblUser.UserPassword = tblCompany.Password;
                    _context.Add(tblUser);
                    await _context.SaveChangesAsync();

                    tblCompany.UserId = tblUser.UserId;
                    tblCompany.UserRoleId = 3; //3=Company - user role
                    _context.Add(tblCompany);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(CompanyIndex));
                }
                catch (Exception ex)
                {
                    throw ex;
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

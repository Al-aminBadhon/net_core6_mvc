﻿using System;
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
    public class CrewsController : Controller
    {
        private readonly MHDBContext _context;
        private readonly AppUser _appUser;

        public CrewsController(MHDBContext context, AppUser appUser)
        {
            _context = context;
            _appUser = appUser;
        }

        // GET: Crews
        public async Task<IActionResult> CrewIndex()
        {
            return View(await _context.TblCrews.Where(x=> x.IsActive == true).ToListAsync());
        }

        // GET: Crews/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var tblCrew = await _context.TblCrews
        //        .FirstOrDefaultAsync(m => m.ExecutiveId == id);
        //    if (tblCrew == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(tblCrew);
        //}

        // GET: Crews/Create
        public IActionResult CreateCrew()
        {
            return View();
        }

        // POST: Crews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCrew(TblCrew tblCrew)
        {
            if (ModelState.IsValid)
            {
                
                tblCrew.IsActive = true;
                //tblCrew.CreatedBy = HttpContext.Session.GetInt32("session_UserID");
                //tblCrew.CreatedDate = DateTime.Now;

                TblUser tblUser = new TblUser();
                tblUser.UserName = tblCrew.Email;
                tblUser.UserPassword = tblCrew.Password;
                _context.Add(tblUser);
                await _context.SaveChangesAsync();

                tblCrew.UserId = tblUser.UserId;
                tblCrew.UserRoleId = 2; // 2 = Crew -> user role
                _context.Add(tblCrew);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CrewIndex));
            }
            return View(tblCrew);
        }

        // GET: Crews/Edit/5
        public async Task<IActionResult> EditCrew(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCrew = await _context.TblCrews.FindAsync(id);
            if (tblCrew == null)
            {
                return NotFound();
            }
            return View(tblCrew);
        }

        // POST: Crews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCrew(int id, /*[Bind("ExecutiveId,UserRoleId,UserId,ExFirstName,ExLastName,Designation,Image,Address,Phone1,Phone2,Email,IsActive,IsApproved,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")]*/ TblCrew tblCrew)
        {
            if (id != tblCrew.CrewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tblCrew.UpdatedBy = 1;
                    tblCrew.UpdatedDate = DateTime.Now;
                    _context.Update(tblCrew);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCrewExists(tblCrew.CrewId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CrewIndex));
            }
            return View(tblCrew);
        }

        //GET: Directors/Delete/5
        public async Task<IActionResult> DeleteCrew(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCrew = await _context.TblCrews
                .FirstOrDefaultAsync(m => m.CrewId == id);
            if (tblCrew == null)
            {
                return NotFound();
            }
            else
            {
                await DeleteCrew(tblCrew.CrewId);
            }

            return RedirectToAction(nameof(CrewIndex));
        }

        // POST: Directors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCrew(int id)
        {
            //var tblGalleryPhoto = await _context.TblDirectors.FindAsync(id);
            //_context.TblDirectors.Remove(tblGalleryPhoto);
            //await _context.SaveChangesAsync();


            var tblCrew = await _context.TblCrews.FindAsync(id);
            tblCrew.IsActive = false;
            _context.Update(tblCrew);
            await _context.SaveChangesAsync();



            return View(tblCrew);
        }


        private bool TblCrewExists(int id)
        {
            return _context.TblCrews.Any(e => e.CrewId == id);
        }
    }
}

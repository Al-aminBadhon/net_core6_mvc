﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.Data;
using App.DAL.Models;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using App.BLL.Services;
using App.BLL.ServiceContracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using App.Home.FileUploadService;
using Microsoft.AspNetCore.Authorization;

namespace App.Home.Controllers
{
    [Authorize]
    public class CrewManningController : Controller
    {
        private readonly MHDBContext _context;
        //private readonly ICrewManningService _CrewManningService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileUploadService _fileUploadService;


        public CrewManningController(/*ICrewManningService CrewManningService,*/ MHDBContext mHDBContext, IWebHostEnvironment webHostEnvironment, IFileUploadService fileUploadService)
        {
            //this._CrewManningService = CrewManningService;
            this._context = mHDBContext;
            this._fileUploadService = fileUploadService;
            this._webHostEnvironment = webHostEnvironment;


        }

        public async Task<IActionResult> CrewManningIndex()
        {
            TblCrewManning tblCrewMannings = await _context.TblCrewMannings.Where(x=> x.IsDeleted != true).FirstOrDefaultAsync();
            return View(tblCrewMannings);
        }

        // GET: CrewManning/EditCrewManning/5
        public async Task<IActionResult> EditCrewManning(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCrewManning = await _context.TblCrewMannings.FindAsync(id);
            if (tblCrewManning == null)
            {
                return NotFound();
            }
            return View(tblCrewManning);
        }

        // POST: Directors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCrewManning(int id, /*[Bind("ImageId,Name,Image,Flag,IsDelete,Details,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")]*/ TblCrewManning tblCrewManning)
        {
            if (id != tblCrewManning.CrewManningId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //using (IDbContextTransaction transaction = _context.Database.BeginTransaction()) {
                    //    transaction.Commit();
                    //    transaction.Rollback();
                    //}
                    
                    if (tblCrewManning.PhotoUpload != null)
                    {
                        if(tblCrewManning.Image != null)
                        {
                             _fileUploadService.DeleteImage(tblCrewManning.Image);
                        }
                        tblCrewManning.Image = await _fileUploadService.UploadImageCrewManning(tblCrewManning);
                         
                    }
                    //tblCrewManning = await _CrewManningService.UpdateCrewManningPhoto(tblCrewManning);
                    tblCrewManning.UpdatedBy = HttpContext.Session.GetInt32("session_UserID");
                    tblCrewManning.UpdatedDate = DateTime.Now;
                    _context.Update(tblCrewManning);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCrewManningsExists(tblCrewManning.CrewManningId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CrewManningIndex));
            }
            return View(tblCrewManning);
        }

        private bool TblCrewManningsExists(int id)
        {
            return _context.TblCrewMannings.Any(e => e.CrewManningId == id);
        }
    }
}

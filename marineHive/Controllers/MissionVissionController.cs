using System;
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
    public class MissionVissionController : Controller
    {
        private readonly MHDBContext _context;
        //private readonly IMissionVissionService _MissionVissionService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileUploadService _fileUploadService;


        public MissionVissionController(/*IMissionVissionService MissionVissionService,*/ MHDBContext mHDBContext, IWebHostEnvironment webHostEnvironment, IFileUploadService fileUploadService)
        {
            //this._MissionVissionService = MissionVissionService;
            this._context = mHDBContext;
            this._fileUploadService = fileUploadService;
            this._webHostEnvironment = webHostEnvironment;


        }

        public async Task<IActionResult> MissionVissionIndex()
        {
            TblMissionVission tblMissionVissions = await _context.TblMissionVissions.Where(x=> x.IsDeleted != true).FirstOrDefaultAsync();
            return View(tblMissionVissions);
        }

        // GET: MissionVission/EditMissionVission/5
        public async Task<IActionResult> EditMissionVission(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMissionVission = await _context.TblMissionVissions.FindAsync(id);
            if (tblMissionVission == null)
            {
                return NotFound();
            }
            return View(tblMissionVission);
        }

        // POST: Directors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMissionVission(int id, /*[Bind("ImageId,Name,Image,Flag,IsDelete,Details,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")]*/ TblMissionVission tblMissionVission)
        {
            if (id != tblMissionVission.MissionVissionId)
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
                    
                    if (tblMissionVission.PhotoUpload1 != null)
                    {
                        if(tblMissionVission.MissionImage != null)
                        {
                             _fileUploadService.DeleteImage(tblMissionVission.MissionImage);
                        }
                        tblMissionVission.MissionImage = await _fileUploadService.UploadImageMissionVission(tblMissionVission.PhotoUpload1);
                         
                    }
                    if (tblMissionVission.PhotoUpload2 != null)
                    {
                        if(tblMissionVission.VissionImage != null)
                        {
                             _fileUploadService.DeleteImage(tblMissionVission.VissionImage);
                        }
                        tblMissionVission.MissionImage = await _fileUploadService.UploadImageMissionVission(tblMissionVission.PhotoUpload2);
                         
                    }
                    //tblMissionVission = await _MissionVissionService.UpdateMissionVissionPhoto(tblMissionVission);
                    tblMissionVission.UpdatedBy = HttpContext.Session.GetInt32("session_UserID");
                    tblMissionVission.UpdatedDate = DateTime.Now;
                    _context.Update(tblMissionVission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblMissionVissionsExists(tblMissionVission.MissionVissionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MissionVissionIndex));
            }
            return View(tblMissionVission);
        }

        private bool TblMissionVissionsExists(int id)
        {
            return _context.TblMissionVissions.Any(e => e.MissionVissionId == id);
        }
    }
}

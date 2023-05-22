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
    public class WhyBdcrewController : Controller
    {
        private readonly MHDBContext _context;
        //private readonly IWhyBdcrewService _WhyBdcrewService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileUploadService _fileUploadService;


        public WhyBdcrewController(/*IWhyBdcrewService WhyBdcrewService,*/ MHDBContext mHDBContext, IWebHostEnvironment webHostEnvironment, IFileUploadService fileUploadService)
        {
            //this._WhyBdcrewService = WhyBdcrewService;
            this._context = mHDBContext;
            this._fileUploadService = fileUploadService;
            this._webHostEnvironment = webHostEnvironment;


        }

        public async Task<IActionResult> WhyBdcrewIndex()
        {
            TblWhyBdcrew tblWhyBdcrews = await _context.TblWhyBdcrews.Where(x=> x.IsDeleted != true).FirstOrDefaultAsync();
            return View(tblWhyBdcrews);
        }

        // GET: WhyBdcrew/EditWhyBdcrew/5
        public async Task<IActionResult> EditWhyBdcrew(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblWhyBdcrew = await _context.TblWhyBdcrews.FindAsync(id);
            if (tblWhyBdcrew == null)
            {
                return NotFound();
            }
            return View(tblWhyBdcrew);
        }

        // POST: Directors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWhyBdcrew(int id, /*[Bind("ImageId,Name,Image,Flag,IsDelete,Details,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")]*/ TblWhyBdcrew tblWhyBdcrew)
        {
            if (id != tblWhyBdcrew.WhyBdcrewId)
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
                    
                    if (tblWhyBdcrew.PhotoUpload != null)
                    {
                        if(tblWhyBdcrew.Image != null)
                        {
                             _fileUploadService.DeleteImage(tblWhyBdcrew.Image);
                        }
                        tblWhyBdcrew.Image = await _fileUploadService.UploadImageWhyBdcrew(tblWhyBdcrew);
                         
                    }
                    //tblWhyBdcrew = await _WhyBdcrewService.UpdateWhyBdcrewPhoto(tblWhyBdcrew);
                    tblWhyBdcrew.UpdatedBy = HttpContext.Session.GetInt32("session_UserID");
                    tblWhyBdcrew.UpdatedDate = DateTime.Now;
                    _context.Update(tblWhyBdcrew);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblWhyBdcrewsExists(tblWhyBdcrew.WhyBdcrewId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(WhyBdcrewIndex));
            }
            return View(tblWhyBdcrew);
        }

        private bool TblWhyBdcrewsExists(int id)
        {
            return _context.TblWhyBdcrews.Any(e => e.WhyBdcrewId == id);
        }
    }
}

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
    public class PortAgencyController : Controller
    {
        private readonly MHDBContext _context;
        //private readonly IPortAgencyService _PortAgencyService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileUploadService _fileUploadService;


        public PortAgencyController(/*IPortAgencyService PortAgencyService,*/ MHDBContext mHDBContext, IWebHostEnvironment webHostEnvironment, IFileUploadService fileUploadService)
        {
            //this._PortAgencyService = PortAgencyService;
            this._context = mHDBContext;
            this._fileUploadService = fileUploadService;
            this._webHostEnvironment = webHostEnvironment;


        }

        public async Task<IActionResult> PortAgencyIndex()
        {
            TblPortAgency tblPortAgencys = await _context.TblPortAgencies.Where(x=> x.IsDeleted != true).FirstOrDefaultAsync();
            return View(tblPortAgencys);
        }

        // GET: PortAgency/EditPortAgency/5
        public async Task<IActionResult> EditPortAgency(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPortAgency = await _context.TblPortAgencies.FindAsync(id);
            if (tblPortAgency == null)
            {
                return NotFound();
            }
            return View(tblPortAgency);
        }

        // POST: Directors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPortAgency(int id, /*[Bind("ImageId,Name,Image,Flag,IsDelete,Details,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")]*/ TblPortAgency tblPortAgency)
        {
            if (id != tblPortAgency.PortAgencyId)
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
                    
                    if (tblPortAgency.PhotoUpload != null)
                    {
                        if(tblPortAgency.Image != null)
                        {
                             _fileUploadService.DeleteImage(tblPortAgency.Image);
                        }
                        tblPortAgency.Image = await _fileUploadService.UploadImagePortAgency(tblPortAgency);
                         
                    }
                    //tblPortAgency = await _PortAgencyService.UpdatePortAgencyPhoto(tblPortAgency);
                    tblPortAgency.UpdatedBy = HttpContext.Session.GetInt32("session_UserID");
                    tblPortAgency.UpdatedDate = DateTime.Now;
                    _context.Update(tblPortAgency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblPortAgencysExists(tblPortAgency.PortAgencyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(PortAgencyIndex));
            }
            return View(tblPortAgency);
        }

        private bool TblPortAgencysExists(int id)
        {
            return _context.TblPortAgencies.Any(e => e.PortAgencyId == id);
        }
    }
}

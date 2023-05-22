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
    public class ShipManagementController : Controller
    {
        private readonly MHDBContext _context;
        //private readonly IShipManagementService _ShipManagementService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileUploadService _fileUploadService;


        public ShipManagementController(/*IShipManagementService ShipManagementService,*/ MHDBContext mHDBContext, IWebHostEnvironment webHostEnvironment, IFileUploadService fileUploadService)
        {
            //this._ShipManagementService = ShipManagementService;
            this._context = mHDBContext;
            this._fileUploadService = fileUploadService;
            this._webHostEnvironment = webHostEnvironment;


        }

        public async Task<IActionResult> ShipManagementIndex()
        {
            TblShipManagement tblShipManagements = await _context.TblShipManagements.Where(x=> x.IsDeleted != true).FirstOrDefaultAsync();
            return View(tblShipManagements);
        }

        // GET: ShipManagement/EditShipManagement/5
        public async Task<IActionResult> EditShipManagement(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblShipManagement = await _context.TblShipManagements.FindAsync(id);
            if (tblShipManagement == null)
            {
                return NotFound();
            }
            return View(tblShipManagement);
        }

        // POST: Directors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditShipManagement(int id, /*[Bind("ImageId,Name,Image,Flag,IsDelete,Details,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")]*/ TblShipManagement tblShipManagement)
        {
            if (id != tblShipManagement.ShipManagementId)
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
                    
                    if (tblShipManagement.PhotoUpload != null)
                    {
                        if(tblShipManagement.Image != null)
                        {
                             _fileUploadService.DeleteImage(tblShipManagement.Image);
                        }
                        tblShipManagement.Image = await _fileUploadService.UploadImageShipManagement(tblShipManagement);
                         
                    }
                    //tblShipManagement = await _ShipManagementService.UpdateShipManagementPhoto(tblShipManagement);
                    tblShipManagement.UpdatedBy = HttpContext.Session.GetInt32("session_UserID");
                    tblShipManagement.UpdatedDate = DateTime.Now;
                    _context.Update(tblShipManagement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblShipManagementsExists(tblShipManagement.ShipManagementId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ShipManagementIndex));
            }
            return View(tblShipManagement);
        }

        private bool TblShipManagementsExists(int id)
        {
            return _context.TblShipManagements.Any(e => e.ShipManagementId == id);
        }
    }
}

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

namespace App.Home.Controllers
{
    public class CrewTrainingController : Controller
    {
        private readonly MHDBContext _context;
        //private readonly ICrewTrainingService _CrewTrainingService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileUploadService _fileUploadService;


        public CrewTrainingController(/*ICrewTrainingService CrewTrainingService,*/ MHDBContext mHDBContext, IWebHostEnvironment webHostEnvironment, IFileUploadService fileUploadService)
        {
            //this._CrewTrainingService = CrewTrainingService;
            this._context = mHDBContext;
            this._fileUploadService = fileUploadService;


        }

        // GET: CrewTraining
        public async Task<IActionResult> CrewTrainingIndex()
        {
            List<TblCrewTraining> crewTraining = await _context.TblCrewTrainings.Where(a => a.IsDeleted != true).ToListAsync();
            return View(crewTraining);
        }

        // GET: CrewTraining/Create
        public IActionResult CreateCrewTraining()
        {
            return View();
        }

        // POST: CrewTraining/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCrewTraining(/*[Bind("DirectorId,DirectorName,Designation,CompanyPost,Image,Details,FacebookLink,TwitterLink,LinkedInLink,IsDeleted,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")]*/ TblCrewTraining tblCrewTraining)
        {
            if (ModelState.IsValid)
            {
                var imagePath = "";
                //_CrewTrainingService.CreateCrewTraining(tblCrewTraining);
                if (tblCrewTraining.PhotoUpload != null)
                {
                    imagePath = await _fileUploadService.UploadImageCrewTraining(tblCrewTraining);
                    tblCrewTraining.Image = imagePath;
                }
                _context.Add(tblCrewTraining);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CrewTrainingIndex));
            }
            return View(tblCrewTraining);
        }

        // GET: CrewTraining/Edit/5
        public async Task<IActionResult> EditCrewTraining(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCrewTraining = await _context.TblCrewTrainings.FindAsync(id);
            if (tblCrewTraining == null)
            {
                return NotFound();
            }
            return View(tblCrewTraining);
        }

        // POST: CrewTraining/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCrewTraining(int id,  TblCrewTraining tblCrewTraining/*, IFormCollection formValues*/)
        {
            if (id != tblCrewTraining.CrewTrainingId)
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
                    var imagePath = "";
                    if (tblCrewTraining.PhotoUpload != null)
                    {
                        imagePath = await _fileUploadService.UploadImageCrewTraining(tblCrewTraining);
                        tblCrewTraining.Image = imagePath;
                        //tblCrewTraining = await _CrewTrainingService.UpdateCrewTraining(tblCrewTraining);
                    }
                        _context.Update(tblCrewTraining);
                        await _context.SaveChangesAsync();

                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCrewTrainingExists(tblCrewTraining.CrewTrainingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CrewTrainingIndex));
            }
            return View(tblCrewTraining);
        }

        // GET: CrewTraining/Delete/5
        public async Task<IActionResult> CrewTrainingDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCrewTraining = await _context.TblCrewTrainings
                .FirstOrDefaultAsync(m => m.CrewTrainingId == id);
            if (tblCrewTraining == null)
            {
                return NotFound();
            }
            else
            {
                await CrewTrainingDelete(tblCrewTraining.CrewTrainingId);
            }

            return RedirectToAction(nameof(CrewTrainingIndex));

        }

        // POST: CrewTraining/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrewTrainingDelete(int id)
        {
            var tblCrewTraining = await _context.TblCrewTrainings.FindAsync(id);
            tblCrewTraining.IsDeleted = true;
            _context.Update(tblCrewTraining);
            await _context.SaveChangesAsync();
            return View(tblCrewTraining);

        }

        private bool TblCrewTrainingExists(int id)
        {
            return _context.TblCrewTrainings.Any(e => e.CrewTrainingId == id);
        }
    }
}

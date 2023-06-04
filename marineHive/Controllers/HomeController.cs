using App.BLL.ServiceContracts;
using App.DAL.Data;
using App.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Core_MVC_Bootstrap.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDirectorsService _directorsService;
        private readonly IGalleryService _galleryService;
        private readonly MHDBContext _context;


        public HomeController(ILogger<HomeController> logger, IDirectorsService directorsService, MHDBContext context)
        {
            _logger = logger;
            this._directorsService = directorsService;
            this._context = context;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult CoreValues()
        {
            return View();
        }
        public async Task<IActionResult> MissionVision()
        {
            var model = await _context.TblMissionVissions.Where(x => x.IsDeleted != true).FirstOrDefaultAsync();
            return View(model);
        }
        
        public IActionResult AboutUs()
        {
            return View();
        }
        
        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult PrivacyStatement()
        {
            return View();
        }
        public async Task<IActionResult> WhyBDCrew()
        {
            var model = await _context.TblWhyBdcrews.Where(x => x.IsDeleted != true).FirstOrDefaultAsync();

            return View(model);
        }
        public async Task<IActionResult> CrewManning()
        {
            var model = await _context.TblCrewMannings.Where(x => x.IsDeleted != true).FirstOrDefaultAsync();

            return View();
        }
        public async Task<IActionResult> PortAgency()
        {
            var model = await _context.TblPortAgencies.Where(x => x.IsDeleted != true).FirstOrDefaultAsync();

            return View();
        }
        public IActionResult TechServices()
        {
            return View();
        }

        public async Task<IActionResult> CrewTraining()
        {
            List<TblCrewTraining> model = await _context.TblCrewTrainings.Where(x => x.IsDeleted != true).ToListAsync();
            return View(model);
        }
        public async Task<IActionResult> BoardOfDirectors()
        {
            List<TblDirector> listDirectors =  await _directorsService.GetAllDirectors();

            return View(listDirectors);
        }
        public  async Task<IActionResult> ShipManagement()
        {
            TblShipManagement model = await _context.TblShipManagements.Where(x => x.IsDeleted != true).FirstOrDefaultAsync();

            return View();
        }

        public  IActionResult Gallery()
        {
            List<TblGalleryPhoto> photos = new List<TblGalleryPhoto>();
            photos =  _context.TblGalleryPhotos.Where(x => x.IsDelete == false).ToList();
            return View(photos);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

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
using HtmlAgilityPack;

namespace App.Home.Controllers
{
    [Authorize]
    public class CrewController : Controller
    {
        private readonly MHDBContext _context;
        private readonly AppUser _appUser;

        public CrewController(MHDBContext context, AppUser appUser)
        {
            _context = context;
            _appUser = appUser;
        }

        // GET: Crew
        public async Task<IActionResult> CrewIndex()
        {
            return View(await _context.TblCrews.Where(x => x.IsActive == true).ToListAsync());
        }
        public async Task<IActionResult> CrewDashboard()
        {
            var currentCrewID = HttpContext.Session.GetInt32("session_UserID");
            return View(await _context.TblCrews.FirstOrDefaultAsync(x => x.IsActive == true && x.UserId == currentCrewID));
        }

        // GET: Crew/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var tblCrew = await _context.TblCompanies
        //        .FirstOrDefaultAsync(m => m.CrewId == id);
        //    if (tblCrew == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(tblCrew);
        //}

        // GET: Crew/Create
        [AllowAnonymous]
        [HttpGet]
        public IActionResult CDCCheck()
        {
            ViewBag.CDCNotMatch = "";
            ViewBag.AlreadyReg = "";
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CDCCheck(TblCrew model)
        {
           
           var alreadyRegCDC = _context.TblCrews.FirstOrDefault(x => x.IsActive != false && x.Cdcnumber == model.Cdcnumber);
            if(alreadyRegCDC == null)
            {
                var urlLast = model.Cdcnumber.ToUpper().Replace("/", "%2F");
                var web = new HtmlWeb();
                var url = "https://erp.gso.gov.bd/cdc-view?cdc=" + urlLast;
                var htmlDoc = web.Load(url);
                var node = htmlDoc.DocumentNode.SelectSingleNode("//table[@id=\"w0\"]");
                //var status = htmlDoc.DocumentNode.SelectSingleNode("//span[@class=\"status-active\"]").InnerText??"";

                if (node != null)
                {
                    HtmlNode row = node.SelectSingleNode("tr[position()=1]");
                    if (row != null)
                    {
                        string desiredThValue = "CDC NUMBER";
                        HtmlNode thNode = row.SelectSingleNode($"th[text()='{desiredThValue}']");
                        var cdc = row.SelectSingleNode("td").InnerText;

                        if(cdc.ToLower() == model.Cdcnumber.ToLower())
                        {
                            return RedirectToAction(nameof(RegisterCrew), new { model.Cdcnumber });
                        }
                        else
                        {
                            
                        }
                    }

                    
                }
                else
                {
                    ViewBag.Msg = "This cdc number is not registered in the Bangladeshi Govt. website. If you have different CDC,  please submit other CDC number";
                    return View(model);
                }
            }
            ViewBag.Msg = "This CDC Number " + model.Cdcnumber + " is already registered to our website.";
            return View(model);

        }
        public List<HtmlNode> GetAllChildNodes(HtmlNode Node)
        {
            List<HtmlNode> childNodes = new List<HtmlNode>();

            // Get all the child nodes of the table node
            foreach (HtmlNode childNode in Node.ChildNodes)
            {
                childNodes.Add(childNode);

                // Recursively get the child nodes of the childNode if it has child nodes
                if (childNode.HasChildNodes)
                {
                    List<HtmlNode> nestedChildNodes = GetAllChildNodes(childNode);
                    childNodes.AddRange(nestedChildNodes);
                }
            }

            return childNodes;
        }
        [AllowAnonymous]
        public IActionResult RegisterCrew(string cdcNumber)
        {
            TblCrew tblcrew = new TblCrew()
            {
                Cdcnumber = cdcNumber,
            };
            return View(tblcrew);
        }
       
        // POST: Crew/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public async Task<IActionResult> RegisterCrew(TblCrew tblCrew)
        {
            if (ModelState.IsValid)
            {
                try
                {


                }
                catch (Exception ex)
                {
                    var error = ex;
                    throw error;
                }

            }
            return View(tblCrew);
        }

        // GET: Crew/Edit/5
        public async Task<IActionResult> EditCrew(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCrew = await _context.TblCompanies.FindAsync(id);
            if (tblCrew == null)
            {
                return NotFound();
            }
            return View(tblCrew);
        }

        // POST: Crew/Edit/5
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

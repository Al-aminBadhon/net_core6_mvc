using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.Data;
using App.DAL.Models;
using System.Security.Claims;
//using DotNetOpenAuth.InfoCard;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using App.DAL.Utilities;

namespace App.Home.Controllers
{
    public class LoginController : Controller
    {
        private readonly MHDBContext _context;
        private readonly AppUser _appUser;

        public LoginController(MHDBContext context, AppUser appUser)
        {
            _context = context;
            _appUser = appUser;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
            var mHDBContext = _context.TblUsers.Include(t => t.UserRole);
            return View(await mHDBContext.ToListAsync());
        }


        // GET: Login/Create
        public IActionResult Create()
        {
            ViewData["UserRoleId"] = new SelectList(_context.TblUserRoles, "UserRoleId", "UserRoleName");
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserRoleId,UserName,UserPassword,IsActive,IsConfirmed,IsDisabled,LastLoginTime,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] TblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserRoleId"] = new SelectList(_context.TblUserRoles, "UserRoleId", "UserRoleName", tblUser.UserRoleId);
            return View(tblUser);
        }

        public IActionResult Login()
        {
            ViewData["UserRoleId"] = new SelectList(_context.TblUserRoles, "UserRoleId", "UserRoleName");
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(TblUser tblUser, string returnUrl)
        {
            //if (ModelState.IsValid)
            //{
                

                var data = _context.TblUsers.Where(e => e.UserName == tblUser.UserName && tblUser.UserPassword == e.UserPassword).FirstOrDefault();
                if (data != null)
                {
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, tblUser.UserName) },
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    //HttpContext.Session.SetString("Username", "");

                    if(data.UserRoleId == 1)
                    {
                        var Fullname = _context.TblExecutives.Where(e => e.UserId == data.UserId).FirstOrDefault();

                        //_appUser.UserFirstName = Fullname.ExFirstName;
                        HttpContext.Session.SetString("session_UserFirstName", Fullname.ExFirstName);
                    }
                    //_appUser.UserRoleID = data.UserRoleId;
                    //_appUser.UserID = data.UserId;
                    //_appUser.UserName = tblUser.UserName;

                    HttpContext.Session.SetInt32("session_UserRoleID", data.UserRoleId);
                    HttpContext.Session.SetInt32("session_UserID", data.UserId);
                    HttpContext.Session.SetString("session_UserName", tblUser.UserName);


                    var name = HttpContext.Session.GetString("session_UserFirstName");

                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("DashboardIndex", "DashBoard");
                }
                else
                {
                    ViewBag.ErrorMsg = "Username or password is incorrect";
                    return View(tblUser);
                }
            //}
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            //ViewData["UserRoleId"] = new SelectList(_context.TblUserRole, "UserRoleId", "UserRoleName", tblUser.UserRoleId);
            return View(tblUser);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var storedCookies = Request.Cookies.Keys;
            foreach (var cookie in storedCookies)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction(nameof(Login));
        }

        // GET: Login/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblUser = await _context.TblUsers.FindAsync(id);
            if (tblUser == null)
            {
                return NotFound();
            }
            ViewData["UserRoleId"] = new SelectList(_context.TblUserRoles, "UserRoleId", "UserRoleName", tblUser.UserRoleId);
            return View(tblUser);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserRoleId,UserName,UserPassword,IsActive,IsConfirmed,IsDisabled,LastLoginTime,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] TblUser tblUser)
        {
            if (id != tblUser.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblUserExists(tblUser.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserRoleId"] = new SelectList(_context.TblUserRoles, "UserRoleId", "UserRoleName", tblUser.UserRoleId);
            return View(tblUser);
        }

        private bool TblUserExists(int id)
        {
            return _context.TblUsers.Any(e => e.UserId == id);
        }
    }
}

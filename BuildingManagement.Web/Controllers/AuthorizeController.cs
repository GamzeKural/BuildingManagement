using AutoMapper;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using BuildingManagement.Web.Business.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace BuildingManagement.Web.Controllers
{
    public class AuthorizeController : Controller
    {
        private readonly IUserService userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizeController(IUserService userService,IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            this.userService = userService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Authenticate(AuthorizeDto user)
        {
            if (ModelState.IsValid)
            {
                var token = userService.Authenticate(user);

                if (!string.IsNullOrEmpty(token.Result.VerifiedToken))
                {
                    var authUser = userService.GetAllUsers().Result.Resource.FirstOrDefault(x => (x.UserName == user.UserName || x.Mail == user.UserName) && x.Password == user.Password);
                    var roleName = userService.GetRole(authUser.RoleId).Result.Resource.Name;

                    _httpContextAccessor.HttpContext.Session.SetString("UserName", authUser.UserName);
                    _httpContextAccessor.HttpContext.Session.SetString("Token", token.Result.VerifiedToken);
                    _httpContextAccessor.HttpContext.Session.SetString("Role", roleName);
                    _httpContextAccessor.HttpContext.Session.SetInt32("UserId", authUser.Id);

                    return RedirectToAction("Index", "Dueses", null);
                }
                else
                    return RedirectToAction("Login");
            }

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }

}

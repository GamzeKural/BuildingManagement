using AutoMapper;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using BuildingManagement.Web.Business.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BuildingManagement.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private static Random random = new Random();

        public string token = string.Empty;

        public UsersController(IUserService userService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;

            token = httpContextAccessor.HttpContext.Session.GetString("Token");
        }

        // GET: Users
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Authorize", null);
            }

            ViewBag.Role = httpContextAccessor.HttpContext.Session.GetString("Role");

            var userDtos = userService.GetAllUsers().Result.Resource;

            var users = mapper.Map<List<User>>(userDtos);

            var roles = mapper.Map<List<Role>>(userService.GetAllRoles().Result.Resource);

            foreach (var user in users)
            {
                var role = roles.FirstOrDefault(x => x.Id == user.RoleId);

                user.Role = role;
            }

            return View(users);
        }

        // GET: Users/Details/5
        public IActionResult Details(int id)
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Authorize", null);
            }
            ViewBag.Role = httpContextAccessor.HttpContext.Session.GetString("Role");

            if (id == null)
            {
                return BadRequest(HttpStatusCode.BadRequest);
            }

            User user = mapper.Map<User>(userService.GetUser(id).Result.Resource);


            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Authorize", null);
            }
            ViewBag.Role = httpContextAccessor.HttpContext.Session.GetString("Role");

            var roles = mapper.Map<IEnumerable<Role>>(userService.GetAllRoles().Result.Resource);

            ViewBag.RoleId = new SelectList(roles, "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            var userDto = mapper.Map<UserDto>(user);

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var newPassword = new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            userDto.Password = newPassword;

            userService.AddUser(userDto);

            return RedirectToAction("Index");
        }

        // GET: Users/Edit/5
        public IActionResult Edit(int id)
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Authorize", null);
            }
            ViewBag.Role = httpContextAccessor.HttpContext.Session.GetString("Role");

            if (id == null)
            {
                return BadRequest(HttpStatusCode.BadRequest);
            }

            User user = mapper.Map<User>(userService.GetUser(id).Result.Resource);

            if (user == null)
            {
                return NotFound();
            }

            var roles = mapper.Map<IEnumerable<Role>>(userService.GetAllRoles().Result.Resource);

            ViewBag.RoleId = new SelectList(roles, "Id", "Name", user.RoleId);

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {
            var userDto = mapper.Map<UserDto>(user);

            if (ModelState.IsValid)
            {
                userService.UpdateUser(userDto);
                return RedirectToAction("Index");
            }

            var roles = mapper.Map<IEnumerable<Role>>(userService.GetAllRoles().Result.Resource);

            ViewBag.RoleId = new SelectList(roles, "Id", "Name", user.RoleId);

            return View(user);
        }

        public IActionResult Delete(int id)
        {
            var result = userService.RemoveUser(id);

            return RedirectToAction("Index");
        }
    }
}

using AutoMapper;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using BuildingManagement.Web.Business.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace BuildingManagement.Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public string token = string.Empty;

        public RolesController(IUserService userService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;

            token = httpContextAccessor.HttpContext.Session.GetString("Token");
        }

        // GET: Roles
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Authorize", null);
            }

            ViewBag.Role = httpContextAccessor.HttpContext.Session.GetString("Role");

            var roleDtos = userService.GetAllRoles().Result.Resource;

            var roles = mapper.Map<List<Role>>(roleDtos);

            return View(roles);
        }

        // GET: Roles/Details/5
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

            Role role = mapper.Map<Role>(userService.GetRole(id).Result.Resource);

            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Authorize", null);
            }
            ViewBag.Role = httpContextAccessor.HttpContext.Session.GetString("Role");

            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Role role)
        {
            var roleDto = mapper.Map<RoleDto>(role);

            if (ModelState.IsValid)
            {
                userService.AddRole(roleDto);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Roles/Edit/5
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

            Role role = mapper.Map<Role>(userService.GetRole(id).Result.Resource);

            if ( role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Role role)
        {
            var roleDto = mapper.Map<RoleDto>(role);

            if (ModelState.IsValid)
            {
                userService.UpdateRole(roleDto);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        public IActionResult Delete(int id)
        {
            var result = userService.RemoveRole(id);

            return RedirectToAction("Index");
        }
    }
}

using AutoMapper;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using BuildingManagement.Web.Business.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BuildingManagement.Web.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly IApartmentService apartmentService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public string token = string.Empty;

        public ApartmentsController(IApartmentService apartmentService, IUserService userService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.apartmentService = apartmentService;
            this.userService = userService;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;

            token = httpContextAccessor.HttpContext.Session.GetString("Token");
        }

        // GET: Apartments
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Authorize", null);
            }

            ViewBag.Role = httpContextAccessor.HttpContext.Session.GetString("Role");

            var apartmentDtos = apartmentService.GetAllApartments().Result.Resource;

            var apartments = mapper.Map<List<Apartment>>(apartmentDtos);

            var users = mapper.Map<List<User>>(userService.GetAllUsers().Result.Resource);

            foreach (var apartment in apartments)
            {
                var user = users.FirstOrDefault(x => x.Id == apartment.UserId);

                apartment.User = user;
            }

            return View(apartments);
        }

        // GET: Apartments/Details/5
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

            Apartment apartment = mapper.Map<Apartment>(apartmentService.GetApartment(id).Result.Resource);


            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // GET: Apartments/Create
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Authorize", null);
            }

            ViewBag.Role = httpContextAccessor.HttpContext.Session.GetString("Role");

            var users = mapper.Map<IEnumerable<User>>(userService.GetAllUsers().Result.Resource);

            ViewBag.UserId = new SelectList(users, "Id", "UserName");
            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Apartment apartment)
        {
            var apartmentDto = mapper.Map<ApartmentDto>(apartment);

            apartment.ApartmentInfo = string.Empty;
            apartmentDto.ApartmentInfo = string.Empty;

            apartmentService.AddApartment(apartmentDto);

            return RedirectToAction("Index");
        }

        // GET: Apartments/Edit/5
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

            Apartment apartment = mapper.Map<Apartment>(apartmentService.GetApartment(id).Result.Resource);

            if (apartment == null)
            {
                return NotFound();
            }

            var users = mapper.Map<IEnumerable<User>>(userService.GetAllUsers().Result.Resource);

            ViewBag.UserId = new SelectList(users, "Id", "UserName", apartment.UserId);

            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Apartment apartment)
        {
            var apartmentDto = mapper.Map<ApartmentDto>(apartment);

            if (ModelState.IsValid)
            {
                apartmentService.UpdateApartment(apartmentDto);
                return RedirectToAction("Index");
            }

            var users = mapper.Map<IEnumerable<User>>(userService.GetAllUsers().Result.Resource);

            ViewBag.UserId = new SelectList(users, "Id", "UserName", apartment.UserId);

            return View(apartment);
        }

        public IActionResult Delete(int id)
        {
            var result = apartmentService.RemoveApartment(id);

            return RedirectToAction("Index");
        }
    }
}

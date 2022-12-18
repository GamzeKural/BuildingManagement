using AutoMapper;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using BuildingManagement.Web.Business.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BuildingManagement.Web.Controllers
{
    public class DuesesController : Controller
    {
        private readonly IDuesService duesService;
        private readonly IApartmentService apartmentService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public string token = string.Empty;

        public DuesesController(IDuesService duesService, IApartmentService apartmentService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            this.duesService = duesService;
            this.apartmentService = apartmentService;
            this.userService = userService;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;

            token = httpContextAccessor.HttpContext.Session.GetString("Token");
        }

        // GET: Dues
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Authorize", null);
            }

            var str = HttpContext.Session.GetString("Role");

            ViewBag.Role = httpContextAccessor.HttpContext.Session.GetString("Role");
            var userId = httpContextAccessor.HttpContext.Session.GetInt32("UserId");

            var duesDtos = duesService.GetAllDueses().Result.Resource;

            var dueses = mapper.Map<List<Dues>>(duesDtos);

            var apartments = mapper.Map<List<Apartment>>(apartmentService.GetAllApartments().Result.Resource);

            var users = mapper.Map<IEnumerable<User>>(userService.GetAllUsers().Result.Resource);

            foreach (var dues in dueses)
            {
                var apartment = apartments.FirstOrDefault(x => x.Id == dues.ApartmentId);

                dues.Apartment = apartment;

                if (apartment != null)
                    apartment.User = users.FirstOrDefault(x => x.Id == apartment.UserId);
            }

            if (!(ViewBag.Role == "Admin"))
                dueses = dueses.Where(x => x.Apartment.UserId == userId).ToList();

            return View(dueses);
        }

        public IActionResult Unpaid()
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Authorize", null);
            }

            var str = HttpContext.Session.GetString("Role");

            ViewBag.Role = httpContextAccessor.HttpContext.Session.GetString("Role");
            var userId = httpContextAccessor.HttpContext.Session.GetInt32("UserId");

            var duesDtos = duesService.GetAllDueses().Result.Resource;

            var dueses = mapper.Map<List<Dues>>(duesDtos);

            var apartments = mapper.Map<List<Apartment>>(apartmentService.GetAllApartments().Result.Resource);

            var users = mapper.Map<IEnumerable<User>>(userService.GetAllUsers().Result.Resource);

            foreach (var dues in dueses)
            {
                var apartment = apartments.FirstOrDefault(x => x.Id == dues.ApartmentId);

                dues.Apartment = apartment;

                if (apartment != null)
                    apartment.User = users.FirstOrDefault(x => x.Id == apartment.UserId);
            }

            if (!(ViewBag.Role == "Admin"))
                dueses = dueses.Where(x => x.Apartment.UserId == userId && x.IsPaid == false).ToList();
            else
            {
                dueses = dueses.Where(x => x.IsPaid == false).ToList();
            }

            return View(dueses);
        }

        // GET: Dues/Details/5
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

            Dues dues = mapper.Map<Dues>(duesService.GetDues(id).Result.Resource);


            if (dues == null)
            {
                return NotFound();
            }

            return View(dues);
        }

        // GET: Dues/Create
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Authorize", null);
            }
            ViewBag.Role = httpContextAccessor.HttpContext.Session.GetString("Role");

            var apartments = mapper.Map<IEnumerable<Apartment>>(apartmentService.GetAllApartments().Result.Resource);

            var users = mapper.Map<IEnumerable<User>>(userService.GetAllUsers().Result.Resource);

            foreach (var item in apartments)
            {
                item.User = users.FirstOrDefault(x => x.Id == item.UserId);
            }

            ViewBag.ApartmentId = new SelectList(apartments, "Id", "ApartmentInfo");

            return View();
        }

        // POST: Dues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Dues dues)
        {
            var duesDto = mapper.Map<DuesDto>(dues);

            if (ModelState.IsValid)
            {
                duesService.AddDues(duesDto);
                return RedirectToAction("Index");
            }

            var apartments = mapper.Map<IEnumerable<Apartment>>(apartmentService.GetAllApartments().Result.Resource);

            ViewBag.ApartmentId = new SelectList(apartments, "Id", "UserId", dues.ApartmentId);
            return View(dues);
        }

        // GET: Dues/Edit/5
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

            Dues dues = mapper.Map<Dues>(duesService.GetDues(id).Result.Resource);

            if (dues == null)
            {
                return NotFound();
            }

            var apartments = mapper.Map<IEnumerable<Apartment>>(apartmentService.GetAllApartments().Result.Resource);

            ViewBag.ApartmentId = new SelectList(apartments, "Id", "UserId", dues.ApartmentId);

            return View(dues);
        }

        // POST: Dues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Dues dues)
        {
            var duesDto = mapper.Map<DuesDto>(dues);

            if (ModelState.IsValid)
            {
                duesService.UpdateDues(duesDto);
                return RedirectToAction("Index");
            }

            var apartments = mapper.Map<IEnumerable<Apartment>>(apartmentService.GetAllApartments().Result.Resource);

            ViewBag.ApartmentId = new SelectList(apartments, "Id", "UserId", dues.ApartmentId);

            return View(dues);
        }

        public IActionResult Pay(int id)
        {
            var duesDto = duesService.GetDues(id).Result.Resource;

            if (duesDto.IsPaid == false)
            {
                duesDto.IsPaid = true;
                duesDto.PaidDate = DateTime.Now;

                var result = duesService.UpdateDues(duesDto).Result;
            }

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            var result = duesService.RemoveDues(id);

            return RedirectToAction("Index");
        }
    }
}

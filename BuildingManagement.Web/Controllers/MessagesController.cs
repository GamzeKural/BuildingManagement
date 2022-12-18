using AutoMapper;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using BuildingManagement.Web.Business.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BuildingManagement.Web.Controllers
{
    public class MessagesController : Controller
    {
        private readonly IMessageService messageService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public string token = string.Empty;

        public MessagesController(IMessageService messageService, IUserService userService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.messageService = messageService;
            this.userService = userService;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;

            token = httpContextAccessor.HttpContext.Session.GetString("Token");
        }

        // GET: Messages
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Authorize", null);
            }

            ViewBag.Role = httpContextAccessor.HttpContext.Session.GetString("Role");

            var userId = httpContextAccessor.HttpContext.Session.GetInt32("UserId");

            var messageDtos = messageService.GetAllMessages().Result.Resource;

            var messages = mapper.Map<List<Message>>(messageDtos);

            var users = mapper.Map<List<User>>(userService.GetAllUsers().Result.Resource);

            foreach (var message in messages)
            {
                var senderUser = users.FirstOrDefault(x => x.Id == message.SenderId);
                var receiverUser = users.FirstOrDefault(x => x.Id == message.ReceiverId);

                message.SenderUser = senderUser;
                message.ReceiverUser = receiverUser;
            }

            if (!(ViewBag.Role == "Admin"))
                messages = messages.Where(x => x.ReceiverId == userId).ToList();

            return View(messages);
        }

        // GET: Messages/Details/5
        public IActionResult Details(int id)
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Authorize", null);
            }

            ViewBag.Role = httpContextAccessor.HttpContext.Session.GetString("Role");
            var userId = httpContextAccessor.HttpContext.Session.GetInt32("UserId");

            if (id == null)
            {
                return BadRequest(HttpStatusCode.BadRequest);
            }

            Message message = mapper.Map<Message>(messageService.GetMessage(id).Result.Resource);

            if(message.ReceiverId == (userId ?? 0))
            {
                message.IsRead = true;

                var messageDto = mapper.Map<MessageDto>(message);

                messageService.UpdateMessage(messageDto);
            }

            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Authorize", null);
            }

            ViewBag.Role = httpContextAccessor.HttpContext.Session.GetString("Role");

            var users = mapper.Map<IEnumerable<User>>(userService.GetAllUsers().Result.Resource);

            ViewBag.SenderId = new SelectList(users, "Id", "UserName");
            ViewBag.ReceiverId = new SelectList(users, "Id", "UserName");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Message message)
        {
            var messageDto = mapper.Map<MessageDto>(message);

            int? receiverId = userService.GetAllUsers().Result.Resource.FirstOrDefault(x => x.RoleId == 1).Id;
            int? senderId = httpContextAccessor.HttpContext.Session.GetInt32("UserId");

            ViewBag.Role = httpContextAccessor.HttpContext.Session.GetString("Role");

            if (!(ViewBag.Role == "Admin"))
            {
                messageDto.SenderId = senderId ?? 0;
                messageDto.ReceiverId = receiverId ?? 0;
            }
            else
            {
                messageDto.SenderId = senderId ?? 0;
            }

            messageService.AddMessage(messageDto);
            return RedirectToAction("Index");
        }

        // GET: Messages/Edit/5
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

            Message message = mapper.Map<Message>(messageService.GetMessage(id).Result.Resource);

            if (message == null)
            {
                return NotFound();
            }

            var users = mapper.Map<IEnumerable<User>>(userService.GetAllUsers().Result.Resource);

            ViewBag.SenderId = new SelectList(users, "Id", "UserName", message.SenderId);
            ViewBag.ReceiverId = new SelectList(users, "Id", "UserName", message.ReceiverId);

            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Message message)
        {
            var messageDto = mapper.Map<MessageDto>(message);

            if (ModelState.IsValid)
            {
                messageService.UpdateMessage(messageDto);
                return RedirectToAction("Index");
            }

            var users = mapper.Map<IEnumerable<User>>(userService.GetAllUsers().Result.Resource);

            ViewBag.SenderId = new SelectList(users, "Id", "UserName", message.SenderId);
            ViewBag.ReceiverId = new SelectList(users, "Id", "UserName", message.ReceiverId);

            return View(message);
        }

        public IActionResult Delete(int id)
        {
            var result = messageService.RemoveMessage(id);

            return RedirectToAction("Index");
        }
    }
}

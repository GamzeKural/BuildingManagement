using BuildigManagement.Business.Abstracts;
using BuildigManagement.Business.Utils;
using BuildingManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace BuildingManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpGet("GetAllMessages")]
        public ActionResult<OperationResponse<List<MessageDto>>> GetAllMessages()
        {
            try
            {
                var result = messageService.GetAllMessages();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        [HttpGet("GetMessage")]
        public ActionResult<OperationResponse<MessageDto>> GetMessage(int id)
        {
            try
            {
                var result = messageService.GetMessage(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        [HttpPost("AddMessage")]
        public ActionResult<OperationResponse<MessageDto>> AddMessage(MessageDto messageDto)
        {
            try
            {
                var result = messageService.AddMessage(messageDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        
        [HttpPut("UpdateMessage")]
        [Authorize]
        public ActionResult<OperationResponse<MessageDto>> UpdateMessage(MessageDto messageDto)
        {
            try
            {
                var result = messageService.UpdateMessage(messageDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }

        [HttpDelete("RemoveMessage")]
        [Authorize]
        public ActionResult<OperationResponse<MessageDto>> RemoveMessage(int id)
        {
            try
            {
                var result = messageService.RemoveMessage(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem("Data getirilirken hata oluştu:" + ex.Message);
            }
        }
    }
}

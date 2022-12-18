using BuildigManagement.Business.Utils;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using BuildingManagement.Web.Business.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildingManagement.Web.Business.Services.Concretes
{
    public class MessageService : IMessageService
    {
        private readonly IHttpService httpService;

        public MessageService(IHttpService httpService)
        {
            this.httpService = httpService;
        }
        public async Task<OperationResponse<Message>> AddMessage(MessageDto messageDto)
        {
            try
            {
                var result = await httpService.Post<OperationResponse<Message>>("Message/AddMessage", messageDto);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<List<MessageDto>>> GetAllMessages()
        {
            try
            {
                var result = await httpService.Get<OperationResponse<List<MessageDto>>>("Message/GetAllMessages");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<MessageDto>> GetMessage(int id)
        {
            try
            {
                var result = await httpService.Get<OperationResponse<MessageDto>>($"Message/GetMessage?id={id}");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<int>> RemoveMessage(int id)
        {
            try
            {
                var result = await httpService.Delete<OperationResponse<int>>($"Message/RemoveMessage?id={id}");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResponse<Message>> UpdateMessage(MessageDto messageDto)
        {
            try
            {
                var result = await httpService.Put<OperationResponse<Message>>("Message/UpdateMessage", messageDto);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

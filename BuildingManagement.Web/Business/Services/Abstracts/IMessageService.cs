using BuildigManagement.Business.Utils;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildingManagement.Web.Business.Services.Abstracts
{
    public interface IMessageService
    {
        Task<OperationResponse<List<MessageDto>>> GetAllMessages();
        Task<OperationResponse<MessageDto>> GetMessage(int id);
        Task<OperationResponse<Message>> AddMessage(MessageDto messageDto);
        Task<OperationResponse<Message>> UpdateMessage(MessageDto messageDto);
        Task<OperationResponse<int>> RemoveMessage(int id);
    }
}

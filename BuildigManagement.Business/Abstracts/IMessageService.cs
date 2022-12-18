using BuildigManagement.Business.Utils;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using System.Collections.Generic;

namespace BuildigManagement.Business.Abstracts
{
    public interface IMessageService
    {
        OperationResponse<List<MessageDto>> GetAllMessages();
        OperationResponse<MessageDto> GetMessage(int id);
        OperationResponse<Message> AddMessage(MessageDto messageDto);
        OperationResponse<Message> UpdateMessage(MessageDto messageDto);
        OperationResponse<int> RemoveMessage(int id);
    }
}

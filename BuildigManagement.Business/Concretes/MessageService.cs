using AutoMapper;
using BuildigManagement.Business.Abstracts;
using BuildigManagement.Business.Utils;
using BuildingManagement.DataAccess.Abstracts;
using BuildingManagement.Entities.DTOs;
using BuildingManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildigManagement.Business.Concretes
{
    public class MessageService : IMessageService
    {
        private readonly IRepository _repo;
        private readonly IMapper mapper;

        public MessageService(IRepository repo, IMapper map)
        {
            _repo = repo;
            mapper = map;
        }
        public OperationResponse<Message> AddMessage(MessageDto messageDto)
        {
            try
            {
                var message = mapper.Map<Message>(messageDto);

                var messages = _repo.GetAll<Message>().ToList();

                var result = new OperationResponse<Message>();

                message.SendDate = DateTime.Now;
                message.IsRead = false;

                _repo.Add(message);
                _repo.SaveChanges();

                result = OperationResponse<Message>.CreateSuccesResponse(message);
                result.Message = "Successfully added.";


                return result;

            }
            catch (Exception ex)
            {
                return OperationResponse<Message>.CreateFailure(ex.Message);
            }
        }

        public OperationResponse<List<MessageDto>> GetAllMessages()
        {
            try
            {
                var messages = _repo.GetAll<Message>().ToList();

                var messagesDto = mapper.Map<List<MessageDto>>(messages);

                var result = OperationResponse<List<MessageDto>>.CreateSuccesResponse(messagesDto);

                result.Message = "Successfully brought.";

                return result;
            }
            catch (Exception ex)
            {
                return OperationResponse<List<MessageDto>>.CreateFailure(ex.Message);
            }
        }

        public OperationResponse<MessageDto> GetMessage(int id)
        {
            try
            {
                var message = _repo.Get<Message>(id);

                var messageDto = mapper.Map<MessageDto>(message);

                var result = OperationResponse<MessageDto>.CreateSuccesResponse(messageDto);

                result.Message = "Successfully brought.";

                return result;
            }
            catch (Exception ex)
            {
                return OperationResponse<MessageDto>.CreateFailure(ex.Message);
            }
        }

        public OperationResponse<int> RemoveMessage(int id)
        {
            try
            {
                var message = _repo.Get<Message>(id);
                _repo.Remove(message);
                var response = _repo.SaveChanges();
                var result = OperationResponse<int>.CreateSuccesResponse(response);
                result.Message = "Successfully deleted.";
                return result;
            }
            catch (Exception ex)
            {
                return OperationResponse<int>.CreateFailure(ex.Message);
            }
        }

        public OperationResponse<Message> UpdateMessage(MessageDto messageDto)
        {
            try
            {
                var message = mapper.Map<Message>(messageDto);

                var messages = _repo.GetAll<Message>().ToList();

                var result = new OperationResponse<Message>();


                _repo.Update(message);
                var response = _repo.SaveChanges();

                result = OperationResponse<Message>.CreateSuccesResponse(message);
                result.Message = "Successfully updated.";


                return result;
            }
            catch (Exception ex)
            {
                return OperationResponse<Message>.CreateFailure(ex.Message);
            }
        }
    }
}

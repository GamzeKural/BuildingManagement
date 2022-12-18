using System;

namespace BuildingManagement.Entities.DTOs
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string MessageBody { get; set; }
        public bool IsRead { get; set; }
        public DateTime SendDate { get; set; }
    }
}

using System;

namespace BuildingManagement.Entities.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string MessageBody { get; set; }
        public bool IsRead { get; set; }
        public DateTime SendDate { get; set; }

        public virtual User? SenderUser { get; set; }
        public virtual User? ReceiverUser { get; set; }
    }
}

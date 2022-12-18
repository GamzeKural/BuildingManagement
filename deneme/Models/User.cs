using System.Collections.Generic;

namespace deneme.Models
{
    public partial class User
    {
        public User()
        {
            Apartments = new HashSet<Apartment>();
            SenderMessages = new HashSet<Message>();
            ReceiverMessages = new HashSet<Message>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Mail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string CarInfo { get; set; }
        public int RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }
        public virtual ICollection<Message> SenderMessages { get; set; }
        public virtual ICollection<Message> ReceiverMessages { get; set; }
    }
}

using System.Collections.Generic;

namespace deneme.Models
{
    public partial class Apartment
    {
        public Apartment()
        {
            Dueses = new HashSet<Dues>();
        }
        public int Id { get; set; }
        public string Block { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int FloorLocation { get; set; }
        public int Number { get; set; }
        public int UserId { get; set; }

        public virtual User? User { get; set; }
        public ICollection<Dues> Dueses { get; set; }
    }
}

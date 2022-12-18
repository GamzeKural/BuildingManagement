using System;

namespace deneme.Models
{
    public partial class Dues
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int ApartmentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaidDate { get; set; }

        public virtual Apartment? Apartment { get; set; }
    }
}

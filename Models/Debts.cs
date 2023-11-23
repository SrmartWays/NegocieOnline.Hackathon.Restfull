using System.ComponentModel.DataAnnotations;

namespace NegocieOnline.Hackathon.Restfull.Models
{
    public class Debt
    {
        [Key] 
        public string CustomerId { get; set; }
        public int DealId { get; set; }
        public decimal Value { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalValue { get; set; }
        public int PaymentId { get; set; }
        public bool isPaid { get; set; }

    }
}

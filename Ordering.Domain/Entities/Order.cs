using Ordering.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Ordering.Domain.Entities
{
    public class Order : EntityBase
    {
        [Required]
        public string UserName { get; set; }

        public decimal TotalPrice { get; set; }

        //billingAddress
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(30)]
        public string EmailAddress { get; set; }

        public string AddressLine { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        //Payment
        public string CardName { get; set; }
        public string CarNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public int PaymentMethod { get; set; }

    }
}

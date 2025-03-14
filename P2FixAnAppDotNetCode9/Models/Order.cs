using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace P2FixAnAppDotNetCode9.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }
        
        [BindNever]
        [ValidateNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessage = "ErrorMissingName")]
        public string Name { get; set; }

        [Required(ErrorMessage = "ErrorMissingAddress")]
        public string Address { get; set; }

        [Required(ErrorMessage = "ErrorMissingCity")]
        public string City { get; set; }

        [Required(ErrorMessage = "ErrorMissingZip")]
        [DataType(DataType.PostalCode)]
        public string Zip { get; set; }

        [Required(ErrorMessage = "ErrorMissingCountry")]
        public string Country { get; set; }

        [BindNever]
        public DateTime Date { get; set; }
    }
}

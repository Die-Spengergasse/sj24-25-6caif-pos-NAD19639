using System.ComponentModel.DataAnnotations;
using SPG_Fachtheorie.Aufgabe1.Model;

namespace SPG_Fachtheorie.Aufgabe1.Commands
{
    public class NewPaymentItemCommand
    {
        [Required(ErrorMessage = "ArticleName is required.")]
        [MaxLength(255, ErrorMessage = "ArticleName cannot exceed 255 characters.")]
        public string ArticleName { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Amount must be at least 1.")]
        public int Amount { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "PaymentId is required.")]
        public int PaymentId { get; set; }
    }
}

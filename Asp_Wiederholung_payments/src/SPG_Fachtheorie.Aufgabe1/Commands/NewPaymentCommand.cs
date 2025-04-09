using System.ComponentModel.DataAnnotations;
using SPG_Fachtheorie.Aufgabe1.Model;

namespace SPG_Fachtheorie.Aufgabe1.Commands;

public class NewPaymentCommand
{
    [Required]
    public int CashDeskNumber { get; set; }

    [Required]
    public int EmployeeId { get; set; }

    [Required]
    public PaymentType PaymentType { get; set; }
}

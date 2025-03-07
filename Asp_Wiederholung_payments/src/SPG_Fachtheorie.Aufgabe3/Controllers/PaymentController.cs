using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

[Route("api/payments")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private static List<PaymentDetailDto> payments = new List<PaymentDetailDto>
    {
        new PaymentDetailDto
        {
            Id = 1,
            EmployeeFirstName = "Max",
            EmployeeLastName = "Mustermann",
            CashDeskNumber = 3,
            PaymentType = "Credit Card",
            PaymentItems = new List<PaymentItemDto>
            {
                new PaymentItemDto { ArticleName = "Apple", Amount = 2, Price = 1.5m },
                new PaymentItemDto { ArticleName = "Milk", Amount = 1, Price = 2.0m }
            }
        },
        new PaymentDetailDto
        {
            Id = 2,
            EmployeeFirstName = "Lisa",
            EmployeeLastName = "Müller",
            CashDeskNumber = 1,
            PaymentType = "Cash",
            PaymentItems = new List<PaymentItemDto>
            {
                new PaymentItemDto { ArticleName = "Bread", Amount = 3, Price = 1.2m }
            }
        }
    };

    // GET /api/payments
    [HttpGet]
    public ActionResult<IEnumerable<PaymentDto>> GetPayments([FromQuery] int? cashDesk, [FromQuery] DateTime? dateFrom)
    {
        var result = payments
            .Where(p => !cashDesk.HasValue || p.CashDeskNumber == cashDesk.Value)
            .Select(p => new PaymentDto
            {
                Id = p.Id,
                EmployeeFirstName = p.EmployeeFirstName,
                EmployeeLastName = p.EmployeeLastName,
                CashDeskNumber = p.CashDeskNumber,
                PaymentType = p.PaymentType,
                TotalAmount = p.PaymentItems.Sum(i => i.Price * i.Amount)
            })
            .ToList();

        return Ok(result);
    }

    // GET /api/payments/{id}
    [HttpGet("{id}")]
    public ActionResult<PaymentDetailDto> GetPaymentById(int id)
    {
        var payment = payments.FirstOrDefault(p => p.Id == id);
        if (payment == null)
        {
            return NotFound();
        }
        return Ok(payment);
    }
}

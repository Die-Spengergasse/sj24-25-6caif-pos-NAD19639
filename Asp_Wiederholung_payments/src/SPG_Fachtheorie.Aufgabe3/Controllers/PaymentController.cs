using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly AppointmentContext _db;

        public PaymentsController(AppointmentContext db)
        {
            _db = db;
        }

        // PATCH /api/payments/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePaymentConfirmed(int id, [FromBody] DateTime? confirmed)
        {
            var payment = await _db.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound(new { message = "Payment not found" });
            }

            if (payment.Confirmed.HasValue)
            {
                return BadRequest(new { message = "Payment already confirmed" });
            }

            if (confirmed.HasValue && confirmed > DateTime.UtcNow.AddMinutes(1))
            {
                return BadRequest(new { message = "Confirmed date cannot be more than 1 minute in the future." });
            }

            payment.Confirmed = confirmed;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // PUT /api/paymentItems/{id}
        [HttpPut("paymentItems/{id}")]
        public async Task<IActionResult> UpdatePaymentItem(int id, [FromBody] PaymentItem updatedItem)
        {
            var paymentItem = await _db.PaymentItems.Include(pi => pi.Payment).FirstOrDefaultAsync(pi => pi.Id == id);
            if (paymentItem == null)
            {
                return NotFound(new { message = "Payment Item not found" });
            }

            if (updatedItem.LastUpdated.HasValue && paymentItem.LastUpdated.HasValue && updatedItem.LastUpdated != paymentItem.LastUpdated)
            {
                return BadRequest(new { message = "Payment item has been modified by another process." });
            }

            paymentItem.ArticleName = updatedItem.ArticleName;
            paymentItem.Amount = updatedItem.Amount;
            paymentItem.Price = updatedItem.Price;
            paymentItem.LastUpdated = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}

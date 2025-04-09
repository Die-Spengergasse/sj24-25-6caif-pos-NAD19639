using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Model;
using SPG_Fachtheorie.Aufgabe1.Commands;
using SPG_Fachtheorie.Aufgabe1.Exeptions;

public class PaymentService
{
    private readonly AppointmentContext _context;

    public PaymentService(AppointmentContext context)
    {
        _context = context;
    }

    public Payment CreatePayment(NewPaymentCommand cmd)
    {
        // Prüfe, ob es bereits ein offenes Payment (nicht confirmed) für den CashDesk gibt.
        var openPayments = _context.Payments
            .Where(p => p.CashDesk.Number == cmd.CashDeskNumber && p.Confirmed == null);
        if (openPayments.Any())
        {
            throw new PaymentServiceException("Open payment for cashdesk.");
        }

        // Ermittle den CashDesk
        var cashDesk = _context.CashDesks.Find(cmd.CashDeskNumber);
        if (cashDesk == null)
        {
            throw new PaymentServiceException("CashDesk not found.");
        }

        // Ermittle den Employee anhand der EmployeeId
        var employee = _context.Employees.FirstOrDefault(e => e.RegistrationNumber == cmd.EmployeeId);
        if (employee == null)
        {
            throw new PaymentServiceException("Employee not found.");
        }

        // Falls der PaymentType CreditCard ist, muss der Employee vom Typ "Manager" sein.
        if (cmd.PaymentType == PaymentType.CreditCard && employee.Type != "Manager")
        {
            throw new PaymentServiceException("Insufficient rights to create a credit card payment.");
        }

        // Neues Payment mit dem vorhandenen Konstruktor anlegen
        var payment = new Payment(cashDesk, DateTime.UtcNow, employee, cmd.PaymentType)
        {
            Confirmed = null
            // PaymentItems wird nicht gesetzt, da es in der Klasse als readonly initialisiert wird.
        };

        _context.Payments.Add(payment);
        _context.SaveChanges();
        return payment;
    }

    public void ConfirmPayment(int paymentId)
    {
        var payment = _context.Payments.Find(paymentId);
        if (payment == null)
        {
            throw new PaymentServiceException("Payment not found.");
        }
        if (payment.Confirmed != null)
        {
            throw new PaymentServiceException("Payment already confirmed.");
        }
        payment.Confirmed = DateTime.UtcNow;
        _context.SaveChanges();
    }

    public void AddPaymentItem(NewPaymentItemCommand cmd)
    {
        var payment = _context.Payments.Find(cmd.PaymentId);
        if (payment == null)
        {
            throw new PaymentServiceException("Payment not found.");
        }
        if (payment.Confirmed != null)
        {
            throw new PaymentServiceException("Payment already confirmed.");
        }
        // PaymentItem wird über den vorhandenen Konstruktor erstellt.
        var paymentItem = new PaymentItem(cmd.ArticleName, cmd.Amount, cmd.Price, payment);

        _context.PaymentItems.Add(paymentItem);
        _context.SaveChanges();
    }

    public void DeletePayment(int paymentId, bool deleteItems)
    {
        var payment = _context.Payments.Include(p => p.PaymentItems)
                                       .FirstOrDefault(p => p.Id == paymentId);
        if (payment == null)
        {
            throw new PaymentServiceException("Payment not found.");
        }
        if (deleteItems)
        {
            _context.PaymentItems.RemoveRange(payment.PaymentItems);
        }
        _context.Payments.Remove(payment);
        _context.SaveChanges();
    }
}

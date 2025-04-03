using System;
using System.Collections.Generic;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Payment
    {

        public Payment(CashDesk cashDesk, DateTime paymentDateTime, Employee employee, PaymentType paymentType)
        {
            CashDesk = cashDesk;
            PaymentDateTime = paymentDateTime;
            Employee = employee;
            PaymentType = paymentType;
        }

#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
        protected Payment() { }
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.

        public int Id { get; set; }
        public CashDesk CashDesk { get; set; }
        public DateTime PaymentDateTime { get; set; }
        public Employee Employee { get; set; }
        public PaymentType PaymentType { get; set; }
        public List<PaymentItem> PaymentItems { get; } = new();
        public DateTime? Confirmed { get; set; }
    }
}

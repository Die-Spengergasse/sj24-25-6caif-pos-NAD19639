using System;
using System.Collections.Generic;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Payment
    {
        public int Id { get; set; }
        public CashDesk CashDesk { get; set; }
        public DateTime PaymentDateTime { get; set; }
        public PaymentType PaymentType { get; set; }
        public Employee Employee { get; set; }
        public List<PaymentItem> PaymentItems { get; set; }


#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
        protected Payment() { }
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.


        public Payment(CashDesk cashDesk, DateTime paymentDateTime, PaymentType paymentType, Employee employee)
        {
            CashDesk = cashDesk;
            PaymentDateTime = paymentDateTime;
            PaymentType = paymentType;
            Employee = employee;
            PaymentItems = new List<PaymentItem>();
        }
    }

}
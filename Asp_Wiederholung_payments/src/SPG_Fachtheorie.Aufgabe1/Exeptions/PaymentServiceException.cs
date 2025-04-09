using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe1.Exeptions;

public class PaymentServiceException : Exception
{
    public PaymentServiceException(string message) : base(message)
    {

            throw new PaymentServiceException("PaymentService Fehler");
    }
}

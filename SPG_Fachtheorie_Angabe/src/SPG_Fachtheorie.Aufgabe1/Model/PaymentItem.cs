namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class PaymentItem
    {
        public int Id { get; set; }
        public string ArticleName { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public Payment Payment { get; set; }


#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
        protected PaymentItem() { }
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.

        public PaymentItem(string articleName, int amount, decimal price, Payment payment)
        {
            ArticleName = articleName;
            Amount = amount;
            Price = price;
            Payment = payment;
        }
    }

}
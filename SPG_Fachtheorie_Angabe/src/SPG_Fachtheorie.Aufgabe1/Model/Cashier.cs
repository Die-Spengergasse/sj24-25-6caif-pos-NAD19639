using System;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Cashier : Employee
    {
        public string JobSpezialisation { get; set; }


        #pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
        protected Cashier() { }
        #pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.


        public Cashier(string jobSpezialisation, string firstName, string lastName, Address address, string type)
            : base(firstName, lastName, address, type)
        {
            JobSpezialisation = jobSpezialisation;
        }
    }

}
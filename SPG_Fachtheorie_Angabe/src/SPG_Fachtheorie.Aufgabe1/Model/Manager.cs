namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Manager : Employee
    {
        public Manager(string carType, string firstName, string lastName, Address address, string type)
            : base(firstName, lastName, address, type)
        {
            CarType = carType;
        }


#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
        protected Manager() { }
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.

        public string CarType { get; set; }

    }
}
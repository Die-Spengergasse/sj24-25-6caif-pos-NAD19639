using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Linq;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe1.Test
{
    [Collection("Sequential")]
    public class Aufgabe1Test
    {
        private AppointmentContext GetEmptyDbContext()
        {
            var options = new DbContextOptionsBuilder<AppointmentContext>()
                .UseSqlite(@"Data Source=cash.db")
                .Options;

            var db = new AppointmentContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        // Erstellt eine leere Datenbank in Debug\net8.0\cash.db
        [Fact]
        public void CreateDatabaseTest()
        {
            using var db = GetEmptyDbContext();
        }

        [Fact]
        public void AddCashierSuccessTest()
        {
            using var db = GetEmptyDbContext();

            // Arrange
            var cashier = new Cashier("Keine", "Max", "M�ller", new Address("Hauptstra�e", "Berlin", "10115"), "Cashier");
            db.Employees.Add(cashier);

            // Act
            db.SaveChanges();
            db.ChangeTracker.Clear();

            // Assert
            var result = db.Employees.OfType<Cashier>().FirstOrDefault(c => c.Type == "Cashier");
            Assert.NotNull(result);
            Assert.Equal(1, result!.RegistrationNumber);  //reg num pr�fung
        }

        [Fact]
        public void AddPaymentSuccessTest()
        {
            using var db = GetEmptyDbContext();

            // Arrange
            var cashier = new Cashier("Keine", "Anna", "Schmidt", new Address("Marktplatz", "M�nchen", "80331"), "Cashier");
            var payment = new Payment(new CashDesk(), DateTime.Now, PaymentType.Cash, cashier);

            db.Employees.Add(cashier);
            db.Payments.Add(payment);

            // Act
            db.SaveChanges();
            db.ChangeTracker.Clear();

            // Assert
            var result = db.Payments.FirstOrDefault(p => p.PaymentDateTime.Date == DateTime.Now.Date);
            Assert.NotNull(result);
            Assert.Equal(PaymentType.Cash, result!.PaymentType);
        }

        [Fact]
        public void EmployeeDiscriminatorSuccessTest()
        {
            using var db = GetEmptyDbContext();

            // Arrange
            var manager = new Manager("SUV", "Lisa", "Meyer", new Address("Bahnhofstra�e", "Hamburg", "20095"), "Manager");
            db.Employees.Add(manager);

            // Act
            db.SaveChanges();
            db.ChangeTracker.Clear();

            // Assert
            var result = db.Employees.FirstOrDefault(e => e.Type == "Manager");
            Assert.NotNull(result);
            Assert.Equal("Manager", db.Entry(result!).Property("Type").CurrentValue);
        }
    }
}

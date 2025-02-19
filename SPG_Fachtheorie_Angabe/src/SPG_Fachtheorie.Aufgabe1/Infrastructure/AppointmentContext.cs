﻿using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Model;

namespace SPG_Fachtheorie.Aufgabe1.Infrastructure
{
    public class AppointmentContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Cashier> Cashiers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<CashDesk> CashDesks { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentItem> PaymentItems { get; set; }

        public AppointmentContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Employee>()
             .HasDiscriminator<string>("Type")
             .HasValue<Cashier>("Cashier")
             .HasValue<Manager>("Manager");

            modelBuilder.Entity<Employee>().OwnsOne(e => e.Address);

            modelBuilder.Entity<Payment>().Property(p => p.PaymentType).HasConversion<string>();
        }
    }
}
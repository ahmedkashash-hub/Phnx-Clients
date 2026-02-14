using Microsoft.EntityFrameworkCore;
using Phnx.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Phnx.Infrastructure.Persistence.Database
{

    public class PhnxDbContext : DbContext
    {
        private readonly AuditInterceptor? _auditInterceptor;

        public PhnxDbContext(DbContextOptions options, AuditInterceptor auditInterceptor) : base(options)
        {
            _auditInterceptor = auditInterceptor;
        }

        public PhnxDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_auditInterceptor is not null)
                optionsBuilder.AddInterceptors(_auditInterceptor);

            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<AgencyTask> AgencyTasks { get; set; }
        public DbSet<Activity> Activities { get; set; }

        //public DbSet<Advertisement> Advertisements { get; set; }
        //public DbSet<Job> Jobs { get; set; }
        //public DbSet<Department> Departments { get; set; }
        //public DbSet<LostItem> LostItems { get; set; }
        //public DbSet<Event> Events { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientCostEstimate.Models;

namespace PatientCostEstimate.Data
{
    public class PatientCostEstimateContext : DbContext
    {
        public PatientCostEstimateContext (DbContextOptions<PatientCostEstimateContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>().ToTable("Service");
            modelBuilder.Entity<Insurance>().ToTable("Insurance");
            modelBuilder.Entity<Patient>().ToTable("Patient");
        }

    }
}

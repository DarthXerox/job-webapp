using System;
using System.Linq;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class JobDbContext : DbContext
    {
        private const string ConnectionString = "";

        public DbSet<Company> Companies { get; set; }
        public DbSet<JobSeeker> JobSeekers { get; set; }
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(ConnectionString)
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobApplication>()
                .HasOne(a => a.Applicant)
                .WithOne().HasForeignKey<JobApplication>(a => a.ApplicantId);

            modelBuilder.Entity<JobApplication>()
                .HasOne(a => a.JobOffer)
                .WithOne().HasForeignKey<JobApplication>(a => a.JobOfferId);

            modelBuilder.Entity<JobOffer>()
                .HasOne(a => a.Address)
                .WithOne().HasForeignKey<JobOffer>(a => a.AddressId);

            modelBuilder.Entity<JobOffer>()
                .HasOne(a => a.Company)
                .WithOne().HasForeignKey<JobOffer>(a => a.CompanyId);

            modelBuilder.Entity<Company>()
                .HasOne(a => a.Address)
                .WithOne().HasForeignKey<Company>(a => a.AddressId);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}

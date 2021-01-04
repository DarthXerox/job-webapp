using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL
{
    public class JobDbContext : DbContext
    {
        private const string ConnectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JobDatabase;Trusted_Connection=True;";

        /// <summary>
        ///     Yes, this is not necessary, but when creating migrations DbContext MUST have a default no-parameter constructor
        /// </summary>
        public JobDbContext() { }

        public JobDbContext(DbContextOptions<JobDbContext> contextOptions)
            : base(contextOptions)
        {
        }

        public DbSet<Company>? Companies { get; set; }
        public DbSet<JobSeeker>? JobSeekers { get; set; }
        public DbSet<JobOffer>? JobOffers { get; set; }
        public DbSet<JobApplication>? JobApplications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(ConnectionString)
                    .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var stringConverter = new ValueConverter<ICollection<string>, string>(
                s => string.Join(';', s),
                s => s.Split(new[] { ';' }));
            modelBuilder.Entity<JobOffer>().Property(nameof(JobOffer.RelevantSkills)).HasConversion(stringConverter);
            modelBuilder.Entity<JobSeeker>().Property(nameof(JobSeeker.Skills)).HasConversion(stringConverter);

            modelBuilder.Entity<JobApplication>()
                .HasOne(a => a.Applicant)
                .WithOne().HasForeignKey<JobApplication>(a => a.ApplicantId);

            modelBuilder.Entity<JobApplication>()
                .HasOne(a => a.JobOffer)
                .WithOne().HasForeignKey<JobApplication>(a => a.JobOfferId);

            modelBuilder.Entity<JobApplicationAnswer>()
                .HasOne(a => a.Question)
                .WithOne().HasForeignKey<JobApplicationAnswer>(a => a.QuestionId);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}

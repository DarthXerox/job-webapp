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
        public DbSet<User>? Users { get; set; }


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

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.SetNull;
            }

            modelBuilder.Entity<JobOfferQuestion>()
                .HasOne(q => q.JobOffer)
                .WithMany(o => o.Questions)
                .HasForeignKey(q => q.JobOfferId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobOffer>()
                .HasOne(o => o.Company)
                .WithMany(c => c.Offers)
                .HasForeignKey(o => o.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobSeeker>()
                .HasOne(j => j.User)
                .WithOne(u => u.JobSeeker)
                .HasForeignKey<JobSeeker>(j => j.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Company>()
                .HasOne(j => j.User)
                .WithOne(u => u.Company)
                .HasForeignKey<Company>(j => j.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}

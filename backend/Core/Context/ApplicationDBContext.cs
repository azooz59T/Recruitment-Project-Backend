using backend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace backend.Core.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<Company> Companys { get; set; }
        
        public DbSet<Job> Jobs { get; set; }

        public DbSet<Candidate> Candidates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Job>()
                .HasOne(job => job.Company)
                .WithMany(company => company.Jobs)
                .HasForeignKey(job => job.CompanyId);

            modelBuilder.Entity<Candidate>()
                .HasOne(candidate => candidate.job)
                .WithMany(job => job.Candidates)
                .HasForeignKey(candidate => candidate.JobId);
        }
    


    }
}

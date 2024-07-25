using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContext : IdentityDbContext<User>
    {
        public StoreContext()
        {

        }
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<AggregatedTransaction> AggregatedTransactions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Transactions
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.HasOne(t => t.User)
                    .WithMany() // Assuming User does not have a collection of Transactions
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(t => t.Amount)
                    .HasPrecision(18, 2);
            });

            // Configure AggregatedTransactions
            modelBuilder.Entity<AggregatedTransaction>(entity =>
            {
                entity.HasKey(at => at.Id);
                entity.HasOne(at => at.User)
                    .WithMany() // Assuming User does not have a collection of AggregatedTransactions
                    .HasForeignKey(at => at.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(at => at.DailyTotal).HasPrecision(18, 2);
                entity.Property(at => at.WeeklyTotal).HasPrecision(18, 2);
                entity.Property(at => at.MonthlyTotal).HasPrecision(18, 2);
            });
        }

    }
}

using System;
using INeed.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace INeed.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Sub>? Subs { get; set; }
        public DbSet<Form>? Forms { get; set; }
        public DbSet<Question>? Questions { get; set; }
        public DbSet<Answer>? Answers { get; set; }
        public DbSet<Category> Categories { get; set; }

        // --- BRAKUJĄCE DBSETS ---
        public DbSet<VisitorResult> VisitorResults { get; set; }
        public DbSet<VisitorCategoryScore> VisitorCategoryScores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sub>().HasKey(s => s.SubId);

            // Konfiguracja Form -> Questions
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Form)
                .WithMany(f => f.Questions)
                .HasForeignKey(q => q.FormId)
                .OnDelete(DeleteBehavior.Restrict);

            // Konfiguracja Question -> Answers
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Konfiguracja VisitorResult -> Form
            modelBuilder.Entity<VisitorResult>()
                .HasOne(v => v.Form)
                .WithMany()
                .HasForeignKey(v => v.FormId)
                .OnDelete(DeleteBehavior.Cascade);

            // Konfiguracja VisitorResult -> CategoryScores
            modelBuilder.Entity<VisitorResult>()
                .HasMany(v => v.CategoryScores)
                .WithOne(s => s.VisitorResult)
                .HasForeignKey(s => s.VisitorResultId)
                .OnDelete(DeleteBehavior.Cascade);

            // Konfiguracja VisitorCategoryScore -> Category
            modelBuilder.Entity<VisitorCategoryScore>()
                .HasOne(s => s.Category)
                .WithMany()
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sub>()
                .HasKey(s => s.SubId);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Form)
                .WithMany(f => f.Questions)
                .HasForeignKey(q => q.FormId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany()
                .HasForeignKey(a => a.QuestionId) 
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

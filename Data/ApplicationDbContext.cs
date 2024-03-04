using ForumApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ForumApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Thread)
                .WithMany(t => t.Posts)
                .HasForeignKey(p => p.ThreadId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<ForumApp.Models.Forum> Forums { get; set; }
        public DbSet<ForumApp.Models.Thread> Threads { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}

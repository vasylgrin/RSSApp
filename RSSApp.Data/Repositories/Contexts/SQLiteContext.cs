using Microsoft.EntityFrameworkCore;
using RSSApp.Entity.Models;

namespace RSSApp.Data.Repositories.Contexts
{
    internal class SQLiteContext : DbContext
    {
        public DbSet<Feed> Feeds { get; set; }
        public DbSet<Entries> Entries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ReadEntries> ReadEntries { get; set; }

        public SQLiteContext() => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entries>()
            .HasOne(entries => entries.Feed)
            .WithMany(feed => feed.Entries)
            .HasForeignKey(entries => entries.FeedId);

            modelBuilder.Entity<ReadEntries>()
            .HasOne(readEntries => readEntries.User)
            .WithMany(user => user.ReadingEntries)
            .HasForeignKey(rEntries => rEntries.UserId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=RSSApp.db");
        }
    }
}

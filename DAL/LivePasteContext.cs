using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class LivePasteContext : DbContext
    {
        public virtual DbSet<Models.Paste> Pastes { get; set; }

        public LivePasteContext()
        {
        }

        public LivePasteContext(DbContextOptions<LivePasteContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Paste>().ToTable("Paste");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(Utils.ReadFile("..\\DAL\\ConnectionString.txt"));
            }
        }
    }
}

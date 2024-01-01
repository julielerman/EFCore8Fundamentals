using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ReverseEngineerBefore;

public partial class PubDatabaseContext : DbContext
{
    public PubDatabaseContext()
    {
    }

    public PubDatabaseContext(DbContextOptions<PubDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer
             ("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PubDatabase");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasIndex(e => e.AuthorId, "IX_Books_AuthorId");

            entity.Property(e => e.BasePrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Author).WithMany(p => p.Books).HasForeignKey(d => d.AuthorId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.DbContext;

public class SdiaDbContext : Microsoft.EntityFrameworkCore.DbContext, ISdiaDbContext
{
    public SdiaDbContext(DbContextOptions<SdiaDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>().HasQueryFilter(x => x.IsDeleted == false);
        modelBuilder.Entity<Folder>().HasQueryFilter(x => x.IsDeleted == false);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new AuditInterceptor());
        optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
    }

    public virtual DbSet<User> Users { get; init; }

    public virtual DbSet<Document> Documents { get; init; }

    public virtual DbSet<Folder> Folders { get; init; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return await base.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        await base.Database.CommitTransactionAsync(cancellationToken);
    }
}
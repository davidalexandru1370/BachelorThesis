using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContext;

public class SdiaDbContext : Microsoft.EntityFrameworkCore.DbContext, ISdiaDbContext
{
    public SdiaDbContext(DbContextOptions<SdiaDbContext> options) : base(options)
    {
        
    }
    public virtual DbSet<User> Users { get; init; }
    
    public virtual DbSet<Document> Documents { get; init; }
    
    public virtual DbSet<Folder> Folders { get; init; }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
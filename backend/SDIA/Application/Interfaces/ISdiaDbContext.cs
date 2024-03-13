using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Interfaces;

public interface ISdiaDbContext
{
    public DbSet<User> Users { get; init; }
    public DbSet<Document> Documents { get; init; }
    
    public DbSet<Folder> Folders { get; init; }
    public DbSet<FolderErrors> FolderErrors { get; init; }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    public Task CommitTransactionAsync(CancellationToken cancellationToken = default);
}
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface ISdiaDbContext
{
    public DbSet<User> Users { get; init; }
    public DbSet<Document> Documents { get; init; }
    
    public DbSet<Folder> Folders { get; init; }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
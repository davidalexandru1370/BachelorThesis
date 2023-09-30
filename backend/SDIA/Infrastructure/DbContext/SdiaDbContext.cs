using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContext;

public class SdiaDbContext : Microsoft.EntityFrameworkCore.DbContext, ISdiaDbContext
{
    public SdiaDbContext(DbContextOptions<SdiaDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    
    public DbSet<Document> Documents { get; set; }
    
    public DbSet<Folder> Folders { get; set; }
}
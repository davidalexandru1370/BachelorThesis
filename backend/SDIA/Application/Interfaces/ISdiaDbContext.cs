using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface ISdiaDbContext
{
    public DbSet<User> Users { get; set; }
}
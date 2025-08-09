using Microsoft.EntityFrameworkCore;
using API.Entities; // Add this if AppUser is in API.Entities namespace

namespace API.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set; } // A constructor to tell the Entity framework where our entity so we use the DbSet property
}

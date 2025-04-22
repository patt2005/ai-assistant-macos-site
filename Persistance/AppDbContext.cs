using AIAssistantMacos.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIAssistantMacos.Persistance;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Code> Codes { get; set; }
}
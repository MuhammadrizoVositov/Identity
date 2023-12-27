using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRole.Domain.Entites;

namespace UserRole.Persistance.DataAccess;
public class UserRoleDbContext : DbContext
{
    
    public DbSet<User> Users => Set<User>();
   
    public UserRoleDbContext(DbContextOptions options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserRoleDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
}

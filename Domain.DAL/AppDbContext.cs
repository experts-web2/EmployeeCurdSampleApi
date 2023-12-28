using Domian.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DAL
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Employee> Employees { get; set; }

        public override int SaveChanges()
        {

            SetBaseEntity();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetBaseEntity();

            return base.SaveChangesAsync(cancellationToken);
        }
        private void SetBaseEntity()
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is EntityBase && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {

                var currentUser = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
                if (entityEntry.State == EntityState.Added)
                {
                    ((EntityBase)entityEntry.Entity).CreatedDate = DateTime.Now;
                    ((EntityBase)entityEntry.Entity).CreatedBy = "User";
                }
                if (entityEntry.State == EntityState.Modified)
                {
                    ((EntityBase)entityEntry.Entity).ModifiedDate = DateTime.Now;
                    ((EntityBase)entityEntry.Entity).ModifiedBy = "User";

                }
            }
        }
    }
}

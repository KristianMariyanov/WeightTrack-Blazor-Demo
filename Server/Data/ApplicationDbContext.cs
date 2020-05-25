using WeightTrack.Shared.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;

namespace WeightTrack.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<FoodLog> FoodLogs { get; set; }

        public DbSet<Food> Foods { get; set; }

        public DbSet<WeightLog> WeightLogs { get; set; }

        public DbSet<FoodCategory> FoodCategories { get; set; }
    }
}

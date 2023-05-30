using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Drones.EntityFrameworkCore
{
    public static class DronesDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<DronesDbContext> builder, string connectionString)
        {
            builder.UseNpgsql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<DronesDbContext> builder, DbConnection connection)
        {
            builder.UseNpgsql(connection);
        }
    }
}

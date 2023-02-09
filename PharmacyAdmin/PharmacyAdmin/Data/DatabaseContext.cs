using Microsoft.EntityFrameworkCore;

namespace PharmacyAdmin.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}

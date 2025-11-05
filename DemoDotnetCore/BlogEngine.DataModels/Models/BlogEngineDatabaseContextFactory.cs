using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BlogEngine.DataModels.Models
{
    public class BlogEngineDatabaseContextFactory : IDesignTimeDbContextFactory<BlogEngineDatabaseContext>
    {
        public BlogEngineDatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogEngineDatabaseContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=BlogEngineDatabase;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True");

            return new BlogEngineDatabaseContext(optionsBuilder.Options);
        }
    }
}

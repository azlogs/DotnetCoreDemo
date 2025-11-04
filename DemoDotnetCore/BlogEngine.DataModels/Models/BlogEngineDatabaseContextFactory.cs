using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BlogEngine.DataModels.Models
{
    public class BlogEngineDatabaseContextFactory : IDesignTimeDbContextFactory<BlogEngineDatabaseContext>
    {
        public BlogEngineDatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogEngineDatabaseContext>();
            optionsBuilder.UseSqlite("Data Source=blogengine.db");

            return new BlogEngineDatabaseContext(optionsBuilder.Options);
        }
    }
}

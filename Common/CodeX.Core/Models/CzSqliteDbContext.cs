using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace CodeX.Core.Models
{
    public class CzSqliteDbContext : DbContext, IValidatableObject
    {
        public static string DatabaseDirectory { get; set; }

        public static string DatabaseFilename => Path.Combine(DatabaseDirectory, "CodeX.db3");

        private static DbContextOptions<CzSqliteDbContext> DatabaseOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CzSqliteDbContext>();
            optionsBuilder.UseSqlite($"Data Source={DatabaseFilename}");
            optionsBuilder.EnableDetailedErrors(true);
            return optionsBuilder.Options;
        }

        public CzSqliteDbContext() : this(DatabaseOptions())
        {
        }

        public CzSqliteDbContext(DbContextOptions<CzSqliteDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<CzAccount> UserAccounts { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
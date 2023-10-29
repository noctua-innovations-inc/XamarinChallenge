/*
 * This project exists solely for Entity Framework (EF) Core.
 * This project is to be referenced as the start-up project for EF Core migrations.
 *
 */

using CodeX.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.IO;

namespace DataModel
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Set database directory
            CzSqliteDbContext.DatabaseDirectory = Directory.GetCurrentDirectory();

            // Create the database context to preform the migration
            using (var db = new CzSqliteDbContext()) { }

            // We're done here ;)
            Console.WriteLine("Hello World!");
        }
    }

    public class ContextFactory : IDesignTimeDbContextFactory<CzSqliteDbContext>
    {
        public CzSqliteDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CzSqliteDbContext>();
            optionsBuilder.UseSqlite("Data Source=CodeX.db3");

            return new CzSqliteDbContext(optionsBuilder.Options);
        }
    }
}
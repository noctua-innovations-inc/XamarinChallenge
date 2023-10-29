using CodeX.Core.Contracts;
using CodeX.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeX.Core.Services
{
    /// <summary>
    /// Repository Pattern
    /// </summary>
    public class CzRepository : ICzRepository
    {
        // Interface Segregation Principle
        // Dependency Injection (DI) without DI container
        public static ICzRepository Instance = new CzRepository();

        public long GetRecordCountAsync<TDbRecord>() where TDbRecord : class
        {
            using (var instance = new CzSqliteDbContext())
            {
                return instance
                    .Set<TDbRecord>()
                    .Count();
            }
        }

        public async Task<int> ExecuteNonQueryAsync(string sqlQuery, object[] parameters = null)
        {
            using (var instance = new CzSqliteDbContext())
            {
                var result = 0;
                using (var command = instance.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sqlQuery;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        command.Connection.Open();
                        result = await command.ExecuteNonQueryAsync();
                    }
                    finally
                    {
                        command.Connection.Close();
                    }
                }
                return result;
            }
        }

        public async Task<List<TDbRecord>> QueryAsync<TDbRecord>(string sqlQuery, object[] parameters = null) where TDbRecord : class
        {
            using (var instance = new CzSqliteDbContext())
            {
                parameters = parameters ?? Array.Empty<object>();

                var recordset = instance.Set<TDbRecord>();
                var result = recordset.FromSqlRaw(sqlQuery, parameters).ToList();
                return await Task.FromResult<List<TDbRecord>>(result);
            }
        }

        public async Task<TDbRecord> GetRecordAsync<TDbRecord>(Expression<Func<TDbRecord, bool>> findRecordExpression) where TDbRecord : class
        {
            using (var instance = new CzSqliteDbContext())
            {
                var recordset = instance.Set<TDbRecord>();
                var result = await recordset.Where(findRecordExpression).FirstOrDefaultAsync();
                return result;
            }
        }

        public async Task<int> SaveRecordAsync<TDbRecord>(TDbRecord record) where TDbRecord : class
        {
            using (var instance = new CzSqliteDbContext())
            {
                var recordset = instance.Set<TDbRecord>();
                recordset.Add(record);
                return await instance.SaveChangesAsync();
            }
        }

        public async Task<int> UpdateOrSaveAsync<TDbRecord>(TDbRecord record) where TDbRecord : class
        {
            using (var instance = new CzSqliteDbContext())
            {
                var entry = instance.Attach(record);
                int result;
                try
                {
                    entry.State = EntityState.Modified;
                    result = await instance.SaveChangesAsync();
                }
                catch
                {
                    try
                    {
                        entry.State = EntityState.Added;
                        result = await instance.SaveChangesAsync();
                    }
                    catch
                    {
                        entry.State = EntityState.Detached;
                        throw;
                    }
                }
                return await Task.FromResult<int>(result);
            }
        }

        public async Task<int> UpdateRecordAsync<TDbRecord>(TDbRecord record) where TDbRecord : class
        {
            using (var instance = new CzSqliteDbContext())
            {
                instance.Update(record);
                return await instance.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteRecordAsync<TDbRecord>(TDbRecord record) where TDbRecord : class
        {
            using (var instance = new CzSqliteDbContext())
            {
                instance.Remove(record);
                return await instance.SaveChangesAsync();
            }
        }
    }
}
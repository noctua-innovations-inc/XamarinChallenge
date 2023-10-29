using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeX.Core.Contracts
{
    public interface ICzRepository
    {
        long GetRecordCountAsync<TDbRecord>() where TDbRecord : class;

        Task<int> ExecuteNonQueryAsync(string sqlQuery, object[] parameters = null);

        Task<List<TDbRecord>> QueryAsync<TDbRecord>(string sqlQuery, object[] parameters = null) where TDbRecord : class;

        Task<TDbRecord> GetRecordAsync<TDbRecord>(Expression<Func<TDbRecord, bool>> findRecordExpression) where TDbRecord : class;

        Task<int> SaveRecordAsync<TDbRecord>(TDbRecord record) where TDbRecord : class;

        Task<int> UpdateOrSaveAsync<TDbRecord>(TDbRecord record) where TDbRecord : class;

        Task<int> UpdateRecordAsync<TDbRecord>(TDbRecord record) where TDbRecord : class;

        Task<int> DeleteRecordAsync<TDbRecord>(TDbRecord record) where TDbRecord : class;
    }
}
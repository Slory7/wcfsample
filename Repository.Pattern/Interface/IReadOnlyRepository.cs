using System.Collections.Generic;
using PetaPoco;
using Repository.Pattern.Infrastructure;

namespace Repository.Pattern.Interface
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class, IObjectState
    {
        TEntity SingleOrDefault(object primaryKey);
        T SingleOrDefault<T>(string whereClauses, params object[] args);

        TEntity FirstOrDefault(string whereClauses, params object[] args);
        T FirstOrDefault<T>(string whereClauses, params object[] args);

        int CountRecord(string whereClauses, params object[] args);

        IEnumerable<TEntity> Query(string whereClauses, params object[] args);
        List<TEntity> Fetch(string whereClauses, params object[] args);
        List<T> Fetch<T>(string whereClauses, params object[] args);
        Page<TEntity> PagedQuery(long pageNumber, long itemsPerPage, string sql, params object[] args);
    }
}

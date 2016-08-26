using System.Collections.Generic;
using PetaPoco;
using Repository.Pattern.Infrastructure;

namespace Repository.Pattern.Interface
{
    public interface IRepository<TEntity> where TEntity : class, IObjectState
    {
        TEntity SingleOrDefault(object primaryKey);

        int Count(string whereClauses, params object[] args);

        IEnumerable<TEntity> Query(string whereClauses, params object[] args);
        List<TEntity> Fetch(string whereClauses, params object[] args);
        List<T> Fetch<T>(string whereClauses, params object[] args);
        Page<TEntity> PagedQuery(long pageNumber, long itemsPerPage, string sql, params object[] args);
        object Insert(TEntity itemToAdd);
        int Update(TEntity itemToUpdate);
        int Update(TEntity poco, IEnumerable<string> columns);
        int Delete(TEntity itemToDelete);
        int Delete(object primaryKey);
    }
}

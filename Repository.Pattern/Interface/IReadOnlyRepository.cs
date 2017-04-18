using System.Collections.Generic;
using PetaPoco;
using Repository.Pattern.Infrastructure;

namespace Repository.Pattern.Interface
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class, IObjectState
    {
        //IReadOnlyDataContext Database { get; }
        TEntity SingleOrDefault(object primaryKey);
        TEntity SingleOrDefault(string whereClauses, params object[] args);
        T SingleOrDefault<T>(string whereClauses, params object[] args);

        TEntity FirstOrDefault(string whereClauses, params object[] args);
        T FirstOrDefault<T>(string whereClauses, params object[] args);
        
        int CountRecord(string whereClauses, params object[] args);

        IEnumerable<TEntity> Query(string whereClauses, params object[] args);

        List<TEntity> Fetch(string whereClauses, params object[] args);
        List<T> Fetch<T>(string whereClauses, params object[] args);
        Paged<TEntity> PagedQuery(long pageNumber, long itemsPerPage, string sql, params object[] args);

        /// <summary>
        ///     Checks for the existence of a row with the specified primary key value.
        /// </summary>
        /// <typeparam name="T">The Type representing the table being queried</typeparam>
        /// <param name="primaryKey">The primary key value to look for</param>
        /// <returns>True if a record with the specified primary key value exists.</returns>
        bool Exists(object primaryKey);

        /// <summary>
        ///     Checks for the existence of a row matching the specified condition
        /// </summary>
        /// <typeparam name="T">The Type representing the table being queried</typeparam>
        /// <param name="whereClauses">The SQL expression to be tested for (ie: the WHERE expression)</param>
        /// <param name="args">Arguments to any embedded parameters in the SQL statement</param>
        /// <returns>True if a record matching the condition is found.</returns>
        bool Exists(string whereClauses, params object[] args);
    }
}

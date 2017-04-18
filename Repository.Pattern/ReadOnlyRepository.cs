using System.Collections.Generic;

using Repository.Pattern.Interface;
using Repository.Pattern.Infrastructure;

namespace Repository.Pattern
{
    public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class, IObjectState
    {
        private readonly IReadOnlyUnitOfWork _unitOfWork;
        public ReadOnlyRepository(IReadOnlyUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //public IReadOnlyDataContext Database { get { return _unitOfWork.Database; } }
        //const string cachePrefix = "NIS_";
        //public TEntity SingleOrDefault(object primaryKey)
        //{
        //    TEntity result = null;
        //    string strTableName = typeof(TEntity).Name;
        //    if (CacheManager.Instance.GetDBCachePolicy().CanUseRowCache(strTableName))
        //    {
        //        string strCacheKey = cachePrefix + strTableName + "_row_" + primaryKey.ToString();
        //        result = CacheManager.Instance.GetCachedData<TEntity>(strCacheKey, CachePriority.Normal, () =>
        //        {
        //            return _unitOfWork.Database.SingleOrDefault<TEntity>(primaryKey);
        //        });
        //    }
        //    else
        //    {
        //        result = _unitOfWork.Database.SingleOrDefault<TEntity>(primaryKey);
        //    }
        //    return result;
        //}
        public TEntity SingleOrDefault(object primaryKey)
        {
            return _unitOfWork.Database.SingleOrDefault<TEntity>(primaryKey);
        }

        public TEntity SingleOrDefault(string whereClauses, params object[] args)
        {
            string strTableName = typeof(TEntity).Name;
            var sql = "SELECT * FROM " + strTableName;
            if (whereClauses != null) sql += " " + whereClauses;
            return _unitOfWork.Database.SingleOrDefault<TEntity>(sql, args);
        }
        public T SingleOrDefault<T>(string whereClauses, params object[] args)
        {
            string strTableName = typeof(TEntity).Name;
            var sql = "SELECT * FROM " + strTableName;
            if (whereClauses != null) sql += " " + whereClauses;
            return _unitOfWork.Database.SingleOrDefault<T>(sql, args);
        }
        public TEntity FirstOrDefault(string whereClauses, params object[] args)
        {
            string strTableName = typeof(TEntity).Name;
            var sql = "SELECT * FROM " + strTableName;
            if (whereClauses != null) sql += " " + whereClauses;
            return _unitOfWork.Database.FirstOrDefault<TEntity>(sql, args);
        }
        public T FirstOrDefault<T>(string whereClauses, params object[] args)
        {
            string strTableName = typeof(TEntity).Name;
            var sql = "SELECT * FROM " + strTableName;
            if (whereClauses != null) sql += " " + whereClauses;
            return _unitOfWork.Database.FirstOrDefault<T>(sql, args);
        }
        public IEnumerable<TEntity> Query(string whereClauses, params object[] args)
        {
            string strTableName = typeof(TEntity).Name;
            var sql = "SELECT * FROM " + strTableName;
            if (whereClauses != null) sql += " " + whereClauses;
            return _unitOfWork.Database.Query<TEntity>(sql, args);
        }
        //public List<TEntity> Fetch(string whereClauses, params object[] args)
        //{
        //    List<TEntity> result = null;
        //    string strTableName = typeof(TEntity).Name;
        //    strTableName = strTableName.Remove(0, strTableName.IndexOf('.') + 1);//remove dbo.
        //    if (whereClauses == null && CacheManager.Instance.GetDBCachePolicy().CanUseTotalCache(strTableName))
        //    {
        //        string strCacheKey = cachePrefix + strTableName;
        //        result = CacheManager.Instance.GetCachedData<List<TEntity>>(strCacheKey, CachePriority.Normal, () =>
        //        {
        //            var sql = "SELECT * FROM " + strTableName;
        //            return _unitOfWork.Database.Fetch<TEntity>(sql);
        //        });
        //    }
        //    else
        //    {
        //        var sql = "SELECT * FROM " + strTableName;
        //        if (whereClauses != null) sql += " " + whereClauses;
        //        result = _unitOfWork.Database.Fetch<TEntity>(sql, args);
        //    }
        //    return result;
        //}
        public List<TEntity> Fetch(string whereClauses, params object[] args)
        {
            string strTableName = typeof(TEntity).Name;
            var sql = "SELECT * FROM " + strTableName;
            if (whereClauses != null) sql += " " + whereClauses;
            return _unitOfWork.Database.Fetch<TEntity>(sql, args);
        }
        public List<T> Fetch<T>(string whereClauses, params object[] args)
        {
            string strTableName = typeof(TEntity).Name;
            var sql = "SELECT * FROM " + strTableName;
            if (whereClauses != null) sql += " " + whereClauses;
            return _unitOfWork.Database.Fetch<T>(sql, args);
        }
        public int CountRecord(string whereClauses, params object[] args)
        {
            string strTableName = typeof(TEntity).Name;
            var sql = "SELECT Count(*) FROM " + strTableName;
            if (whereClauses != null) sql += " " + whereClauses;
            return _unitOfWork.Database.ExecuteScalar<int>(sql, args);
        }

        public Paged<TEntity> PagedQuery(long pageNumber, long itemsPerPage, string sql, params object[] args)
        {
            var result = _unitOfWork.Database.Paged<TEntity>(pageNumber, itemsPerPage, sql, args);
            var objPaged = new Paged<TEntity>()
            {
                Context = result.Context,
                CurrentPage = result.CurrentPage,
                Items = result.Items,
                ItemsPerPage = result.ItemsPerPage,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages
            };
            return objPaged;
        }

        public bool Exists(object primaryKey)
        {
            return _unitOfWork.Database.Exists<TEntity>(primaryKey);
        }

        public bool Exists(string whereClauses, params object[] args)
        {
            return _unitOfWork.Database.Exists<TEntity>(whereClauses, args);
        }
    }
}
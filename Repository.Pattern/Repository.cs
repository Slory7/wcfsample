using System;
using System.Collections.Generic;

using PetaPoco;

using Repository.Pattern.Interface;
using Repository.Pattern.Infrastructure;

namespace Repository.Pattern
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IObjectState
    {
        private readonly IUnitOfWork _unitOfWork;
        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public TEntity SingleOrDefault(object primaryKey)
        {
            return _unitOfWork.Database.SingleOrDefault<TEntity>(primaryKey);
        }
        public IEnumerable<TEntity> Query(string whereClauses, params object[] args)
        {
            var pd = PocoData.ForType(typeof(TEntity), _unitOfWork.Database.DefaultMapper);
            var sql = "SELECT * FROM " + pd.TableInfo.TableName;
            if (whereClauses != null) sql += " " + whereClauses;
            return _unitOfWork.Database.Query<TEntity>(sql, args);
        }
        public List<TEntity> Fetch(string whereClauses, params object[] args)
        {
            var pd = PocoData.ForType(typeof(TEntity), _unitOfWork.Database.DefaultMapper);
            var sql = "SELECT * FROM " + pd.TableInfo.TableName;
            if (whereClauses != null) sql += " " + whereClauses;
            return _unitOfWork.Database.Fetch<TEntity>(sql, args);
        }
        public List<T> Fetch<T>(string whereClauses, params object[] args)
        {
            var pd = PocoData.ForType(typeof(TEntity), _unitOfWork.Database.DefaultMapper);
            var sql = "SELECT * FROM " + pd.TableInfo.TableName;
            if (whereClauses != null) sql += " " + whereClauses;
            return _unitOfWork.Database.Fetch<T>(sql, args);
        }
        public int Count(string whereClauses, params object[] args)
        {
            var pd = PocoData.ForType(typeof(TEntity), _unitOfWork.Database.DefaultMapper);
            var sql = "SELECT Count(*) FROM " + pd.TableInfo.TableName;
            if (whereClauses != null) sql += " " + whereClauses;
            return _unitOfWork.Database.ExecuteScalar<int>(sql, args);
        }

        public Page<TEntity> PagedQuery(long pageNumber, long itemsPerPage, string sql, params object[] args)
        {
            return _unitOfWork.Database.Page<TEntity>(pageNumber, itemsPerPage, sql, args);
        }
        public object Insert(TEntity poco)
        {
            return _unitOfWork.Database.Insert(poco);
        }
        public int Update(TEntity poco)
        {
            return _unitOfWork.Database.Update(poco);
        }
        public int Update(TEntity poco, IEnumerable<string> columns)
        {
            return _unitOfWork.Database.Update(poco, columns);
        }
        public int Delete(TEntity poco)
        {
            return _unitOfWork.Database.Delete(poco);
        }
        public int Delete(object primaryKey)
        {
            return _unitOfWork.Database.Delete(primaryKey);
        }
    }
}
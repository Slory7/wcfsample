using System;
using System.Collections.Generic;

using PetaPoco;

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
        public TEntity SingleOrDefault(object primaryKey)
        {
            return _unitOfWork.Database.SingleOrDefault<TEntity>(primaryKey);
        }
        public T SingleOrDefault<T>(string whereClauses, params object[] args)
        {
            var pd = PocoData.ForType(typeof(TEntity), _unitOfWork.Database.DefaultMapper);
            var sql = "SELECT * FROM " + pd.TableInfo.TableName;
            if (whereClauses != null) sql += " " + whereClauses;
            return _unitOfWork.Database.SingleOrDefault<T>(sql, args);
        }
        public TEntity FirstOrDefault(string whereClauses, params object[] args)
        {
            var pd = PocoData.ForType(typeof(TEntity), _unitOfWork.Database.DefaultMapper);
            var sql = "SELECT * FROM " + pd.TableInfo.TableName;
            if (whereClauses != null) sql += " " + whereClauses;
            return _unitOfWork.Database.FirstOrDefault<TEntity>(sql, args);
        }
        public T FirstOrDefault<T>(string whereClauses, params object[] args)
        {
            var pd = PocoData.ForType(typeof(TEntity), _unitOfWork.Database.DefaultMapper);
            var sql = "SELECT * FROM " + pd.TableInfo.TableName;
            if (whereClauses != null) sql += " " + whereClauses;
            return _unitOfWork.Database.FirstOrDefault<T>(sql, args);
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
        public int CountRecord(string whereClauses, params object[] args)
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
    }
}
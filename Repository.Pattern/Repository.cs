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
        //public IDataContext Database { get { return _unitOfWork.Database; } }
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
        public int Update(string setClauses, string whereClauses, params object[] args)
        {
            string strTableName = typeof(TEntity).Name;
            string strSQL = "Update " + strTableName + " " + setClauses + " " + whereClauses;
            return _unitOfWork.Database.Execute(strSQL, args);
        }
        public int Delete(object primaryKey)
        {
            return _unitOfWork.Database.Delete(primaryKey);
        }
        public int Delete(string whereClauses, params object[] args)
        {
            string strTableName = typeof(TEntity).Name;
            string strSQL = "Delete " + strTableName + " " + whereClauses;
            return _unitOfWork.Database.Execute(strSQL, args);
        }

    }
}
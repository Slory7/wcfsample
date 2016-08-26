using Business.Core.Interfaces;
using Framework.Core.Reflection;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Interface;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.BaseManager
{
    public abstract class BaseBizManager<TEntity> : IBizManager<TEntity> where TEntity : class, IObjectState
    {
        IReadOnlyRepository<TEntity> _repositoryReadOnly;
        IRepository<TEntity> _repository;

        public BaseBizManager(IReadOnlyRepository<TEntity> repositoryReadOnly
            , IRepository<TEntity> repository
            )
        {
            _repositoryReadOnly = repositoryReadOnly;
            _repository = repository;
        }

        public abstract string InfoName { get; }

        public abstract void InitObject(TEntity item);

        public virtual string CheckDataValid(TEntity item, DBOperation operation)
        {
            var validatorType = typeof(IDataValidator<TEntity>);
            TypeLocator objTypeLocator = new TypeLocator();
            var types = objTypeLocator.GetAllMatchingTypes((t) =>
            {
                return t.IsClass && !t.IsAbstract && validatorType.IsAssignableFrom(t);
            });
            foreach (var valType in types)
            {
                var valObj = Activator.CreateInstance(valType) as IDataValidator<TEntity>;
                string strCheckResult = valObj.CheckValid(item, operation);
                if (strCheckResult != null) return strCheckResult;
            }
            return null;
        }

        public virtual bool HasPermission()
        {
            //TODO: check permission
            return true;
        }

        public virtual TEntity SingleOrDefault(object primaryKey)
        {
            return _repositoryReadOnly.SingleOrDefault(primaryKey);
        }

        public virtual int CountRecord()
        {
            return _repositoryReadOnly.CountRecord(null);
        }

        public virtual IEnumerable<TEntity> Query()
        {
            return _repositoryReadOnly.Query(null);
        }

        public virtual List<TEntity> Fetch()
        {
            return _repositoryReadOnly.Fetch(null);
        }

        public virtual ResultData<object> Insert(TEntity itemToAdd)
        {
            var result = new ResultData<object>();

            InitObject(itemToAdd);

            string strCheckResult = CheckDataValid(itemToAdd, DBOperation.Insert);
            if (strCheckResult == null)
            {
                result.Result = _repository.Insert(itemToAdd);
                if (result.Result == null)
                    result.Status = ResultStatus.Error;
            }
            else
            {
                result.Status = ResultStatus.BadData;
                result.Message = strCheckResult;
            }
            return result;
        }

        public virtual ResultData<int> Update(TEntity itemToUpdate)
        {
            var result = new ResultData<int>();
            string strCheckResult = CheckDataValid(itemToUpdate, DBOperation.Update);
            if (strCheckResult == null)
            {
                result.Result = _repository.Update(itemToUpdate);
                if (result.Result <= 0)
                    result.Status = ResultStatus.Error;
            }
            else
            {
                result.Status = ResultStatus.BadData;
                result.Message = strCheckResult;
            }
            return result;
        }

        public virtual ResultData<int> UpdateByColumns(TEntity poco)
        {
            var result = new ResultData<int>();
            string strCheckResult = CheckDataValid(poco, DBOperation.Update);
            if (strCheckResult == null)
            {
                result.Result = _repository.Update(poco);
                if (result.Result <= 0)
                    result.Status = ResultStatus.Error;
            }
            else
            {
                result.Status = ResultStatus.BadData;
                result.Message = strCheckResult;
            }
            return result;
        }

        public virtual ResultData<int> Delete(TEntity itemToDelete)
        {
            var result = new ResultData<int>();
            string strCheckResult = CheckDataValid(itemToDelete, DBOperation.Delete);
            if (strCheckResult == null)
            {
                result.Result = _repository.Update(itemToDelete);
                if (result.Result <= 0)
                    result.Status = ResultStatus.Error;
            }
            else
            {
                result.Status = ResultStatus.BadData;
                result.Message = strCheckResult;
            }
            return result;
        }

        public virtual ResultData<int> Delete(object primaryKey)
        {
            var result = new ResultData<int>();
            result.Result = _repository.Delete(primaryKey);
            return result;
        }
    }
}

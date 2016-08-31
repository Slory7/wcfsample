using Repository.Pattern.Infrastructure;
using Repository.Pattern.Interface;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Interfaces
{
    public interface IManager<TEntity>
    {
        string InfoName { get; }
        void InitObject(TEntity item);
        string CheckDataValid(TEntity item, DBOperation operation);
        bool HasPermission();

        TEntity SingleOrDefault(object primaryKey);
        int CountRecord();
        IEnumerable<TEntity> Query();
        List<TEntity> Fetch();
        ResultData<object> Insert(TEntity itemToAdd);
        ResultData<int> Update(TEntity itemToUpdate);
        ResultData<int> UpdateByColumns(TEntity poco);
        ResultData<int> Delete(TEntity itemToDelete);
        ResultData<int> Delete(object primaryKey);
        ResultData<object> ProcessBizFlow(TEntity source, string bizType);
    }
}

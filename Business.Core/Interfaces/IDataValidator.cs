using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Interfaces
{
    public interface IDataValidator<TEntity>
    {
        int Order { get; }
        bool IsEnabled { get; }
        string CheckValid(TEntity item, DBOperation operation);
    }    
}

﻿using Repository.Pattern.Infrastructure;
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
    public interface IBizManager<TEntity>
    {
        string InfoName { get; }
        void InitObject(TEntity item);
        string CheckDataValid(TEntity item, DBOperation operation);
        bool HasPermission();

        ResultData<object> ProcessBizFlow(TEntity source, string bizType);
    }
}

﻿using Business.Core.Interfaces;
using Framework.Core.Logging;
using Framework.Core.Reflection;
using Microsoft.Practices.Unity;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Interface;
using Service.Contracts;
using Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.BaseManager
{
    public abstract class BaseBizManager<TEntity> : IBizManager<TEntity> where TEntity : class
    {
        public virtual string InfoName { get { return typeof(TEntity).Name; } }

        public virtual void InitObject(TEntity item) { }

        public virtual string CheckDataValid(TEntity item, DBOperation operation)
        {
            var validatorType = typeof(IDataValidator<TEntity>);
            var types = Assembly.GetCallingAssembly().GetTypes().Where((t) =>
            {
                return t.IsClass && !t.IsAbstract && validatorType.IsAssignableFrom(t);
            });
            var lstTypeInstances = new List<IDataValidator<TEntity>>(types.Count());
            foreach (var valType in types)
            {
                var valObj = ServiceGlobals.UnityContainer.Resolve(valType) as IDataValidator<TEntity>;
                lstTypeInstances.Add(valObj);
            }
            var canUseInstances = lstTypeInstances.Where(t => t.IsEnabled).OrderBy(t => t.Order);
            foreach (var valObj in canUseInstances)
            {
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

        public virtual ResultData<object> ProcessBizFlow(TEntity source, string bizType)
        {
            var result = new ResultData<object>();
            string strCheckResult = CheckDataValid(source, DBOperation.Complex);
            if (strCheckResult != null)
            {
                result.Message = strCheckResult;
                result.Status = ResultStatus.BadData;
                return result;
            }

            var bizStepType = typeof(IBizFlowStep<TEntity>);
            var types = Assembly.GetCallingAssembly().GetTypes().Where((t) =>
            {
                return t.IsClass && !t.IsAbstract && bizStepType.IsAssignableFrom(t);
            });
            var lstTypeInstances = new List<IBizFlowStep<TEntity>>(types.Count());
            foreach (var valType in types)
            {
                var valObj = ServiceGlobals.UnityContainer.Resolve(valType) as IBizFlowStep<TEntity>;
                lstTypeInstances.Add(valObj);
            }
            var canUseInstances = lstTypeInstances.Where(t => t.IsEnabled && t.BizType == bizType).OrderBy(t => t.StepNumber);
            var serialIntances = canUseInstances.Where(t => !t.AllowParallel);
            int nLastStep = 0;
            object lastStepData = null;
            foreach (var stepObj in serialIntances)
            {
                if (stepObj.StepNumber >= nLastStep)
                {
                    ResultData<object> stepResult = null;
                    try
                    {
                        stepResult = stepObj.ProcessStep(source, lastStepData);
                        result = stepResult;
                    }
                    catch (Exception e)
                    {
                        result = new ResultData<object>() { Status = ResultStatus.Error, Message = e.Message };
                        Log.Instance.LogError(e);
                        break;
                    }

                    if (stepResult.Status == ResultStatus.Success)
                    {
                        lastStepData = stepResult.Data;
                        if (stepObj.NextStep == -1)
                            break;
                        else if (stepObj.NextStep > 0)
                            nLastStep = stepObj.NextStep;
                        else
                            nLastStep = stepObj.StepNumber;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (nLastStep > 0)
            {
                var parallelIntances = canUseInstances.Where(t => t.AllowParallel && t.StepNumber <= nLastStep);
                Parallel.ForEach(parallelIntances, (stepObj) =>
                {
                    stepObj.ProcessStep(source, lastStepData);
                });
            }
            return result;
        }
    }
}

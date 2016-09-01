using System;
namespace Service.Core
{
    public interface IServiceGlobals
    {
        /// <summary>
        /// 是否生产环境
        /// </summary>
        //bool IsProductMode { get; }
        //int CurrentUserSchoolID { get; }
        //string CurrentUserID { get; }
        //string CurrentDisplayName { get; }

        string CurrentUserSessionID { get; }

    }
}

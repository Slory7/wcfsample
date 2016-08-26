using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public class Constants
    {
        public static string GetResultStatusString(ResultStatus status)
        {
            if (status == ResultStatus.Success)
                return "操作成功";
            else if (status == ResultStatus.NotFound)
                return "数据未找到";
            else if (status == ResultStatus.BadData)
                return "提交的数据不正确";
            else if (status == ResultStatus.Forbidden)
                return "此操作不被接受";
            else if (status == ResultStatus.BadLogic)
                return "业务逻辑不正确";
            else if (status == ResultStatus.Error)
                return "服务器出现错误";
            else if (status == ResultStatus.Conflict)
                return "数据操作冲突";
            else if (status == ResultStatus.Unauthorized)
                return "此操作未授权";

            return null;
        }        
    }
}

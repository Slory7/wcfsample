using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public enum ResultStatus
    {
        Success = 1,
        NotFound,
        Forbidden,
        Unauthorized,
        Conflict,
        BadData,
        BadLogic,
        Error
    }   
}

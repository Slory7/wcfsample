using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace Framework.Core.Logging.WCFLogger
{
    public class Log4NetErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            Log.Instance.LogError("unexpected wcf error.", error);

            return false; // Exception has to pass the stack further
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
        }
    }
}

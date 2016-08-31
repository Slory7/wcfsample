using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;

namespace Framework.Core.Logging.WCFLogger
{
    public class Log4NetBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(Log4NetServiceBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new Log4NetServiceBehavior();
        }
    }
}

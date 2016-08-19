using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    [HostService]
    public class Service2 : IService2
    {
        public string GetData(int value)
        {
            if (OperationContext.Current != null)
            {
                //var header = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("Authorization", "http://yournamespace.com/v1");
                var requestHttpMessage = (HttpRequestMessageProperty)OperationContext.Current.IncomingMessageProperties[HttpRequestMessageProperty.Name];
            }
            return string.Format("You entered: {0}", value);
        }

        public CompositeType2 GetDataUsingDataContract(CompositeType2 composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}

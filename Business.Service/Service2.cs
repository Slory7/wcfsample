using DevTrends.WCFDataAnnotations;
using Service.Core;
using Service.Contracts;
using Service.Contracts.Services;
using Service.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;

namespace Business.Service
{
    [HostService]
    [ValidateDataAnnotationsBehavior]
    public class Service2 : IService2
    {
        public Service2()
        {
        }
        public string GetData(int value)
        {
            if (OperationContext.Current != null)
            {
                //var header = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("Authorization", "http://yournamespace.com/v1");
                var requestHttpMessage = (HttpRequestMessageProperty)OperationContext.Current.IncomingMessageProperties[HttpRequestMessageProperty.Name];
            }
            return string.Format("You entered: {0}", value);
        }

        public ResultData<CompositeType2> GetDataUsingDataContract(CompositeType2 composite)
        {
            var result = new ResultData<CompositeType2>();

            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }

            result.Result = composite;

            return result;
        }
    }
}

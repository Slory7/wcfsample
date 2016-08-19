using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace WcfClient1
{
    public partial class Service2Client:IDisposable
    {
        static IService2 client = null;

        public static IService2 Instance()
        {
            if (client == null)
            {
                BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
                //binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;

                string strServerUrl = System.Configuration.ConfigurationManager.AppSettings["SVCServerUrl"];

                EndpointAddress remoteAddress = new EndpointAddress(strServerUrl + "/Service2.svc");

                var myChannelFactory = new ChannelFactory<IService2>(binding, remoteAddress);

                //myChannelFactory.Credentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;

                client = myChannelFactory.CreateChannel();

                new System.ServiceModel.OperationContextScope((IContextChannel)client);
                //MessageHeader headAuth = MessageHeader.CreateHeader("Authorization", "http://yournamespace.com/v1", "aa");
                //OperationContext.Current.OutgoingMessageHeaders.Add(headAuth);
                HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
                requestMessage.Headers["accept"] = "application/json";
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;

            }
            return client;
        }

        public void Dispose()
        {
            if (client != null)
            {
                ((IChannel)client).Close();
            }
        }
    }
}

using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Client.Core
{
    public partial class ClientService : IDisposable
    {
        private Dictionary<string, IChannel> clients = new Dictionary<string, IChannel>();

        public IService GetClient<IService>()
        {
            var serviceName = typeof(IService).Name.TrimStart('I', 'i');
            IChannel client = clients.ContainsKey(serviceName) ? clients[serviceName] : null;
            if (client == null)
            {
                BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None)
                {
                    MaxReceivedMessageSize = 2 * 1024 * 1024 * 8//能收到2M的数据包
                };
                //binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;

                string strServerUrl = System.Configuration.ConfigurationManager.AppSettings["SVCServerUrl"];

                EndpointAddress remoteAddress = new EndpointAddress(strServerUrl + "/" + serviceName + ".svc");

                var myChannelFactory = new ChannelFactory<IService>(binding, remoteAddress);

                //myChannelFactory.Credentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;

                client = (IChannel)myChannelFactory.CreateChannel();

                clients.Add(serviceName, client);
            }

            new System.ServiceModel.OperationContextScope((IContextChannel)client);
            //MessageHeader headAuth = MessageHeader.CreateHeader("Authorization", "http://yournamespace.com/v1", "aa");
            //OperationContext.Current.OutgoingMessageHeaders.Add(headAuth);
            HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
            requestMessage.Headers["UserSessionID"] = Guid.NewGuid().ToString();
            OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;

            return (IService)client;
        }

        public void Dispose()
        {
            foreach (var client in clients.Values)
            {
                if (client != null)
                {
                    client.Close();
                }
            }
        }
    }
}

using Contracts;
using System.ServiceModel;

namespace Client_App
{
    public static class Connection
    {
        public static IService_1 GetProxy()
        {
            NetTcpBinding binding = new NetTcpBinding();
            ChannelFactory<IService_1> channelFactory = new ChannelFactory<IService_1>(binding, new EndpointAddress("net.tcp://127.255.0.0:10100/InputRequest"));
            IService_1 proxy = channelFactory.CreateChannel();
            return proxy;
        }

    }
}

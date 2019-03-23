using Contracts;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Diagnostics;
using System.ServiceModel;

namespace JobWorker
{
    class Service2 : IService_2
    {
        public void DoSomthigElse(string someString)
        {
            var id = RoleEnvironment.CurrentRoleInstance.Id;
            Trace.TraceInformation(someString + "  " + id);

            NetTcpBinding binding = new NetTcpBinding();
            EndpointAddress endpointAddress = new EndpointAddress("net.tcp://" + RoleEnvironment.Roles["JobWorker2"].Instances[0].InstanceEndpoints["InternalRequest"].IPEndpoint + "/InternalRequest");
            ChannelFactory<IService_2> factory = new ChannelFactory<IService_2>(binding, endpointAddress);

            IService_2 proxy = factory.CreateChannel()                ;
            proxy.DoSomthigElse("\n\tMiddle role id is" + id + ". Message>>" + someString + "<<");
        }
    }
}

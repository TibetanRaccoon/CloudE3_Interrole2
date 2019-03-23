using Contracts;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;

namespace JobWorker
{
    class Service1 : IService_1
    {
        public void DoSomethig(string someString)
        {
            var id = RoleEnvironment.CurrentRoleInstance.Id;
            var roleCount = RoleEnvironment.Roles.Count;
            var roles = new List<RoleInstance>();
            var name = "InternalRequest";

            foreach (var role in RoleEnvironment.Roles[RoleEnvironment.CurrentRoleInstance.Role.Name].Instances)
            {
                roles.Add(role);
            }

            for (int i = 0; i < roles.Count; i++)
            {
                if (roles[i].Id == id)
                {
                    if (++i > roles.Count - 1)
                        i = 0;

                    var binding = new NetTcpBinding();
                    var endpoint = new EndpointAddress(string.Format("net.tcp://{0}/{1}",
                                                                roles[i].InstanceEndpoints[name].IPEndpoint,
                                                                name));

                    var factory = new ChannelFactory<IService_2>(binding, endpoint);
                    var proxy = factory.CreateChannel();

                    proxy.DoSomthigElse("\tSender id is: " + id + "Message: " + someString);


                    Trace.TraceInformation("Recived message:" + someString + ".\nTarget instance id is:" + i);
                }
            }
        }



    }
}

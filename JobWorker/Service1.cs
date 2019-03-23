using Contracts;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
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
            var roles = new Dictionary<int, RoleInstance>();
            var name = "InternalRequest";
            int targetId;
            string[] namespaces = id.Split('.');
            string[] acctualId = namespaces[namespaces.Length - 1].Split('_');
            Int32.TryParse(acctualId[acctualId.Length - 1], out int currentId);


            foreach (var role in RoleEnvironment.Roles[RoleEnvironment.CurrentRoleInstance.Role.Name].Instances)
            {
                // Split word to get instances IDs 
                namespaces = role.Id.Split('.');
                acctualId = namespaces[namespaces.Length - 1].Split('_');
                Int32.TryParse(acctualId[acctualId.Length - 1], out int tempId);
                roles.Add(tempId, role);
            }

            // Incrementing instance id
            if ((targetId = currentId + 1) == roles.Count)
                targetId = 0;

            Trace.TraceInformation("Recived message:" + someString + ".\nTarget instance id is:" + targetId + "  Curretn id : " + currentId);
            var binding = new NetTcpBinding();
            var endpoint = new EndpointAddress(string.Format("net.tcp://{0}/{1}",
                                                        roles[targetId].InstanceEndpoints[name].IPEndpoint,
                                                        name));

            var factory = new ChannelFactory<IService_2>(binding, endpoint);
            var proxy = factory.CreateChannel();

            proxy.DoSomthigElse("\tSender id is: " + id + "Message: " + someString);
        }



    }
}

using Contracts;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Diagnostics;
using System.ServiceModel;

namespace JobWorker2
{
    class JobServer2
    {
        ServiceHost host;
        public JobServer2()
        {
            var endpointName = "InternalRequest";

            var binding = new NetTcpBinding();
            var endpoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints[endpointName];
            var address = string.Format("net.tcp://{0}/{1}", endpoint.IPEndpoint, endpointName);

            host = new ServiceHost(typeof(Service2));
            host.AddServiceEndpoint(typeof(IService_2), binding, address);
            Open();
        }

        public void Open()
        {
            try
            {
                host.Open();
                Trace.TraceInformation("Open");
            }
            catch (Exception)
            {
                Trace.TraceInformation("Failed to open");
            }
        }

        public void Close()
        {
            try
            {
                host.Close();
            }
            catch (Exception)
            {
                Trace.TraceInformation("Failed to close");
            }
        }

    }
}

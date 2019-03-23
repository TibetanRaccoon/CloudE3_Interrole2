using Contracts;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Diagnostics;
using System.ServiceModel;

namespace JobWorker
{
    class JobServer1
    {
        ServiceHost host;
        public JobServer1()
        {
            var endpointName = "InputRequest";

            var binding = new NetTcpBinding();
            var endpoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints[endpointName];
            var address = string.Format("net.tcp://{0}/{1}", endpoint.IPEndpoint, endpointName);

            host = new ServiceHost(typeof(Service1));
            host.AddServiceEndpoint(typeof(IService_1), binding, address);
            Open();
        }

        public void Open()
        {
            try
            {
                host.Open();
                Trace.TraceInformation("Open service 1");

            }
            catch (Exception)
            {
                Trace.TraceInformation("Failed to open service 1");
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

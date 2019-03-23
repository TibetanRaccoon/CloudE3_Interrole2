using Contracts;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Diagnostics;

namespace JobWorker2
{
    class Service2 : IService_2
    {
        public void DoSomthigElse(string someString)
        {
            var id = RoleEnvironment.CurrentRoleInstance.Id;
            Trace.TraceInformation(string.Format("\nRecived message:>>{0}<< My id is: {1} Role name is: {2}",
                                                    someString,
                                                    id,
                                                    RoleEnvironment.CurrentRoleInstance.Role.Name));
        }
    }
}

using System.ServiceModel;

namespace Contracts
{
    [ServiceContract]
    public interface IService_1
    {
        [OperationContract]
        void DoSomethig(string someString);
    }
}

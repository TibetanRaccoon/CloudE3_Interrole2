using System.ServiceModel;

namespace Contracts
{
    [ServiceContract]
    public interface IService_2
    {
        [OperationContract]
        void DoSomthigElse(string someString);
    }
}

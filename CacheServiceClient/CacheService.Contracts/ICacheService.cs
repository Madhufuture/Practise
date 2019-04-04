using System.ServiceModel;

namespace CacheService.Contracts
{
    [ServiceContract]
    public interface ICacheService
    {
        [OperationContract]
        StudentDetails GetStudentDetails(int id);
    }
}

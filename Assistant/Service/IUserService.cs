using Service.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserService" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {
        IEnumerable<UserServiceModel> GetAllUsers();

        [OperationContract]
        int? CreateUser(UserServiceModel userModel);

        [OperationContract]
        void UpdateUser(UserServiceModel userModel);

        [OperationContract]
        void DeleteUser(int? Id);
    }
}

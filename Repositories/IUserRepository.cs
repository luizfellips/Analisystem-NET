using Analisystem.Models;

namespace Analisystem.Repositories
{
    public interface IUserRepository
    {
        UserModel getUserByID(int id);
        List<UserModel> getUsers();
        UserModel addUser(UserModel user);
        bool removeUser(UserModel user);
        UserModel updateUser(UserModel user);
        UserModel getUserByLogin(string login);
        UserModel getUserByLoginAndEmail(string login, string email);
        UserModel changePassword(ChangePasswordModel changePasswordModel);
        


    }
}

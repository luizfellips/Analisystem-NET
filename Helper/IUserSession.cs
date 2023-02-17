using Analisystem.Models;

namespace Analisystem.Helper
{
    public interface IUserSession
    {
        void CreateUserSession(UserModel user);
        void RemoveUserSession();
        UserModel GetUserSession();
    }
}

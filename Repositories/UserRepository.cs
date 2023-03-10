using Analisystem.Data;
using Analisystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Analisystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public UserModel addUser(UserModel user)
        {
            user.RegisterDate = DateTime.Now;
            user.SetHash();
            _databaseContext.Users.Add(user);
            _databaseContext.SaveChanges();
            return user;
        }

        public UserModel changePassword(ChangePasswordModel changePasswordModel)
        {
            UserModel userDB = getUserByID(changePasswordModel.Id);
            if (userDB == null) throw new Exception("An error occurred while updating the user password.");
            if (!userDB.ValidatePassword(changePasswordModel.OldPassword)) throw new Exception("Old password doesn't match.");
            if (userDB.ValidatePassword(changePasswordModel.NewPassword)) throw new Exception("New password must be different from the old password.");
            userDB.SetPassword(changePasswordModel.NewPassword);
            userDB.LastUpdated = DateTime.Now;
            _databaseContext.Users.Update(userDB);
            _databaseContext.SaveChanges();
            return userDB;
            
        }

        public UserModel getUserByID(int id)
        {
            return _databaseContext.Users.Find(id);
        }

        public UserModel getUserByLogin(string login)
        {
            return _databaseContext.Users.FirstOrDefault(x => x.Login == login);
        }

        public UserModel getUserByLoginAndEmail(string login, string email)
        {
            return _databaseContext.Users.FirstOrDefault(x => x.Login == login && x.Email == email);
        }

        public List<UserModel> getUsers()
        {
            return _databaseContext.Users.Include(x => x.Products).ToList();
        }

        public bool removeUser(UserModel user)
        {
            try
            {
                _databaseContext.Users.Remove(user);
                _databaseContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public UserModel updateUser(UserModel user)
        {
            /* EFCore maps the existing properties of the returnde database
            objects and preserves the changed properties of the constructor.*/
            UserModel userDB = getUserByID(user.Id);
            if (userDB == null) throw new Exception("An error occurred while updating the user.");
            userDB.Name = user.Name;
            userDB.Login = user.Login;
            userDB.Address = user.Address;
            userDB.Phone = user.Phone;
            userDB.CPF = user.CPF;
            userDB.ProfileLevel = user.ProfileLevel;
            userDB.LastUpdated = DateTime.Now;

            _databaseContext.Users.Update(userDB);
            _databaseContext.SaveChanges();
            return userDB;
        }
    }
}

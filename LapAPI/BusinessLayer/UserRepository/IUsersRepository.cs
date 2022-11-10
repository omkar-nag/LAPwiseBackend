using LapAPI.Models;

namespace LapAPI.BusinessLayer.UserRepository
{
    public interface IUsersRepository
    {
        IEnumerable<Users> GetAll();
        Users? GetById(int userId);
        Users? GetUserByUserName(string userName);
        Users? GetUserByUserNameAndPassword(AuthUserModel authUser);
        Users Insert(Users user);
        Task<Users> Update(int id, Users user);
        void Delete(int userId);
        void Save();
    }
}

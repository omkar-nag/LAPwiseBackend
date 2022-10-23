using LapAPI.Models;

namespace LapAPI.BusinessLayer.UserRepository
{
    public interface IUsersRepository
    {
        IEnumerable<Users> GetAll();
        Users? GetById(int userId);
        Users? GetUserByUserNameAndPassword(AuthUserModel authUser);
        void Insert(Users user);
        void Update(Users user);
        void Delete(int userId);
        void Save();
    }
}

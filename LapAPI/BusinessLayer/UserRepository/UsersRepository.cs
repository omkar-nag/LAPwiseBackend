using LapAPI.DataAccessLayer;
using LapAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LapAPI.BusinessLayer.UserRepository
{
    public class UsersRepository : IUsersRepository
    {

        private readonly LAPwiseDBContext _dbContext;

        public UsersRepository(LAPwiseDBContext context)
        {
            _dbContext = context;
        }
        public IEnumerable<Users> GetAll()
        {
            return _dbContext.Users.ToList();
        }
        public Users? GetById(int userId)
        {
            return _dbContext.Users.Find(userId);
        }

        public Users? GetUserByUserName(string userName)
        {
            return _dbContext.Users.Where(u => u.UserName == userName).SingleOrDefault();
        }

        public Users? GetUserByUserNameAndPassword(AuthUserModel authUser)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserName == authUser.UserName & u.Password == authUser.Password);
            return user;
        }

        public Users Insert(Users user)
        {
            _dbContext.Users.Add(user);
            this.Save();
            return user;
        }
        public Users Update(Users user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            this.Save();
            return user;
        }
        public void Delete(int userId)
        {
            Users? user = this.GetById(userId);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                this.Save();
            }
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
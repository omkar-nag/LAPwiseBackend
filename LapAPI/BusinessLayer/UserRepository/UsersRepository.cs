﻿using LapAPI.BusinessLayer.NotesRepository;
using LapAPI.DataAccessLayer;
using LapAPI.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<Users>Update(int id,Users user)
        {
            
            if (id != user.Id)
            {
                throw new ItemUpdateException();
            }

            _dbContext.Entry(user).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    throw new ItemNotFoundException();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult<Users>(null);

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

        private bool UserExists(int id)
        {
            return _dbContext.Users.Any(user => user.Id == id);
        }
    }
}
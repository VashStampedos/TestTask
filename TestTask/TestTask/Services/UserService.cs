using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class UserService:IUserService
    {
        
        ApplicationDbContext _dbContext;
        public UserService(ApplicationDbContext db) 
        {
            this._dbContext = db;
        }

        public async Task<User> GetUser()
        {
            var ordersGroupedByUserId =await _dbContext.Orders.AsNoTracking().GroupBy(o => o.UserId).ToListAsync();
            var user = ordersGroupedByUserId.MaxBy(x => x.Count());
            var user1 =await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == user.Key);

            return user1;
        }

        public async Task<List<User>> GetUsers()
        {
            var users =await _dbContext.Users.AsNoTracking().Where(x => x.Status == UserStatus.Inactive).ToListAsync();
           
            return users;
        }
    }
}

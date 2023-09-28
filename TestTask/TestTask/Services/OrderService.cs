using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class OrderService:IOrderService
    {
        ApplicationDbContext _dbContext;
        public OrderService(ApplicationDbContext db)
        {
            this._dbContext = db;
        }

        public async Task<Order> GetOrder()
        {
            var orders = await _dbContext.Orders.AsNoTracking().Select(x => new
            {
                id = x.Id,
                price = x.Price * x.Quantity,
            }).ToListAsync();
            var maxOrder = orders.MaxBy(x=> x.price);
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == maxOrder.id);
           
            return order;
        }

        public async Task<List<Order>> GetOrders()
        {
            var orders =await _dbContext.Orders.AsNoTracking().Where(x => x.Quantity > 10).ToListAsync();
            return orders;
        }
    }
}

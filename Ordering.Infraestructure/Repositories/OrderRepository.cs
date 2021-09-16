using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infraestructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infraestructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext context) : base(context) { }

        public async Task<IReadOnlyList<Order>> GetOrdersByUserName(string userName)
            => await context.Orders.Where(o => o.UserName == userName).ToListAsync();
    }
}

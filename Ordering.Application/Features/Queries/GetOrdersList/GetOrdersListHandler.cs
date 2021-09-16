using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Queries.GetOrdersList
{
    public class GetOrdersListHandler : IRequestHandler<GetOrdersListQuery, List<OrdersViewModel>>
    {
        private readonly IOrderRepository repository;
        private readonly IMapper mapper;

        public GetOrdersListHandler(IOrderRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<OrdersViewModel>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orderList = await repository.GetOrdersByUserName(request.UserName);
            return mapper.Map<List<OrdersViewModel>>(orderList);
        }
    }
}

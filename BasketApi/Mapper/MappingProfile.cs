using AutoMapper;
using BasketApi.Entities;
using EventBus.Messages.Events;

namespace BasketApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }
    }
}

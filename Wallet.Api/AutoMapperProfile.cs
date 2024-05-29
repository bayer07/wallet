using AutoMapper;
using Wallet.Api.Models;
using Wallet.Domain;

namespace Wallet.Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MessageModel, Message>().AfterMap((src, dst) => { dst.Date = DateTime.Now; });
        }
    }
}

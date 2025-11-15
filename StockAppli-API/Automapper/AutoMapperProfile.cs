using AutoMapper;
using StockAppli.Models.Entities;
using StockAppli.Models.Models;

namespace StockAppli_API.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUserModel, User>().ReverseMap();
        }
    }
}

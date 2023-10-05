using AutoMapper;
using BusinessObjects.Models;
using FUCarRentingSystem.DTO;

namespace FUCarRentingSystem.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CarDto, Car>().ReverseMap();
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<CarRentalDto, CarRental>().ReverseMap();
        }
    }
}

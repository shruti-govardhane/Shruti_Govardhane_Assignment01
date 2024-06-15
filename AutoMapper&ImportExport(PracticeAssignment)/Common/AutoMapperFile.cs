using AutoMapper;
using E_Commerce.DTO;
using E_Commerce.Entity;

namespace E_Commerce.Common
{
    public class AutoMapperFile:Profile
    {

        public AutoMapperFile() {

            CreateMap<ProductEntity,ProductDTO>().ReverseMap();
            CreateMap<CustomerEntity, CustomerDTO>().ReverseMap();

        }
    }
}

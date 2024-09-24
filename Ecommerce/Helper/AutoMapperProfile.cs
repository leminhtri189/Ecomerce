using AutoMapper;
using Ecommerce.Data;
using Ecommerce.ViewModels;

namespace Ecommerce.Helper
{
    public class AutoMapperProfile : Profile
    { 
        public AutoMapperProfile() {
            CreateMap<RegisterVM, KhachHang>();
             }
    }
}

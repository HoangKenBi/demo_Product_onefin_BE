using AutoMapper;
using demo_product.Entity;
using demo_product.Models;

namespace demo_product.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Account, AccountModel>().ReverseMap();
            CreateMap<Order, OrderModel>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailModel>().ReverseMap();
            CreateMap<OrderDetailVIew, OrderDetail>().ReverseMap();
        }
    }
}

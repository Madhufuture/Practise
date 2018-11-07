namespace ProductCatalogGateway.MapperProfiles
{
    using AutoMapper;
    using Models;
    using ProductCatalog.DataAccess;

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductsModel>();
            CreateMap<ProductsModel, Product>();
        }
    }
}
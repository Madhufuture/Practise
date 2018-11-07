namespace ProductCatalogGateway
{
    using ProductCatalog.DataAccess;

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ProductCatalogContext context) : base(context)
        {
        }
    }
}
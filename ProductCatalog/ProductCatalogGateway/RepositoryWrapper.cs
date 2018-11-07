namespace ProductCatalogGateway
{
    using ProductCatalog.DataAccess;

    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ProductCatalogContext _productContext;
        private IProductRepository _productRepository;

        public RepositoryWrapper(ProductCatalogContext context)
        {
            _productContext = context;
        }

        public IProductRepository Product =>
            _productRepository ?? (_productRepository = new ProductRepository(_productContext));
    }
}
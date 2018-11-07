namespace ProductCatalogGateway
{
    public interface IRepositoryWrapper
    {
        IProductRepository Product { get; }
    }
}
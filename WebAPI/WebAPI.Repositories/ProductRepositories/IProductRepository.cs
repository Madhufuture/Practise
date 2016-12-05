using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Repositories.GenericRepositories;

namespace WebAPI.Repositories.ProductRepositories
{
    public interface IProductRepository
    {
        IList<ProductEntity> GetAllProducts();
        ProductEntity GetProductByID(int ID);
        void AddProduct(ProductEntity entity);
        void UpdateProduct(ProductEntity entity);
        void DeleteProduct(ProductEntity entity);

    }
}

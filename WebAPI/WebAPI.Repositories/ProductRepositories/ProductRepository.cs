using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using WebAPI.Repositories.GenericRepositories;

namespace WebAPI.Repositories.ProductRepositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly IGenericRepository<ProductEntity> _productRep;

        public ProductRepository(IGenericRepository<ProductEntity> ProdEntity)
        {
            this._productRep = ProdEntity;
        }

        public void AddProduct(ProductEntity entity)
        {
            if (entity != null)
            {
                _productRep.AddProduct(entity);
            }
        }

        public void DeleteProduct(ProductEntity entity)
        {
            if (entity != null)
            {
                _productRep.DeleteProduct(entity);
            }
        }

        public IList<ProductEntity> GetAllProducts()
        {
            return _productRep.GetAllProducts();
        }

        public ProductEntity GetProductByID(int ID)
        {
           return _productRep.GetProductByID(c => c.ID == ID);
        }

        public void UpdateProduct(ProductEntity entity)
        {
            if (entity != null)
            {
                _productRep.UpdateProduct(entity);
            }
        }
    }
}

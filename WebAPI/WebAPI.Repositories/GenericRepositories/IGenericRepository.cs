using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Repositories.GenericRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        IList<T> GetAllProducts();
        T GetProductByID(Expression<Func<T,bool>> predicate);
        void AddProduct(params T[] items);
        void UpdateProduct(params T[] item);
        void DeleteProduct(params T[] item);

    }
}

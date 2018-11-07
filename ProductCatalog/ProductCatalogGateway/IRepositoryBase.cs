namespace ProductCatalogGateway
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Models;

    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> FindAll();
        Task<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task Create(T entity);
        Task Update(T entity);
        void Delete(T entity);
        Task Save();

        //Task<List<T>> GetallProducts();
        //Task<T> GetProductById(int id);
        //Task UpdateProduct(int id, T product);
        //Task InsertProduct(T product);

    }
}
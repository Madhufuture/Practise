using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Repositories.GenericRepositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public void AddProduct(params T[] items)
        {
            using (var context = new ProductContext())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Added;
                }
                context.SaveChanges();
            };
        }

        public void DeleteProduct(params T[] items)
        {
            using (var context = new ProductContext())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Deleted;
                }
                context.SaveChanges();
            }
        }

        public IList<T> GetAllProducts()
        {
            List<T> list;

            using (var context = new ProductContext())
            {
                IQueryable<T> query = context.Set<T>();
                list = query.AsNoTracking().ToList<T>();

            }
            return list;
        }

        public T GetProductByID(Expression<Func<T, bool>> predicate)
        {

            T retItem = null;
            using (var context = new ProductContext())
            {
                IQueryable<T> query = context.Set<T>().Where(predicate);
                retItem = query.AsNoTracking().FirstOrDefault();
            }
            return retItem;
        }

        public void UpdateProduct(params T[] items)
        {
            using (var context = new ProductContext())
            {
                foreach (var item in items)
                {
                    context.Entry(item).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
        }
    }
}

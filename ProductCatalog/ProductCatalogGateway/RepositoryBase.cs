namespace ProductCatalogGateway
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using ProductCatalog.DataAccess;

    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryBase(ProductCatalogContext context)
        {
            ProductCatalogContext = context;
        }

        public ProductCatalogContext ProductCatalogContext { get; set; }

        public async Task<IEnumerable<T>> FindAll()
        {
            var query = ProductCatalogContext.Set<T>().AsQueryable();
            foreach (var property in ProductCatalogContext.Model.FindEntityType(typeof(T)).GetNavigations())
                query = query.Include(property.Name);

            return await query.ToListAsync();
        }

        public async Task<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            var query = ProductCatalogContext.Set<T>().AsQueryable();

            foreach (var property in ProductCatalogContext.Model.FindEntityType(typeof(T)).GetNavigations())
                query = query.Include(property.Name);

            return await query.AsNoTracking().SingleOrDefaultAsync(expression);
        }

        public async Task Create(T entity)
        {
            ProductCatalogContext.Set<T>().Add(entity);
            await Save();
        }

        public async Task Update(T entity)
        {
            ProductCatalogContext.Set<T>().Attach(entity);
            var entry = ProductCatalogContext.Entry(entity);
            entry.State = EntityState.Modified;
            await Save();
        }

        public void Delete(T entity)
        {
            ProductCatalogContext.Set<T>().Remove(entity);
        }

        public async Task Save()
        {
            try
            {
                await ProductCatalogContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                    if (entry.Entity is Product)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = await entry.GetDatabaseValuesAsync().ConfigureAwait(false);
                        foreach (var property in proposedValues.Properties)
                        {
                            var proposedValue = proposedValues[property];
                            var databaseValue = databaseValues[property];

                            proposedValues[property] = proposedValue;
                        }

                        entry.OriginalValues.SetValues(proposedValues);
                    }
                    else
                    {
                        throw new NotSupportedException(
                            $"Concurrency conflits while saving the record {entry.Metadata.Name}");
                    }
            }
        }
    }
}
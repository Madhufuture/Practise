namespace DataAccess.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.ProductContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccess.ProductContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            var products = new List<ProductEntity>
            {
                new ProductEntity {ID =1,Name="Apple",Category="Fruit", Price=20},
                new ProductEntity {ID=2,Name="Banana",Category="Fruit",Price=30 },
                new ProductEntity {ID=3,Name="Carrot",Category="Vegetable",Price=40 }
            };

            products.ForEach(s => context.Products.Add(s));
            context.SaveChanges();

        }
    }
}

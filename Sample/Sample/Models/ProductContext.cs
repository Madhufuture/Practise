using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Sample.Models
{
    public class ProductContext: DbContext
    {
        public DbSet<Products> Products { get; set; }
    }
}
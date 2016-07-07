using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BooksAPI.Models
{
    public class BooksAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BooksAPIContext() : base("name=BooksAPIContext")
        {
        }

        public System.Data.Entity.DbSet<BooksAPI.Models.Book> Books { get; set; }

        public System.Data.Entity.DbSet<BooksAPI.Models.Author> Authors { get; set; }
    }
}

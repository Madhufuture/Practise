using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BooksAPI.Models
{
    public class Author
    {
        public int AuthorID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
using BooksAPI.DTO;
using BooksAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace BooksAPI.Repositories
{
    public class BookRepository:IDisposable,IBookRepository
    {
        private BooksAPIContext db = new BooksAPIContext();


        private static readonly Expression<Func<Book, BookDto>> AsBooksDto =
            x => new BookDto
            {
                Author = x.Author.Name,
                Genre = x.Genre,
                Title = x.Title
            };

        private static readonly Expression<Func<Book, BookDetailDto>> AsBookDetailsDto =
            x => new BookDetailDto
            {
                Author = x.Author.Name,
                Description = x.Description,
                Genre = x.Genre,
                Price = x.Price,
                PublishDate = x.PublishDate,
                Title = x.Title
            };

        public IEnumerable<BookDto> GetBooks()
        {
            return db.Books.Include(a => a.Author).Select(AsBooksDto);
        }

        public BookDto GetBook(int id)
        {
            var bookDto = db.Books.Include(b => b.Author)
                .Where(b => b.BookID == id)
                .Select(AsBooksDto).FirstOrDefault();
                
            return bookDto;
        }

        public IEnumerable<BookDetailDto> GetBookDetails(int id)
        {

            var bookDetails =  db.Books.Include(b => b.Author)
                    .Where(b => b.BookID == id)
                    .Select(AsBookDetailsDto);

            return bookDetails;

        }

        public IEnumerable<BookDto> GetBooksByGenre(string genre)
        {
            return db.Books.Include(c => c.Author)
                            .Where(c => c.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
                            .Select(AsBooksDto);
        }

        public IEnumerable<BookDto> GetBookByAuthor(int authorID)
        {
            return db.Books.Include(b => b.Author)
                .Where(b => b.AuthorID == authorID)
                .Select(AsBooksDto);
        }

        public bool UpdateBook(int id, Book book)
        {
            if (id == book.BookID)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
    }
}
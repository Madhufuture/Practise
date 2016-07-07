using BooksAPI.DTO;
using BooksAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAPI.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<BookDto> GetBooks();
        BookDto GetBook(int id);
        IEnumerable<BookDetailDto> GetBookDetails(int id);
        IEnumerable<BookDto> GetBookByAuthor(int authorID);
        IEnumerable<BookDto> GetBooksByGenre(string genre);
        bool UpdateBook(int id, Book book);
    }
}

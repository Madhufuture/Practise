using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using BooksAPI.Models;
using BooksAPI.DTO;
using System;
using System.Threading.Tasks;
using BooksAPI.Repositories;
using System.Collections.Generic;

namespace BooksAPI.Controllers
{
    [RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        private IBookRepository rep;

        public BooksController(IBookRepository repository)
        {
            rep = repository;
        }

        // GET: api/Books
        [Route("")]
        public IEnumerable<BookDto> GetBooks()
        {
            return rep.GetBooks();
        }

        // GET: api/Books/5
        [Route("{id:int}")]
        [ResponseType(typeof(BookDto))]
        public IHttpActionResult GetBook(int id)
        {
            var bookDTO = rep.GetBook(id);

            if (bookDTO == null)
                return NotFound();
            return Ok(bookDTO);
        }

        [Route("{id:int}/details")]
        [ResponseType(typeof(BookDetailDto))]
        public IHttpActionResult GetBookDetails(int id)
        {
            var bookDetails = rep.GetBookDetails(id);

            if (bookDetails == null)
                return NotFound();
            return Ok(bookDetails);
        }

        [Route("{genre}")]
        [ResponseType(typeof(BookDto))]
        public IEnumerable<BookDto> GetBookByGenre(string genre)
        {
            return rep.GetBooksByGenre(genre);
        }


        // ~ sign overwrites the route mentioned in Route prefix
        [Route("~api/authors/{authorId}/books")]
        [ResponseType(typeof(BookDto))]
        public IEnumerable<BookDto> GetBookByAuthor(int authorID)
        {
            return rep.GetBookByAuthor(authorID);
        }


        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            var updated = rep.UpdateBook(id, book);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Books
        //[ResponseType(typeof(Book))]
        //public IHttpActionResult PostBook(Book book)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Books.Add(book);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = book.BookID }, book);
        //}

        //// DELETE: api/Books/5
        //[ResponseType(typeof(Book))]
        //public IHttpActionResult DeleteBook(int id)
        //{
        //    Book book = db.Books.Find(id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Books.Remove(book);
        //    db.SaveChanges();

        //    return Ok(book);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool BookExists(int id)
        //{
        //    return db.Books.Count(e => e.BookID == id) > 0;
        //}
    }
}
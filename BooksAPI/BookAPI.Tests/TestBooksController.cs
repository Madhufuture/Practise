using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BooksAPI.Repositories;
using BooksAPI.Controllers;
using System.Web.Http;
using System.Web.Http.Results;
using BooksAPI.DTO;

namespace BookAPI.Tests
{
    [TestClass]
    public class TestBooksController
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var mockRepository = new Mock<IBookRepository>();
            
            mockRepository.Setup(x => x.GetBook(1))
                .Returns(new BookDto { Title = "Midnight Rain" });
                        
            var controller = new BooksController(mockRepository.Object);

            //Act
            IHttpActionResult actionResult = controller.GetBook(1);
            var contentResult = actionResult as OkNegotiatedContentResult<BookDto>;


            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("Midnight Rain", contentResult.Content.Title);

        }
    }
}

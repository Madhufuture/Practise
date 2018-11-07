namespace ProductCatalog.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using API.Controllers;
    using API.Logger;
    using AutoMapper;
    using DataAccess;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using ProductCatalogGateway;
    using ProductCatalogGateway.Models;
    using Xunit;

    public class ProductCatalogControllerTest
    {
        public ProductCatalogControllerTest()
        {
            _repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            _mapperMock = new Mock<IMapper>();
            _mockLogger = new Mock<ILoggerAdapter<ProductCatalogController>>();
            _controller =
                new ProductCatalogController(_repositoryWrapperMock.Object, _mapperMock.Object, _mockLogger.Object);
        }

        private readonly Mock<IRepositoryWrapper> _repositoryWrapperMock;
        private readonly Mock<ILoggerAdapter<ProductCatalogController>> _mockLogger;
        private readonly ProductCatalogController _controller;
        private readonly Mock<IMapper> _mapperMock;

        private static Product MockProductAndProductModelWithData(out ProductsModel responseModelMock)
        {
            var responseMock = Mock.Of<Product>(x => x.ProductId == 1
                                                     && x.Image == Encoding.ASCII.GetBytes("VGVzdCBWQWx1ZQ==")
                                                     && x.LastUpdated ==
                                                     Convert.ToDateTime("2018-11-03 18:15:54.1840000")
                                                     && x.ProductPrice == 11 && x.ProductName == "Test"
            );

            responseModelMock = Mock.Of<ProductsModel>(x => x.ProductId == 1
                                                            && x.Image ==
                                                            Encoding.ASCII.GetBytes("VGVzdCBWQWx1ZQ==")
                                                            && x.LastUpdated ==
                                                            Convert.ToDateTime("2018-11-03 18:15:54.1840000")
                                                            && x.ProductPrice == 11 && x.ProductName == "Test");
            return responseMock;
        }

        private static Product MockProductAndProductModelWithNoData(out ProductsModel productsModel)
        {
            var responseMock = Mock.Of<Product>();
            productsModel = Mock.Of<ProductsModel>();
            return responseMock;
        }

        [Fact]
        public async void ProductCatalog_Controller_GetAllProducts_ThrowException_Test()
        {
            var productMock = MockProductAndProductModelWithData(out _);

            //Mock the repository wrapper
            var lstresponseMock = new List<Product> {productMock};
            _repositoryWrapperMock.Setup(repo => repo.Product.FindAll()).ReturnsAsync(lstresponseMock);

            //Mock the Mapper object
            _mapperMock.Setup(m => m.Map<List<ProductsModel>>(It.IsAny<List<Product>>()))
                .Throws(new Exception());

            await _controller.GetAllProducts().ConfigureAwait(false);
            _mockLogger.Verify(x => x.LogError(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()), Times.Once);
        }

        [Fact]
        public async void ProductCatalog_Controller_GetAllProducts_ValidResponse_Test()
        {
            var productMock = MockProductAndProductModelWithData(out var productsModelMock);

            //Mock the repository wrapper
            var lstresponseMock = new List<Product> {productMock};

            _repositoryWrapperMock.Setup(repo => repo.Product.FindAll()).ReturnsAsync(lstresponseMock);

            //Mock the Mapper object
            _mapperMock.Setup(m => m.Map<List<ProductsModel>>(It.IsAny<List<Product>>()))
                .Returns(new List<ProductsModel> {productsModelMock});

            var result = await _controller.GetAllProducts().ConfigureAwait(false);

            if (result is OkObjectResult response)
            {
                var items = Assert.IsType<List<ProductsModel>>(response.Value);
                Assert.True(items.Count == 1);
            }
        }

        [Fact]
        public async void ProductCatalog_Controller_GetAllProducts_WhenNoProducts_Test()
        {
            var productMock = MockProductAndProductModelWithNoData(out var productsModelMock);

            //Repository wrapper
            var lstproductMock = new List<Product> {productMock};
            _repositoryWrapperMock.Setup(repo => repo.Product.FindAll()).ReturnsAsync(lstproductMock);

            //Mapper
            _mapperMock.Setup(m => m.Map<List<ProductsModel>>(It.IsAny<List<Product>>()))
                .Returns(new List<ProductsModel> {productsModelMock});

            var result = await _controller.GetAllProducts().ConfigureAwait(false);

            if (result is OkObjectResult response)
            {
                var items = Assert.IsType<List<ProductsModel>>(response.Value);
                Assert.DoesNotContain(items, x => x.ProductId != 0);
            }
        }

        [Fact]
        public async void ProductCatalog_Controller_GetProductById_404NotFound_Test()
        {
            var testProductId = 2;

            //Mock the repository wrapper
            _repositoryWrapperMock
                .Setup(repo => repo.Product.FindByCondition(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync((Product) null);

            //Mock the Mapper object
            _mapperMock.Setup(m => m.Map<ProductsModel>(It.IsAny<Product>()))
                .Returns((ProductsModel) null);

            var result = await _controller.GetProduct(testProductId).ConfigureAwait(false);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void ProductCatalog_Controller_GetProductById_MatchResponse_Test()
        {
            var testProductId = 1;
            var productMock = MockProductAndProductModelWithData(out var productsModelMock);

            //Mock the repository wrapper
            _repositoryWrapperMock
                .Setup(repo => repo.Product.FindByCondition(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(productMock);

            //Mock the Mapper object
            _mapperMock.Setup(m => m.Map<ProductsModel>(It.IsAny<Product>()))
                .Returns(productsModelMock);

            var result = await _controller.GetProduct(testProductId).ConfigureAwait(false);

            var response = Assert.IsType<OkObjectResult>(result);
            if (response.Value is ProductsModel productModelResponse)
            {
                Assert.Equal(productsModelMock.ProductName, productModelResponse.ProductName);
                Assert.Same(productsModelMock, productModelResponse);
            }
        }

        [Fact]
        public async void ProductCatalog_Controller_GetProductById_SuccessfulResponse_Test()
        {
            var testProductId = 1;
            var productMock = MockProductAndProductModelWithData(out var productsModelMock);

            //Mock the repository wrapper
            _repositoryWrapperMock
                .Setup(repo => repo.Product.FindByCondition(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(productMock);

            //Mock the Mapper object
            _mapperMock.Setup(m => m.Map<ProductsModel>(It.IsAny<Product>()))
                .Returns(productsModelMock);

            var result = await _controller.GetProduct(testProductId).ConfigureAwait(false);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void ProductCatalog_Controller_InsertProduct_400BadRequest_Test()
        {
            var productModel = Mock.Of<ProductsModel>(x =>
                x.ProductId == 2
                && x.ProductName ==
                "Product name more than 50 characters Product name more than 50 characters Product name more than 50 characters"
                && x.Image == new byte[20]
                && x.LastUpdated == Convert.ToDateTime("10/20/30")
                && x.ProductPrice == -10);

            _repositoryWrapperMock.Setup(repo => repo.Product.Create(It.IsAny<Product>()))
                .Returns(Task.FromResult(productModel));

            //Mock the Mapper object
            _mapperMock.Setup(m => m.Map<Product>(It.IsAny<ProductsModel>()))
                .Returns(new Product());

            _controller.ModelState.AddModelError("ProductName", "Name can't be longer than 50 characters");
            var result = await _controller.InsertProduct(productModel).ConfigureAwait(false);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void ProductCatalog_Controller_InsertProduct_MatchResponse_Test()
        {
            var productModel = Mock.Of<ProductsModel>(x =>
                x.ProductId == 2
                && x.ProductName == "Second Product"
                && x.Image == new byte[20]
                && x.LastUpdated == DateTime.Now
                && x.ProductPrice == 200);

            var product = Mock.Of<Product>(x =>
                x.ProductId == 2
                && x.ProductName == "Second Product"
                && x.Image == new byte[20]
                && x.LastUpdated == DateTime.Now
                && x.ProductPrice == 200);

            //Mock the repository wrapper
            _repositoryWrapperMock.Setup(repo => repo.Product.Create(It.IsAny<Product>()))
                .Returns(Task.FromResult(productModel));

            //Mock the Mapper object
            _mapperMock.Setup(m => m.Map<Product>(It.IsAny<ProductsModel>()))
                .Returns(product);

            var result = await _controller.InsertProduct(productModel).ConfigureAwait(false);

            if (result is CreatedAtActionResult response)
                if (response.Value is ProductsModel responseProduct)
                {
                    Assert.Equal("Second Product", responseProduct.ProductName);
                    Assert.Same(productModel, responseProduct);
                }
        }

        [Fact]
        public async void ProductCatalog_Controller_InsertProduct_Success_Test()
        {
            var productModel = Mock.Of<ProductsModel>(x =>
                x.ProductId == 2
                && x.ProductName == "Second Product"
                && x.Image == new byte[20]
                && x.LastUpdated == DateTime.Now
                && x.ProductPrice == 200);

            //Mock the repository wrapper
            _repositoryWrapperMock.Setup(repo => repo.Product.Create(It.IsAny<Product>()))
                .Returns(Task.FromResult(productModel));

            //Mock the Mapper object
            _mapperMock.Setup(m => m.Map<Product>(It.IsAny<ProductsModel>()))
                .Returns(new Product());

            var result = await _controller.InsertProduct(productModel).ConfigureAwait(false);

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async void ProductCatalog_Controller_InsertProduct_ThrowsException_Test()
        {
            var productModel = Mock.Of<ProductsModel>(x =>
                x.ProductId == 2
                && x.ProductName == "Product name "
                && x.Image == new byte[20]
                && x.LastUpdated == DateTime.Now
                && x.ProductPrice == -10);

            //Mock the repository wrapper
            _repositoryWrapperMock.Setup(repo => repo.Product.Create(It.IsAny<Product>())).ThrowsAsync(new Exception());

            //Mock the Mapper object
            _mapperMock.Setup(m => m.Map<List<ProductsModel>>(It.IsAny<ProductsModel>()))
                .Throws(new Exception());

            await _controller.InsertProduct(productModel).ConfigureAwait(false);
            _mockLogger.Verify(x => x.LogError(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()), Times.Once);
        }

        [Fact]
        public async void
            ProductCatalog_Controller_UpdateProduct_DifferentProductId_ModelState_NotValid_BadRequest_Test()
        {
            var productModel = Mock.Of<ProductsModel>(x =>
                x.ProductId == 3
                && x.ProductName == "Test"
                && x.Image == new byte[20]
                && x.LastUpdated == DateTime.Now
                && x.ProductPrice == 10);

            _repositoryWrapperMock.Setup(repo => repo.Product.Create(It.IsAny<Product>()))
                .Returns(Task.FromResult(productModel));

            //Mock the Mapper object
            _mapperMock.Setup(m => m.Map<Product>(It.IsAny<ProductsModel>()))
                .Returns(new Product());

            _controller.ModelState.AddModelError("ProductId", "Updating product details for wrong product");
            var result = await _controller.UpdateProduct(2, productModel).ConfigureAwait(false);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async void ProductCatalog_Controller_UpdateProduct_ModelState_NotValid_BadRequest_Test()
        {
            var productModel = Mock.Of<ProductsModel>(x =>
                x.ProductId == 2
                && x.ProductName ==
                "Product name more than 50 characters Product name more than 50 characters Product name more than 50 characters"
                && x.Image == new byte[20]
                && x.LastUpdated == Convert.ToDateTime("10/20/30")
                && x.ProductPrice == -10);

            _repositoryWrapperMock.Setup(repo => repo.Product.Create(It.IsAny<Product>()))
                .Returns(Task.FromResult(productModel));

            //Mock the Mapper object
            _mapperMock.Setup(m => m.Map<Product>(It.IsAny<ProductsModel>()))
                .Returns(new Product());

            _controller.ModelState.AddModelError("ProductName", "Name can't be longer than 50 characters");
            var result = await _controller.UpdateProduct(2, productModel).ConfigureAwait(false);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void
            ProductCatalog_Controller_UpdateProduct_NonExisting_ProductId_And_ProductObject_NotFound_Error_Test()
        {
            var productModel = Mock.Of<ProductsModel>(x =>
                x.ProductId == 3
                && x.ProductName == "Third Product"
                && x.Image == new byte[20]
                && x.LastUpdated == DateTime.Now
                && x.ProductPrice == 300);

            //Mock the repository wrapper
            _repositoryWrapperMock
                .Setup(repo => repo.Product.FindByCondition(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync((Product) null);
            _repositoryWrapperMock.Setup(repo => repo.Product.Create(It.IsAny<Product>()))
                .Returns(Task.FromResult(productModel));

            //Mock the Mapper object
            _mapperMock.Setup(m => m.Map<Product>(It.IsAny<ProductsModel>()))
                .Returns((Product) null);

            var result = await _controller.UpdateProduct(3, productModel).ConfigureAwait(false);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void ProductCatalog_Controller_UpdateProduct_Success_Test()
        {
            var productModel = Mock.Of<ProductsModel>(x =>
                x.ProductId == 2
                && x.ProductName == "Third Product"
                && x.Image == new byte[20]
                && x.LastUpdated == DateTime.Now
                && x.ProductPrice == 300);

            //Mock the repository wrapper
            _repositoryWrapperMock.Setup(repo => repo.Product.Create(It.IsAny<Product>()))
                .Returns(Task.FromResult(productModel));

            _repositoryWrapperMock
                .Setup(repo => repo.Product.FindByCondition(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(new Product());

            //Mock the Mapper object
            _mapperMock.Setup(m => m.Map<Product>(It.IsAny<ProductsModel>()))
                .Returns(new Product());

            var result = await _controller.UpdateProduct(2, productModel).ConfigureAwait(false);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void ProductCatlog_Controller_DeleteProduct_Success_Test()
        {
            _repositoryWrapperMock
                .Setup(repo => repo.Product.FindByCondition(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(new Product());

            _repositoryWrapperMock.Setup(repo => repo.Product.Delete(It.IsAny<Product>())).Verifiable();

            var result = await _controller.DeleteProduct(1).ConfigureAwait(false);

            Assert.IsType<OkObjectResult>(result);
            _repositoryWrapperMock.Verify(x => x.Product.Delete(It.IsAny<Product>()), Times.Once);
        }
    }
}
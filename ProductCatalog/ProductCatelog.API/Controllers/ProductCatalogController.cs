namespace ProductCatalog.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using DataAccess;
    using Logger;
    using Microsoft.AspNetCore.Mvc;
    using ProductCatalogGateway;
    using ProductCatalogGateway.Models;

    [Route("api/productcatalog")]
    [ApiController]
    public class ProductCatalogController : ControllerBase
    {
        private readonly ILoggerAdapter<ProductCatalogController> _loggerFactory;
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ProductCatalogController(IRepositoryWrapper repositoryWrapper, IMapper mapper,
            ILoggerAdapter<ProductCatalogController> loggerFactory)
        {
            _repositoryWrapper = repositoryWrapper;
            _loggerFactory = loggerFactory;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                _loggerFactory.LogInformation("Fetching all products");
                var products = await _repositoryWrapper.Product.FindAll().ConfigureAwait(false);
                var lstProducts = _mapper.Map<List<ProductsModel>>(products);
                _loggerFactory.LogInformation($"Returning {lstProducts.Count} products");

                return Ok(lstProducts);
            }
            catch (Exception ex)
            {
                _loggerFactory.LogError($"Something went wrong: {ex.Message}", GetSecondaryInfo(ex));
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            try
            {
                _loggerFactory.LogInformation("Find product by Id");

                var product = await _repositoryWrapper.Product.FindByCondition(x => x.ProductId == id)
                    .ConfigureAwait(false);
                var prod = _mapper.Map<ProductsModel>(product);

                if (prod != null) return Ok(prod);

                return NotFound();
            }
            catch (Exception ex)
            {
                _loggerFactory.LogError($"Something went wrong: {ex.Message}", GetSecondaryInfo(ex));
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] ProductsModel product)
        {
            try
            {
                _loggerFactory.LogInformation($"Update product {id}");
                if (!ModelState.IsValid) return BadRequest(ModelState);

                if (id != product.ProductId) return BadRequest();

                var objProduct = await _repositoryWrapper.Product.FindByCondition(x => x.ProductId == id).ConfigureAwait(false);
                if (objProduct == null)
                    return NotFound();

                var prod = _mapper.Map<Product>(product);

                await _repositoryWrapper.Product.Update(prod).ConfigureAwait(false);
                _loggerFactory.LogInformation($"{product.ProductName} has been updated successfully");

                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerFactory.LogError($"Something went wrong: {ex.Message}", GetSecondaryInfo(ex));
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertProduct([FromBody] ProductsModel product)
        {
            try
            {
                _loggerFactory.LogInformation($"Inserting product {product.ProductName}");
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var prod = _mapper.Map<Product>(product);

                await _repositoryWrapper.Product.Create(prod).ConfigureAwait(false);
                return CreatedAtAction("GetProduct", new {id = product.ProductId}, product);
            }
            catch (Exception ex)
            {
                _loggerFactory.LogError($"Something went wrong: {ex.Message}", GetSecondaryInfo(ex));
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            try
            {
                _loggerFactory.LogInformation($"Deleting the product {id}");
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var product = await _repositoryWrapper.Product.FindByCondition(x => x.ProductId == id)
                    .ConfigureAwait(false);

                if (product == null)
                {
                    _loggerFactory.LogInformation($"No products found for product {id}");
                    return NotFound();
                }

                _repositoryWrapper.Product.Delete(product);
                await _repositoryWrapper.Product.Save();

                return Ok(product);
            }
            catch (Exception ex)
            {
                _loggerFactory.LogError($"Something went wrong: {ex.Message}", GetSecondaryInfo(ex));
                return StatusCode(500, "Internal server error");
            }
        }

        private static Dictionary<string, object> GetSecondaryInfo(Exception ex)
        {
            var secondaryInfo = new Dictionary<string, object>
            {
                {"Exception Message", ex.Message},
                {"Source", ex.Source},
                {"Stack Trace", ex.StackTrace},
                {"Method Info", ex.TargetSite.Name}
            };

            return secondaryInfo;
        }
    }
}
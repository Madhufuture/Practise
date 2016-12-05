using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Repositories.GenericRepositories;

namespace WebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        private IGenericRepository<object> _genericRepo;

        public ProductsController()
        {

        }
        public ProductsController(IGenericRepository<object> repository)
        {
            _genericRepo = repository;
        }

        public IHttpActionResult getProducts()
        {
            return Ok(_genericRepo.GetAllProducts());
        }

    }
}

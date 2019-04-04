namespace ProductCatalog.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<ProductViewModel> products = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/api/");

                var response = client.GetAsync("productcatalog");

                if (response.Result.IsSuccessStatusCode)
                    products = response.Result.Content.ReadAsAsync<IList<ProductViewModel>>().Result;
            }

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model, List<IFormFile> image)
        {
            HttpResponseMessage response;

            foreach (var item in image)
            {
                if (item.Length <= 0) continue;

                using (var stream = new MemoryStream())
                {
                    await item.CopyToAsync(stream);
                    model.Image = stream.ToArray();
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/api/");
                response = client.PostAsJsonAsync("productcatalog", model).Result;
            }

            return response.IsSuccessStatusCode ? RedirectToAction("Index") : RedirectToAction("Error");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var productModel = GetProductById(id);

            return View(productModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model, List<IFormFile> image)
        {
            HttpResponseMessage response;
            foreach (var img in image)
            {
                if (img.Length <= 0) continue;

                using (var stream = new MemoryStream())
                {
                    await img.CopyToAsync(stream).ConfigureAwait(false);
                    model.Image = stream.ToArray();
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/api/");
                response = client.PutAsJsonAsync($"productcatalog/{model.ProductId}", model).Result;
            }

            return response.IsSuccessStatusCode ? RedirectToAction("Index") : RedirectToAction("Error");
        }

        public ActionResult Delete(int id)
        {
            var product = GetProductById(id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/api/");
                response = await client.DeleteAsync($"productcatalog/{id}").ConfigureAwait(false);
            }

            return response.IsSuccessStatusCode ? RedirectToAction("Index") : RedirectToAction("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        private ProductViewModel GetProductById(int id)
        {
            ProductViewModel productModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/api/");
                var product = client.GetAsync($"productcatalog/{id}").Result;

                if (product.IsSuccessStatusCode) productModel = product.Content.ReadAsAsync<ProductViewModel>().Result;
            }

            return productModel;
        }
    }
}
using Catalogs.api.Entities;
using Catalogs.api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogs.api.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository repository;

        public CatalogController(IProductRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CretaeProduct([FromBody] Product product)
        {
            await repository.CreateProduct(product);
            return Ok();
        }

        [HttpGet("{id:length(24)}", Name = "GetProductById")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var currentProduct = await repository.GetProduct(id);
            if (currentProduct == null)
                return NotFound();

            return Ok(currentProduct);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return Ok(await repository.GetProducts());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        => Ok(await repository.UpdateProduct(product));


        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteProduct(string id)
            => Ok(await repository.DeleteProduct(id));

    }
}

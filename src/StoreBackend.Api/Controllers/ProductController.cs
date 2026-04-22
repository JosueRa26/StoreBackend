using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreBackend.Facade;
using StoreBackend.Api.Models.Requests;
using StoreBackend.Api.Mappers;
using StoreBackend.Exceptions;
using StoreBackend.Domain.Entities;
using StoreBackend.DomainService;



namespace StoreBackend.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductFacade productFacade;

        public ProductController(IProductFacade productFacade)
        {
            this.productFacade = productFacade;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var products = await productFacade.GetAllAsync();
            var models = ProductMapper.ToModel(products);
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            try
            {
                var product = await productFacade.GetByIdAsync(id);
                var model = ProductMapper.ToModel(product);
                return Ok(model);
            }
            catch (ResourceNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductRequestModel product)
        {
            var dto = ProductMapper.ToDto(product);
            var addedProduct = await productFacade.AddAsync(dto);
            var model = ProductMapper.ToModel(addedProduct);
            return CreatedAtAction(nameof(GetProduct), new { id = model.ProductResourceId }, model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await productFacade.DeleteAsync(id);
                return Ok();
            }
            catch (ResourceNotFoundException)
            {
                return NotFound();
            }
        }


    }
}

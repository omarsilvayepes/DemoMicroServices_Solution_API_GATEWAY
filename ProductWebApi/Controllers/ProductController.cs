using Microsoft.AspNetCore.Mvc;
using ProductWebApi.Models;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _dbContext;

        public ProductController(ProductDbContext productDbContext)
        {
            _dbContext = productDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return _dbContext.Products;
        }

        [HttpGet ("{product_Id:int}")]
        public async Task<ActionResult<Product>> GetProductIdAsync(int product_Id)
        {
           return await _dbContext.Products.FindAsync(product_Id);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return Ok("Create Successfully");
        }
        [HttpPut]
        public async Task<ActionResult> UpdateProductAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return Ok("Update Succesfully");
        }

        [HttpDelete("{productId:int}")]
        public async Task<ActionResult> DeleteProductAsync(int product_Id)
        {
            var product = await _dbContext.Products.FindAsync(product_Id);
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return Ok("Delete Succesfully");
        }
    }
}

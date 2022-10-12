using CustomerWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext _customerDbContext;
        public CustomerController(CustomerDbContext customerDbContext)
        {
            this._customerDbContext = customerDbContext;// use dependency Injection
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            return _customerDbContext.Customers;
        }

        [HttpGet("{customerId:int}")]
        public async Task<ActionResult<Customer>> GetCustomerByIdAsync(int customerId)
        {
             return await _customerDbContext.Customers.FindAsync(customerId);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomerAsync(Customer customer)
        {
            await _customerDbContext.Customers.AddAsync(customer);
            await _customerDbContext.SaveChangesAsync();
            return Ok("Saved Sucessfully");
        }
        [HttpPut]
        public async Task<ActionResult> UpdateCustomerAsync(Customer customer)
        {
            _customerDbContext.Update(customer);
            await _customerDbContext.SaveChangesAsync();
            return Ok("Update Sucessfully");
        }

        [HttpDelete("{customerId:int}")]
        public async Task<ActionResult> DeleteCustomerAsync(int customerId)
        {
            var customer = await _customerDbContext.Customers.FindAsync(customerId);
            _customerDbContext.Remove(customer);
            await _customerDbContext.SaveChangesAsync();
            return Ok("Delete Sucessfully");
        }
    }
}

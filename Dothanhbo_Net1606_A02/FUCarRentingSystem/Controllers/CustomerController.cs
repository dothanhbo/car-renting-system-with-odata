using AutoMapper;
using BusinessObjects.Models;
using FUCarRentingSystem.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repositories.Repositories;

namespace FUCarRentingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await customerRepository.GetCustomers();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var result = await customerRepository.GetCustomer(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerDto customerDto)
        {
            if (await customerRepository.CheckEmailCanBeUsed(customerDto.Email) == false)
                return BadRequest();
            var customer = mapper.Map<Customer>(customerDto);
            await customerRepository.AddCustomer(customer);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerDto customerDto)
        {
            var customer = mapper.Map<Customer>(customerDto);
            customer.Id = id;
            await customerRepository.UpdateCustomer(customer);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await customerRepository.DeleteCustomer(id);
            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            IConfiguration config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();
            string adminEmail = config["AdminAccount:Email"];
            string adminPassword = config["AdminAccount:Password"];
            if (loginDto.email.ToLower().Equals(adminEmail.ToLower()) && loginDto.password.Equals(adminPassword))
                return Ok("Admin");
            var customer = await customerRepository.Login(loginDto.email, loginDto.password);
            if (customer == null)
            {
                return Unauthorized();
            }
            //Provide token
            return Ok(customer);
        }
    }
}

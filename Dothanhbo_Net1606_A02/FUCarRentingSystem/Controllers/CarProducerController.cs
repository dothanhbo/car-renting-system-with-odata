using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Repositories;

namespace FUCarRentingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarProducerController : ControllerBase
    {
        private readonly ICarProducerRepository carProducerRepository;

        public CarProducerController(ICarProducerRepository carProducerRepository)
        {
            this.carProducerRepository = carProducerRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCarProducers()
        {
            var result = await carProducerRepository.GetCarProducers();
            return Ok(result);
        }
    }
}

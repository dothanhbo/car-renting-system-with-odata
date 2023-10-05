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
    public class CarController : ControllerBase
    {

        private readonly ICarRepository carRepository;
        private readonly IMapper mapper;

        public CarController(ICarRepository carRepository, IMapper mapper)
        {
            this.carRepository = carRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetCars()
        {
            var result = await carRepository.GetCars();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar(int id)
        {
            var result = await carRepository.GetCar(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddCar(CarDto carDto)
        {
            var car = mapper.Map<Car>(carDto);
            await carRepository.AddCar(car);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, CarDto carDto)
        {
            var car = mapper.Map<Car>(carDto);
            car.Id = id;
            await carRepository.UpdateCar(car);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await carRepository.DeleteCar(id);
            return Ok();
        }
    }
}

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
    public class CarRentalController : ControllerBase
    {
        private readonly ICarRentalRepository carRentalRepository;
        private readonly IMapper mapper;

        public CarRentalController(ICarRentalRepository carRentalRepository, IMapper mapper)
        {
            this.carRentalRepository = carRentalRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetCarRentals()
        {
            var result = await carRentalRepository.GetCarRentals();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddCarRental(CarRentalDto carRentalDto)
        {
            var carRental = mapper.Map<CarRental>(carRentalDto);
            if (await carRentalRepository.CheckCarAvalable(carRental.CarID, carRental.PickupDate, carRental.ReturnDate) == false) 
            {
                return BadRequest();
            }
            await carRentalRepository.AddCarRental(carRental);
            return Ok();
        }
    }
}

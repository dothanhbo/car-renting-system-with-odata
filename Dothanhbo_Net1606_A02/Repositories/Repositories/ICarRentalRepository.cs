using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public interface ICarRentalRepository
    {
        Task<List<CarRental>> GetCarRentals();
        Task AddCarRental(CarRental carRental);
        Task<bool> CheckCarAvalable(int carId, DateTime pickUpDate, DateTime returnDate);
    }
}

using BusinessObjects.Models;
using DataAcessObjects.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Imple
{
    public class CarRentalRepository : ICarRentalRepository
    {

        private readonly CarRentalDAO carRentalDAO;
        private readonly CarDAO carDAO;
        private readonly CustomerDAO customerDAO;
        public CarRentalRepository(CarRentalDAO carRentalDAO, CarDAO carDAO, CustomerDAO customerDAO)
        {
            this.carRentalDAO = carRentalDAO;
            this.carDAO = carDAO;
            this.customerDAO = customerDAO;
        }
        public async Task<List<CarRental>> GetCarRentals()
        {
            var result = await carRentalDAO.GetCarRentals();
            if(result.Count > 0)
            {
                foreach (CarRental carRental in result)
                {
                    var Car = await carDAO.GetAsync(expression: x => x.Id == carRental.CarID, pageSize: 1);
                    carRental.Car = Car.Result[0];
                    var Customer = await customerDAO.GetAsync(expression: x => x.Id == carRental.CustomerID, pageSize: 1);
                    carRental.Customer = Customer.Result[0];
                }
            }
            return result;
        }
        public async Task AddCarRental(CarRental carRental)
            => await carRentalDAO.AddCarRental(carRental);
        public async Task<bool> CheckCarAvalable(int carId, DateTime pickUpDate, DateTime returnDate)
            => await carRentalDAO.CheckCarAvalable(carId, pickUpDate, returnDate);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DataAcessObjects.DAO;

namespace Repositories.Repositories.Imple
{
    public class CarRepository : ICarRepository
    {
        private readonly CarDAO carDAO;
        private readonly CarProducerDAO carProducerDAO;
        private readonly CarRentalDAO carRentalDAO;

        public CarRepository(CarDAO carDAO, CarProducerDAO carProducerDAO, CarRentalDAO carRentalDAO)
        {
            this.carDAO = carDAO;
            this.carProducerDAO = carProducerDAO;
            this.carRentalDAO = carRentalDAO;
        }

        public async Task<List<Car>> GetCars() { 
            var carList = await carDAO.GetAsync(isTakeAll: true);
            foreach (Car car in carList.Result) {
                var carProducer = await carProducerDAO.GetAsync(expression: x => x.Id == car.ProducerID, pageSize: 1);
                car.CarProducer = carProducer.Result[0];
            }
            return carList.Result;
        }
        public async Task<Car> GetCar(int id)
        {
            var car = await carDAO.GetAsync(expression: x=> x.Id == id, pageSize: 1);
            var carProducer = await carProducerDAO.GetAsync(expression: x => x.Id == car.Result[0].ProducerID, pageSize: 1);
            car.Result[0].CarProducer = carProducer.Result[0];
            return car.Result[0];
        }
        public async Task UpdateCar(Car car)
        {
            var result = await carDAO.GetAsync(expression: x => x.Id == car.Id, pageSize: 1);
            if (result.Result.Count == 0)
                throw new Exception("Can not found!");
            await carDAO.UpdateAsync(car);
        }
        public async Task AddCar(Car car)
        => await carDAO.AddAsync(car);

        public async Task DeleteCar(int id)
        {
            var result = await carDAO.GetAsync(expression: x => x.Id == id, pageSize: 1);
            if (result.Result.Count == 0)
                throw new Exception("Can not found!");
            if (await carRentalDAO.CheckCarRentalExist(id))
            {
                result.Result[0].Status = 0;
                await carDAO.UpdateAsync(result.Result[0]);
            }
            else
            {   
                await carDAO.DeleteAsync(id);
            }
        }
    }
}

using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public interface ICarRepository
    {
        Task<List<Car>> GetCars();
        Task<Car> GetCar(int id);
        Task UpdateCar(Car car);
        Task AddCar(Car car);
        Task DeleteCar(int id);
    }
}

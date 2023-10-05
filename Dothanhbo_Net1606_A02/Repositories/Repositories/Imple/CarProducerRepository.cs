using BusinessObjects.Models;
using DataAcessObjects.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Imple
{
    public class CarProducerRepository : ICarProducerRepository
    {

        private readonly CarProducerDAO carProducerDAO;

        public CarProducerRepository(CarProducerDAO carProducerDAO)
        {
            this.carProducerDAO = carProducerDAO;
        }

        public async Task<List<CarProducer>> GetCarProducers()
        {
            var carProducerList = await carProducerDAO.GetAsync(isTakeAll: true);
            return carProducerList.Result;
        }
        public async Task<CarProducer> GetCarProducer(int id)
        {
            var carProducerList = await carProducerDAO.GetAsync(expression: x=> x.Id == id, pageSize:1);
            return carProducerList.Result[0];
        }
    }
}

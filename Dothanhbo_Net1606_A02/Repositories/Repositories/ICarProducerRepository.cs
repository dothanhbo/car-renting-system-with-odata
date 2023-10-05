using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public interface ICarProducerRepository
    {
        Task<List<CarProducer>> GetCarProducers();
        Task<CarProducer> GetCarProducer(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DataAcessObjects.DAO.Base;
using Microsoft.EntityFrameworkCore;

namespace DataAcessObjects.DAO
{
    public class CarDAO : BaseDAO<Car>
    {
        public CarDAO() : base()
        {
        }
    }
}

using DataAcessObjects.DAO.Base;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAcessObjects.DAO
{
    public class CustomerDAO : BaseDAO<Customer>
    {
        public CustomerDAO() : base()
        {
        }
        public async Task<bool> CheckEmailCanBeUsed(string email)
        {
            using (var context = new FUCarRentingSystemContext())
            {
                var result = await context.Customers.Where(x => x.Email.ToLower().Equals(email.ToLower())).ToListAsync();
                if (result.Count > 0)
                    return false;
                else
                    return true;
            }
        }
    }
}

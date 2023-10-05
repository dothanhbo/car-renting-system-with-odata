using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetCustomer(int id);
        Task UpdateCustomer(Customer customer);
        Task AddCustomer(Customer customer);
        Task DeleteCustomer(int id);
        Task<Customer> Login(string email, string password);
        Task<bool> CheckEmailCanBeUsed(string email);
    }
}

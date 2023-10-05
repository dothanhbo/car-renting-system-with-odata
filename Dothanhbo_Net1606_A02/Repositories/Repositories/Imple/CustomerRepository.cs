using BusinessObjects.Models;
using DataAcessObjects.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Imple
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDAO customerDAO;

        public CustomerRepository(CustomerDAO customerDAO)
        {
            this.customerDAO = customerDAO;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            var customerList = await customerDAO.GetAsync(isTakeAll: true);
            return customerList.Result;
        }
        public async Task<Customer> GetCustomer(int id)
        {
            var customer = await customerDAO.GetAsync(expression: x => x.Id == id, pageSize: 1);
            return customer.Result[0];
        }
        public async Task UpdateCustomer(Customer customer)
        {
            var result = await customerDAO.GetAsync(expression: x => x.Id == customer.Id, pageSize: 1);
            if (result.Result == null)
                throw new Exception("Can not found!");
            await customerDAO.UpdateAsync(customer);
        }
        public async Task AddCustomer(Customer customer)
        => await customerDAO.AddAsync(customer);

        public async Task DeleteCustomer(int id)
        {
            var result = await customerDAO.GetAsync(expression: x => x.Id == id, pageSize: 1);
            if (result.Result == null)
                throw new Exception("Can not found!");
            await customerDAO.DeleteAsync(id);
        }
        public async Task<Customer> Login(string email, string password)
        {
            var result = await customerDAO.GetAsync(expression: x => x.Email.ToLower().Equals(email.ToLower()) && x.Password.Equals(password), pageSize: 1);
            if (result.Result.Count == 0)
                return null;
            return result.Result[0];
        }
        public async Task<bool> CheckEmailCanBeUsed(string email)
            => await customerDAO.CheckEmailCanBeUsed(email);
    }
}

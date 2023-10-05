using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessObjects.DAO
{
    public class CarRentalDAO
    {
        public async Task<List<CarRental>> GetCarRentals()
        {
            using (var context = new FUCarRentingSystemContext())
            {
                var result = await context.CarRentals.ToListAsync();
                return result;
            }
        }
        public async Task AddCarRental(CarRental carRental)
        {
            using (var context = new FUCarRentingSystemContext())
            {
                var result = await context.CarRentals.AddAsync(carRental);
                await context.SaveChangesAsync();
            }
        }
        public async Task<bool> CheckCarRentalExist(int carId) 
        {
            using (var context = new FUCarRentingSystemContext())
            {
                var carRentalList = await context.CarRentals.Where(od => od.CarID == carId).ToListAsync();
                if (carRentalList.Count > 0)
                    return true;
                else
                    return false;
            }
        }
        public async Task<bool> CheckCarAvalable(int carId, DateTime pickUpDate, DateTime returnDate)
        {
            bool check = true;
            using (var context = new FUCarRentingSystemContext())
            {
                var carRentalList = await context.CarRentals.Where(od => od.CarID == carId && od.ReturnDate >= DateTime.Today).ToListAsync();
                foreach (var carRental in carRentalList)
                {
                    if ((pickUpDate.Date < carRental.PickupDate.Date && returnDate.Date < carRental.PickupDate.Date) ||
                        (pickUpDate.Date > carRental.ReturnDate.Date && returnDate.Date > carRental.ReturnDate.Date))
                    { }
                    else
                    {
                        check = false;
                    }
                    break;
                }
            }
            return check;
        }
    }
}

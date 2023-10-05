using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Newtonsoft.Json;
using CustomerPages.SessionHelper;
using System.Text;
using System.Net;

namespace DoThanhBo_Net1606_A02_Razor.Pages.CustomerPages
{
    public class CarRentingModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CarRentingModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public int TotalDay { get; set; }
        public decimal Total { get; set; }
        public CarRental CarRental { get; set; }
        public IActionResult OnGetRent(int? id)
        {
            int customerId = HttpContext.Session.GetInt32("Customer") ?? 0;
            if (customerId == 0)
                return RedirectToPage("/NotAuthorized");
            CarRental = new CarRental();
            CarRental.CarId = id.Value;
            CarRental.PickupDate = DateTime.Now.Date;
            CarRental.ReturnDate = DateTime.Now.Date;    
            SessionHelper.SetObjectAsJson(HttpContext.Session, "CarRental", CarRental);
            return RedirectToPage("./CarRenting");
            
        }
        public async Task<IActionResult> OnGetAsync()
        {
            CarRental = SessionHelper.GetObjectFromJson<CarRental>(HttpContext.Session, "CarRental");
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("http://localhost:5044/api/");
                var response = await client.GetAsync("Car/" + CarRental.CarId.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    CarRental.Car = JsonConvert.DeserializeObject<Car>(responseContent);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to retrieve car.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            }
            TotalDay = 1;
            CarRental.RentPrice = TotalDay * CarRental.Car.RentPrice;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "CarRental", CarRental);
            return Page();
        }
        public IActionResult OnPostUpdate(DateTime pickUpDate, DateTime returnDate, decimal total)
        {
            CarRental = SessionHelper.GetObjectFromJson<CarRental>(HttpContext.Session, "CarRental");
            CarRental.PickupDate = pickUpDate;
            CarRental.ReturnDate = returnDate;
            if (CarRental.PickupDate > CarRental.ReturnDate)
                TotalDay = 0;
            else
                TotalDay = ((int)(CarRental.ReturnDate - CarRental.PickupDate).TotalDays + 1);
            CarRental.RentPrice = TotalDay * CarRental.Car.RentPrice;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "CarRental", CarRental);
            return Page();
        }
        public async Task<IActionResult> OnPostRent()
        {
            int customerId = HttpContext.Session.GetInt32("Customer") ?? 0;
            if (customerId == 0)
                return RedirectToPage("/NotAuthorized");
            CarRental = SessionHelper.GetObjectFromJson<CarRental>(HttpContext.Session, "CarRental");
            CarRental.CustomerId = customerId;
            CarRental.Status = 1;
            CarRental.Car = null;
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("http://localhost:5044/api/");
                var carRentalInfoJson = JsonConvert.SerializeObject(CarRental);
                var content = new StringContent(carRentalInfoJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("CarRental", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Renting Success.";
                    return RedirectToPage("./Index");
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        TempData["Message"] = "This car has been rent for that days.";
                        return RedirectToPage("./Index");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            }
            return RedirectToPage("./Index");
        }
    }
}

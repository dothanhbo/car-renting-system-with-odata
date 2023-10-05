using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Newtonsoft.Json;

namespace DoThanhBo_Net1606_A02_Razor.Pages.CustomerPages
{
    public class CarRentalListModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CarRentalListModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IList<CarRental> CarRental { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            int customerId = HttpContext.Session.GetInt32("Customer") ?? 0;
            if (customerId == 0)
                return RedirectToPage("/NotAuthorized");
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("http://localhost:5044/api/");

                var response = await client.GetAsync("CarRental?$filter=CustomerID%20eq%20"+ customerId.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    CarRental = JsonConvert.DeserializeObject<List<CarRental>>(responseContent);
                }
                else
                {
                    // Handle the error accordingly.
                    ModelState.AddModelError(string.Empty, "Failed to retrieve car information.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the API call.
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            }
            return Page();
        }
    }
}

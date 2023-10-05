using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Newtonsoft.Json;

namespace DoThanhBo_Net1606_A02_Razor.Pages.Admin.CarManagement
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Car Car { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("http://localhost:5044/api/"); // Adjust the base URL as needed.

                // Send an HTTP GET request to retrieve the list of CarInformation from the API.
                var response = await client.GetAsync("Car/" + id.ToString()); // Use the appropriate endpoint URL.

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a JSON string.
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into a list of CarInformation.
                    Car = JsonConvert.DeserializeObject<Car>(responseContent);
                }
                else
                {
                    // Handle the error accordingly.
                    ModelState.AddModelError(string.Empty, "Failed to retrieve car.");
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

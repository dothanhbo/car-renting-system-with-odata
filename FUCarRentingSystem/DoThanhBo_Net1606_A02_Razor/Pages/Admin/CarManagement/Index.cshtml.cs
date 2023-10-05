using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace DoThanhBo_Net1606_A02_Razor.Pages.Admin.CarManagement
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [BindProperty(SupportsGet = true)]
        public string Keyword { get; set; }
        public IList<Car> Car { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("http://localhost:5044/api/"); // Adjust the base URL as needed.

                // Send an HTTP GET request to retrieve the list of CarInformation from the API.
                var response = await client.GetAsync("Car"); // Use the appropriate endpoint URL.

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a JSON string.
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into a list of CarInformation.
                    Car = JsonConvert.DeserializeObject<List<Car>>(responseContent);
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
        public async Task<IActionResult> OnPostAsync()
        {
            if (Keyword == null)
                return RedirectToPage("./Index");
            else
                try
                {
                    var client = _httpClientFactory.CreateClient();
                    client.BaseAddress = new Uri("http://localhost:5044/api/");
                    var response = await client.GetAsync("Car?$filter=contains(tolower(carName),%27" + Keyword.ToLower() +"%27)");
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        Car = JsonConvert.DeserializeObject<List<Car>>(responseContent);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to retrieve car information.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                }
            return Page();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Newtonsoft.Json;
using System.Text;

namespace DoThanhBo_Net1606_A02_Razor.Pages.Admin.CarManagement
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
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
            List<CarProducer> carProducers = new List<CarProducer>();
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("http://localhost:5044/api/"); // Adjust the base URL as needed.
                // Send an HTTP GET request to retrieve the list of CarInformation from the API.
                var response = await client.GetAsync("CarProducer");
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a JSON string.
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into a list of CarInformation.
                    carProducers = JsonConvert.DeserializeObject<List<CarProducer>>(responseContent);
                }
                else
                {
                    // Handle the error accordingly.
                    ModelState.AddModelError(string.Empty, "Failed to retrieve manufacturers.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the API call.
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            }
            ViewData["CarProducerId"] = new SelectList(carProducers, "Id", "ProducerName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("http://localhost:5044/api/"); // Adjust the base URL as needed.
                // Send an HTTP GET request to retrieve the list of CarInformation from the API.
                var carInfoJson = JsonConvert.SerializeObject(Car);
                var content = new StringContent(carInfoJson, Encoding.UTF8, "application/json");
                var response = await client.PutAsync("Car/" + Car.Id.ToString(), content);
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a JSON string.
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into a list of CarInformation.
                }
                else
                {
                    // Handle the error accordingly.
                    ModelState.AddModelError(string.Empty, "Failed to retrieve suppliers.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the API call.
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            }
            return RedirectToPage("./Index");
        }
    }
}

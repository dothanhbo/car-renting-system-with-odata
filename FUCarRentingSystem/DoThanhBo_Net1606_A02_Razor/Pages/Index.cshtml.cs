using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace DoThanhBo_Net1606_A02_Razor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult OnGet()
        {
            HttpContext.Session.Clear();
            return Page();
        }
        [BindProperty]
        public LoginModel loginModel { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("http://localhost:5044/api/"); // Adjust the base URL as needed.

                var loginJson = JsonConvert.SerializeObject(loginModel);
                var content = new StringContent(loginJson, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync("Customer/login", content);

                if (response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if (responseContent.Equals("Admin"))
                    {
                        HttpContext.Session.SetString("Admin", "Admin");
                        return RedirectToPage("/Admin/CarManagement/Index");
                    }
                    else
                    {
                        Customer customer = JsonConvert.DeserializeObject<Customer>(responseContent);
                        HttpContext.Session.SetInt32("Customer", customer.Id);
                        return RedirectToPage("/CustomerPages/Index");
                    }
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    // Unauthorized status code (401) indicates incorrect username or password.
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    return Page();
                }
                else
                {
                    // Handle other error cases here.
                    ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the API call.
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return Page();
            }
        }
    }
}
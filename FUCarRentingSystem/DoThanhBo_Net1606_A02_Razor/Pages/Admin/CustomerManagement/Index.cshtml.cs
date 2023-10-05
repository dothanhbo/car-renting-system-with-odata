using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Newtonsoft.Json;

namespace DoThanhBo_Net1606_A02_Razor.Pages.Admin.CustomerManagement
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
        public IList<Customer> Customer { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("http://localhost:5044/api/");
                var response = await client.GetAsync("Customer");
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Customer = JsonConvert.DeserializeObject<List<Customer>>(responseContent);
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
        public async Task<IActionResult> OnPostAsync()
        {
            if (Keyword == null)
                return RedirectToPage("./Index");
            else
                try
                {
                    var client = _httpClientFactory.CreateClient();
                    client.BaseAddress = new Uri("http://localhost:5044/api/");
                    var response = await client.GetAsync("Customer?$filter=contains(tolower(customerName),%27" + Keyword.ToLower() + "%27)");
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        Customer = JsonConvert.DeserializeObject<List<Customer>>(responseContent);
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

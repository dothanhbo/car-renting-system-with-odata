using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using Newtonsoft.Json;
using System.Text;
using System.Net;

namespace DoThanhBo_Net1606_A02_Razor.Pages.Admin.CustomerManagement
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("http://localhost:5044/api/");
                var customerInfoJson = JsonConvert.SerializeObject(Customer);
                var content = new StringContent(customerInfoJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Customer", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Customer " + Customer.CustomerName + " created successfully.";
                    return RedirectToPage("./Index");
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError("Customer.Email", "This email has beed used");
                        return Page();
                    }
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

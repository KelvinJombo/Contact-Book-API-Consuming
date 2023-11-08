using Microsoft.AspNetCore.Mvc;
using MyContactBook.UI.Models;
using MyContactBook.UI.Models.DTO;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace MyContactBook.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ContactDTO> response = new List<ContactDTO>();
            try
            {
                //Get All Contacts from the API project
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7106/api/contact");
                httpResponseMessage.EnsureSuccessStatusCode();
                
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<ContactDTO>>());
               
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }

            return View(response);
        }



        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddContactViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7106/api/contact"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            try
            {
                var response = await httpResponseMessage.Content.ReadFromJsonAsync<ContactDTO>();
                if (response != null)
                {
                    return RedirectToAction("Index", "Contact");
                }
            }
            catch (JsonException jsonEx)
            {
                // Handle JSON parsing errors, e.g., log the exception
                Console.WriteLine($"JSON Error: {jsonEx.Message}");
                
            }
                return BadRequest("Invalid JSON response");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
             var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<ContactDTO>($"https://localhost:7106/api/contact/{id.ToString()}");

            if (response != null)
            {
                return View(response);
            }

            return View(null);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ContactPutDto request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7106/api/contact/{request.ContactId}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
               var httpResponseMessage =  await client.SendAsync(httpRequestMessage);

                httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<ContactDTO>();

            if (response != null)
            {
                return RedirectToAction("Edit", "Contact");
            }

            return View(null);
        }





    }
}

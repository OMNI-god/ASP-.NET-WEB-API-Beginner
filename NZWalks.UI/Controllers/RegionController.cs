using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NZWalks.UI.Controllers
{
    public class RegionController : Controller
    {
        private readonly IHttpClientFactory httpClient;

        public RegionController(IHttpClientFactory httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var client = httpClient.CreateClient();
                var httpResponseMessage = await client.GetAsync("https://localhost:7127/api/regions");
                httpResponseMessage.EnsureSuccessStatusCode();
                var stringResponseBody = httpResponseMessage.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}

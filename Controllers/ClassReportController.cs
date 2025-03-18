using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace YourProject.Controllers
{
    public class ClassReportController : Controller
    {
        private readonly HttpClient _httpClient;

        public ClassReportController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetStringAsync("http://localhost:5022");  


            if (!string.IsNullOrEmpty(response))
            {
                var rows = response.Split('\n')
                                    .Select(row => row.Split(',').Select(col => col.Trim()).ToArray())
                                    .ToList();
                
                ViewBag.ReportData = rows; 
            }

            return View();
        }
    }
}

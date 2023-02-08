using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CarDealer.Service;

namespace CarDealer.Pages.Car
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; } = "Testing";
        public void OnGet()
        {
            CarDealer.Service.serviceSQL test = new CarDealer.Service.serviceSQL();
            test.TestQueries();
            this.Message = "Hello there!";
        }
    }
}

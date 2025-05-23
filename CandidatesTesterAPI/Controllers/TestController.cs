using Microsoft.AspNetCore.Mvc;

namespace CandidatesTesterAPI.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

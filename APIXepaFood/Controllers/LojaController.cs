using Microsoft.AspNetCore.Mvc;

namespace APIXepaFood.Controllers
{
    public class LojaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

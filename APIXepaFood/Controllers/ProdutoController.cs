using Microsoft.AspNetCore.Mvc;

namespace APIXepaFood.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
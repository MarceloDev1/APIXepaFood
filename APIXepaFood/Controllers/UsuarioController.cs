using Microsoft.AspNetCore.Mvc;

namespace APIXepaFood.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

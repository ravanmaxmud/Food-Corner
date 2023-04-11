using Microsoft.AspNetCore.Mvc;

namespace FoodCorner.Areas.Client.Controllers
{
    [Area("client")]
    [Route("chat")]
    public class ChatController : Controller
    {
        [HttpGet("index", Name = "client-chat-index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

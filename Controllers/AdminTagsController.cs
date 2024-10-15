using Microsoft.AspNetCore.Mvc;

namespace Blog_web_app.Controllers;

public class AdminTagsController : Controller
{
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
}
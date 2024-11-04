using Microsoft.AspNetCore.Mvc;

namespace Blog_web_app.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        return View();
    }
}
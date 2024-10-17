using Blog_web_app.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog_web_app.Controllers;

public class AdminTagsController : Controller
{
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    // [ActionName("Add")]
    public IActionResult Add(AddTagRequest addTagRequest)
    {
        var name = addTagRequest.Name;
        var displayName = addTagRequest.DisplayName;

        /* var name = Request.Form["name"];
        var displayName = Request.Form["displayName"]; */

        return View("Add");
    }
}
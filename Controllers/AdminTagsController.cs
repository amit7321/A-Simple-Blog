using System.ComponentModel.DataAnnotations;
using Blog_web_app.Data;
using Blog_web_app.Models.Domain;
using Blog_web_app.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog_web_app.Controllers;

public class AdminTagsController : Controller
{
    private readonly BlogDbContext blogDbContext;
    public AdminTagsController(BlogDbContext blogDbContext)
    {
        this.blogDbContext = blogDbContext;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    // [ActionName("Add")]
    public IActionResult Add(AddTagRequest addTagRequest)
    {
        var tag = new Tag
        {
            Name = addTagRequest.Name,
            DisplayName = addTagRequest.DisplayName
        };

        blogDbContext.Tags.Add(tag);
        blogDbContext.SaveChanges();

        // var name = addTagRequest.Name;
        // var displayName = addTagRequest.DisplayName;

        /* var name = Request.Form["name"];
        var displayName = Request.Form["displayName"]; */

        return View("Add");
    } 
}
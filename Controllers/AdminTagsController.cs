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
    [ActionName("Add")]
    public IActionResult AddList(AddTagRequest addTagRequest)
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

    [HttpGet]
    [ActionName("List")]
    public IActionResult ListTag()
    {
        var tags = blogDbContext.Tags.ToList();

        return View(tags);
    }

    [HttpGet]
    [ActionName("Edit")]
    public IActionResult EditTag(Guid id)
    {
        var tag = blogDbContext.Tags.FirstOrDefault(t => t.Id == id);

        if (tag != null)
        {
            var editTagRequest = new EditTagRequest
            {
                Id = tag.Id,
                Name = tag.Name,
                DisplayName = tag.DisplayName
            };

            return View(editTagRequest);
        }

        return View();
    }

    [HttpPost]
    [ActionName("Edit")]
    public IActionResult EditTag(EditTagRequest editTagRequest)
    {
        var tag = new Tag
        {
            Id = editTagRequest.Id,
            Name = editTagRequest.Name,
            DisplayName = editTagRequest.DisplayName
        };

        var currentTag = blogDbContext.Tags.Find(tag.Id);

        if (currentTag != null)
        {
            currentTag.Name = editTagRequest.Name;
            currentTag.DisplayName = editTagRequest.DisplayName;

            blogDbContext.SaveChanges();
            return RedirectToAction("EditTag", new { id = editTagRequest.Id });
        }

        return RedirectToAction("EditTag", new { id = editTagRequest.Id });
    }
}
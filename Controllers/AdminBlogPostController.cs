using Blog_web_app.Models.ViewModels;
using Blog_web_app.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog_web_app.Controllers;

public class AdminBlogPostController : Controller 
{
    private readonly ITagRepository tagRepository;

    public AdminBlogPostController(ITagRepository tagRepository)
    {
        this.tagRepository = tagRepository;
    }

    [HttpGet]
    [ActionName("Add")]
    public async Task<IActionResult> AddBlog()
    {
        var tags = await tagRepository.GetAllAsync();

        var model = new AddBlogPostRequest
        {
            Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString()})
        };

        return View(model);
    }
}
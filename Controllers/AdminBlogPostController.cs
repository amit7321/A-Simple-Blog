using Blog_web_app.Models.Domain;
using Blog_web_app.Models.ViewModels;
using Blog_web_app.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog_web_app.Controllers;

public class AdminBlogPostController : Controller
{
    private readonly ITagRepository tagRepository;
    private readonly IBlogPostRepository blogPostRepository;

    public AdminBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
    {
        this.tagRepository = tagRepository;
        this.blogPostRepository = blogPostRepository;
    }

    [HttpGet]
    [ActionName("Add")]
    public async Task<IActionResult> AddTag()
    {
        var tags = await tagRepository.GetAllAsync();

        var model = new AddBlogPostRequest
        {
            Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
        };

        return View(model);
    }

    [HttpPost]
    [ActionName("Add")]
    public async Task<IActionResult> AddBlogPost(AddBlogPostRequest addBlogPostRequest)
    {
        var blog = new BlogPost
        {
            Heading = addBlogPostRequest.Heading,
            PageTitle = addBlogPostRequest.PageTitle,
            Content = addBlogPostRequest.Content,
            ShortDescription = addBlogPostRequest.ShortDescription,
            FeatureImageUrl = addBlogPostRequest.FeatureImageUrl,
            UrlHandle = addBlogPostRequest.UrlHandle,
            PublishedDate = addBlogPostRequest.PublishedDate,
            Author = addBlogPostRequest.Author,
            Visible = addBlogPostRequest.Visible

        };

        var selectedTags = new List<Tag>();
        foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
        {
            var selectedTagIdGuid = Guid.Parse(selectedTagId);
            var existingTag = await tagRepository.GetAsync(selectedTagIdGuid);

            if (existingTag != null)
            {
                selectedTags.Add(existingTag);
            }
        }

        blog.Tags = selectedTags;

        await blogPostRepository.AddAsync(blog);

        return RedirectToAction("Add");
    }

    [HttpGet]
    [ActionName("List")]
    public async Task<IActionResult> BlogList()
    {
        var blogList = await blogPostRepository.GetAllAsync();

        return View(blogList);
    }
}
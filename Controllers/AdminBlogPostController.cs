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

    [HttpGet]
    [ActionName("Edit")]
    public async Task<IActionResult> EditBlog(Guid id)
    {
        var blog = await blogPostRepository.GetAsync(id);
        var tags = await tagRepository.GetAllAsync();

        if (blog != null)
        {
            var blogModel = new EditBlogPostRequest
            {
                Id = blog.Id,
                Heading = blog.Heading,
                PageTitle = blog.PageTitle,
                Content = blog.Content,
                ShortDescription = blog.ShortDescription,
                FeatureImageUrl = blog.FeatureImageUrl,
                UrlHandle = blog.UrlHandle,
                PublishedDate = blog.PublishedDate,
                Author = blog.Author,
                Visible = blog.Visible,
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
                SelectedTags = blog.Tags.Select(x => x.Id.ToString()).ToArray()
            };

            return View(blogModel);
        }

        return View(null);
    }

    [HttpPost]
    [ActionName("Edit")]
    public async Task<IActionResult> EditBlog(EditBlogPostRequest editBlogPostRequest)
    {
        var editedBlog = new BlogPost
        {
            Id = editBlogPostRequest.Id,
            Heading = editBlogPostRequest.Heading,
            PageTitle = editBlogPostRequest.PageTitle,
            Content = editBlogPostRequest.Content,
            ShortDescription = editBlogPostRequest.ShortDescription,
            FeatureImageUrl = editBlogPostRequest.FeatureImageUrl,
            UrlHandle = editBlogPostRequest.UrlHandle,
            PublishedDate = editBlogPostRequest.PublishedDate,
            Author = editBlogPostRequest.Author,
            Visible = editBlogPostRequest.Visible,
        };

        var selectedTags = new List<Tag>();
        foreach (var selectedTag in editBlogPostRequest.SelectedTags)
        {
            if (Guid.TryParse(selectedTag, out var tag))
            {
                var foungTag = await tagRepository.GetAsync(tag);

                if (foungTag != null)
                {
                    selectedTags.Add(foungTag);
                }
            }
        }

        editedBlog.Tags = selectedTags;

        var updatedBlog = await blogPostRepository.UpdateAsync(editedBlog);

        if (updatedBlog != null)
        {
            return RedirectToAction("List");
        }

        return RedirectToAction("List");
    }
}
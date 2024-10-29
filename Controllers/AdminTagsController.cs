using Blog_web_app.Data;
using Blog_web_app.Models.Domain;
using Blog_web_app.Models.ViewModels;
using Blog_web_app.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog_web_app.Controllers;

public class AdminTagsController : Controller
{
    private readonly ITagRepository tagRepository;
    public AdminTagsController(ITagRepository tagRepository)
    {
        this.tagRepository = tagRepository;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    [ActionName("Add")]
    public async Task<IActionResult> Add(AddTagRequest addTagRequest)
    {
        var tag = new Tag
        {
            Name = addTagRequest.Name,
            DisplayName = addTagRequest.DisplayName
        };

        await tagRepository.AddAsync(tag);

        return RedirectToAction("List");
    }

    [HttpGet]
    [ActionName("List")]
    public async Task<IActionResult> ListTag()
    {
        var tags = await tagRepository.GetAllAsync();
        return View(tags);
    }

    [HttpGet]
    [ActionName("Edit")]
    public async Task<IActionResult> EditTag(Guid id)
    {
        var tag = await tagRepository.GetAsync(id);

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

        return View("List");
    }

    [HttpPost]
    [ActionName("Edit")]
    public async Task<IActionResult> EditTag(EditTagRequest editTagRequest)
    {
        var tag = new Tag
        {
            Id = editTagRequest.Id,
            Name = editTagRequest.Name,
            DisplayName = editTagRequest.DisplayName
        };

        var updatedTag = await tagRepository.UpdateAsync(tag);

        if(updatedTag != null)
        {

        }
        else
        {

        }

        return RedirectToAction("List", new { id = editTagRequest.Id });
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteTag(EditTagRequest editTagRequest)
    {
        var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

        if (deletedTag != null)
        {
            return RedirectToAction("List");
        }


        return RedirectToAction("List", new { id = editTagRequest.Id });

    }


}
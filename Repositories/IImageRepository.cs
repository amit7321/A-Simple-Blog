namespace Blog_web_app.Repositories;

public interface IImageRepositroy
{
    Task<string> UploadImageAync(IFormFile file);
}
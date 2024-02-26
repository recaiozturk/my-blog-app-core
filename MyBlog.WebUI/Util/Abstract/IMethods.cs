using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyBlog.WebUI.Models.UtilModels;

namespace MyBlog.WebUI.Util.Abstract
{
    public interface IMethods
    {
        List<string> ModelErrors(ModelStateDictionary modelState);
        Task<ImageFileModel> CreateImageFileAsync(IFormFile imageFile, int imageType);
        Task DeletePortfolioImage(string fileName);
    }
}

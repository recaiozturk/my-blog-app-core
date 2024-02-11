using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyBlog.WebUI.Models.UtilModels;
using NuGet.Packaging.Signing;

namespace MyBlog.WebUI.Util
{
    public class Methods
    {
        public List<String> ModelErrors(ModelStateDictionary modelState)
        {
            var modelErrors = new List<string>();
            foreach (var mState in modelState.Values)
            {
                foreach (var modelError in mState.Errors)
                {
                    modelErrors.Add(modelError.ErrorMessage);
                }
            }
            return modelErrors;
        }

        //image file creator
        public async Task<ImageFileModel> CreateImageFileAsync(IFormFile imageFile)
        {
            ImageFileModel imageFileModel = new ImageFileModel();
            string tempString = "";
            imageFileModel.IsValid = false;
            if (imageFile != null)
            {
                string[] allowedExtension = new[] { ".jpg",".JPG", ".jpeg", ".png" };
                tempString = Path.GetExtension(imageFile.FileName); // abc.jpg

                if (!allowedExtension.Contains(tempString))
                {
                    tempString = "only \".jpg\", \".jpeg\", \".png\" types are valid";
                    imageFileModel.ErrorString = tempString;
                    return imageFileModel;

                }
                else
                {
                    var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{tempString}");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    imageFileModel.IsValid = true;
                    imageFileModel.ImageCreatedName = randomFileName;
                    return imageFileModel;
                }
            }
            imageFileModel.ErrorString = "Please choose a image file";
            return imageFileModel;
        }
    }
}

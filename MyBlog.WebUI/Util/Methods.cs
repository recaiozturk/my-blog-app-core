using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using MyBlog.WebUI.Models.UtilModels;
using MyBlog.WebUI.Util.Abstract;
using NuGet.Packaging.Signing;

namespace MyBlog.WebUI.Util
{
    public class Methods: IMethods
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
        public async Task<ImageFileModel> CreateImageFileAsync(IFormFile imageFile,int imageType)
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

                    string path = "";

                    if (imageType == (int)Enums.ImageType.Profile)
                    {
                        await DeleteOldImages();
                        path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/profilepicture", randomFileName);
                    }
                    else
                    {
                        path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/portfoliopicture", randomFileName);
                    }
                        
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

        public async Task DeletePortfolioImage(string fileName)
        {
            try
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "portfoliopicture");
                string filePath = Path.Combine(imagePath, fileName);


                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private async Task DeleteOldImages()
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "profilepicture");

            if (Directory.Exists(imagePath))
            {
                try
                {
                    string[] files = Directory.GetFiles(imagePath);

                    foreach (string file in files)
                    {
                        File.Delete(file);

                    }
                }
                catch (Exception ex)
                {
                }
            }
        }


    }
}

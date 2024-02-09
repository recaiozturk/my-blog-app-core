using Microsoft.AspNetCore.Mvc.ModelBinding;

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
    }
}

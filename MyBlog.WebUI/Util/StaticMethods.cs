using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Encodings.Web;

namespace MyBlog.WebUI.Util
{
    public static class StaticMethods
    {
        public static List<string> SplitSentences(string text)
        {
            string[] sentenceArray = text.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            List<string> sentences = new List<string>();
            foreach (var sentence in sentenceArray)
            {
                string trimmedSentence = sentence.Trim();
                sentences.Add(trimmedSentence);
            }

            return sentences;
        }

        public static async Task<string> RenderViewComponentToStringAsync(this Controller controller, string componentName, object arguments = null)
        {
            // HttpContext üzerinden servisleri alıyoruz
            var services = controller.HttpContext.RequestServices;

            // IViewComponentHelper örneği alınıyor ve ViewContext ayarlanıyor
            var viewComponentHelper = services.GetRequiredService<IViewComponentHelper>() as DefaultViewComponentHelper;
            var viewContext = new ViewContext(
                controller.ControllerContext,
                new FakeView(),
                controller.ViewData,
                controller.TempData,
                TextWriter.Null,
                new HtmlHelperOptions()
            );

            viewComponentHelper.Contextualize(viewContext);

            using (var writer = new StringWriter())
            {
                var result = await viewComponentHelper.InvokeAsync(componentName, arguments);
                result.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }

        private class FakeView : IView
        {
            public string Path => string.Empty;

            public Task RenderAsync(ViewContext context)
            {
                return Task.CompletedTask;
            }
        }
    }
}

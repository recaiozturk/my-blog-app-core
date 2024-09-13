using Microsoft.AspNetCore.Mvc;
using MyBlog.WebUI.Models.Contact;

namespace MyBlog.WebUI.ViewComponents
{
    public class ContactViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ContactViewModel viewModel = new ContactViewModel();
            viewModel.ContactFormViewModel = new ContactFormViewModel();

            return View(viewModel);
        }
    }
}

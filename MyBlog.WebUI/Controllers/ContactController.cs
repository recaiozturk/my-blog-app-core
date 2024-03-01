using Microsoft.AspNetCore.Mvc;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.DataAccess.Concrate.EfCore;
using MyBlog.WebUI.Models.Contact;
using MyBlog.WebUI.Util.Abstract;
using System.Security.Policy;

namespace MyBlog.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactDal _contactDal;
        private readonly IMethods _methods;
        private readonly IEmailSender _emailSender;

        public ContactController(IContactDal contactDal, IMethods methods, IEmailSender emailSender)
        {
            _contactDal = contactDal;
            _methods = methods;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index()//admin contact
        {
            var contacts = await _contactDal.GetAllMessages();
            return View(contacts);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteMessage(int MessageId)
        {
            bool valid = false;

            try
            {
                if (MessageId != 0)
                {
                    var portf = await _contactDal.GetById(MessageId);
                    await _contactDal.DeleteAsync(portf);
                    valid = true;
                }
            }
            catch (Exception)
            {
                //log error
            }

            return Json(new
            {
                IsValid = valid,
                ErrorMessage = "Error !!",
            });
        }

        [HttpPost]
        public async Task<JsonResult> SendMessage(ContactFormViewModel model)
        {
            List<string> allErrors = new List<string>();
            bool valid = false;

            if (ModelState.IsValid)
            {
                try
                {
                    await _contactDal.CreateAsync(new Entity.Contact
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Subject = model.Subject,
                        Message = model.Message,
                    });

                    await _emailSender.SendEmailAsync("recaiozturk54@gmail.com", model.Name, model.Email, model.Subject, model.Message);
                    valid = true;
                }
                catch (Exception)
                {
                    //hatayı mail olarak gönderebiliriz kendimize
                    allErrors.Add("Beklenmedik Hata!");
                    valid = false;

                    return Json(new
                    {
                        IsValid = valid,
                        ErrorMessages = allErrors,
                    });
                }
            }

            allErrors = _methods.ModelErrors(ModelState);

            return Json(new
            {
                IsValid = valid,
                ErrorMessages = allErrors,
            });
        }
    }
}

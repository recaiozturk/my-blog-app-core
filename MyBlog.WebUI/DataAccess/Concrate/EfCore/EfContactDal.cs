using Microsoft.EntityFrameworkCore;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.Entity;

namespace MyBlog.WebUI.DataAccess.Concrate.EfCore
{
    public class EfContactDal : EFGenericDal<Contact>, IContactDal
    {
        private readonly ApplicationDbContext _context;
        public EfContactDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Contact>> GetAllMessages()
        {
            return  _context.Contacts.ToListAsync();

        }
    }
}

using Microsoft.EntityFrameworkCore;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.Entity;

namespace MyBlog.WebUI.DataAccess.Concrate.EfCore
{
    public class EfPortfolioDal:EFGenericDal<Portfolio>,IPortfolioDal
    {
        private readonly ApplicationDbContext _context;
        public EfPortfolioDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Portfolio>> GetPortfolios()
        {
            return await _context.Portfolios.ToListAsync();
        }
    }
}

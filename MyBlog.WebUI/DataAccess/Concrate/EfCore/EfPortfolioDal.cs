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

        public async Task DeletePortfolioWithImages(int id)
        {
            var imagesToDelte=await _context.ProjectImages.Where(x => x.Id == id).ToListAsync();

            _context.ProjectImages.RemoveRange(imagesToDelte);
            var portf = await _context.Portfolios.FindAsync(id);

            if(portf!=null)
                _context.Portfolios.Remove(portf);

            await _context.SaveChangesAsync();
        }

        public async  Task<Portfolio> GetPortfolioById(int id)
        {
            return await _context.Portfolios.Include(x => x.ProjectImages).FirstOrDefaultAsync(x => x.Id==id);
        }

        public async Task<List<Portfolio>> GetPortfolios()
        {
            return await _context.Portfolios.Include(x => x.ProjectImages).ToListAsync();
        }

        public async Task ResetPortfolioImagesIsCover(int id)
        {
            var portf = await GetPortfolioById(id);
            foreach (var image in portf.ProjectImages)
            {
                image.IsCoverImage = false;
            }

            await _context.SaveChangesAsync();
        }
    }
}

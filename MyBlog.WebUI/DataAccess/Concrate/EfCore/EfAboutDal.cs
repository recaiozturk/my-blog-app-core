using Microsoft.EntityFrameworkCore;
using MyBlog.WebUI.DataAccess.Abstract;
using MyBlog.WebUI.Entity;

namespace MyBlog.WebUI.DataAccess.Concrate.EfCore
{
    public class EfAboutDal : EFGenericDal<About>, IAboutDal
    {
        private readonly ApplicationDbContext _context;
        public EfAboutDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Skill> AddSkillToAbout(Skill entity)
        {
            await _context.Skills.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteSkillForAbout(int id)
        {
            var skill = _context.Skills.FirstOrDefault(s => s.Id == id);
            _context.Skills.Remove(skill);
            _context.SaveChangesAsync();
        }

        public async Task EditSkillForAbout(Skill skill)
        {
            _context.Entry(skill).State = EntityState.Modified; 
            _context.SaveChangesAsync();
        }

        public async Task<About> GetAboutAsync()
        {
            var about = await GetAll().FirstAsync();
            about.Skills = await GetSkillsForAboutAsync();
            return about;

        }

        public async Task<Skill> GetSkillById(int id)
        {
            return await _context.Skills.FindAsync(id);
        }

        public async Task<List<Skill>> GetSkillsForAboutAsync()
        {
            return await _context.Skills.ToListAsync();
        }

        public async Task UpdateAbout(About entity)
        {
            var about = _context.Abouts.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (about != null)
            {
                about.Result.Title = entity.Title;
                about.Result.Adress = entity.Adress;
                about.Result.Age = entity.Age;
                about.Result.Birthday = entity.Birthday;
                about.Result.Email = entity.Email;
                about.Result.FavoriteBook = entity.FavoriteBook;
                about.Result.FavoriteMovie = entity.FavoriteMovie;
                about.Result.FavoriteMusic = entity.FavoriteMusic;
                about.Result.FavoriteSerie = entity.FavoriteSerie;
                about.Result.PhoneNumber = entity.PhoneNumber;
                about.Result.Summary = entity.Summary;
                about.Result.Website = entity.Website;
                about.Result.Image=entity.Image;

                _context.SaveChangesAsync();
            }
        }

    }
}

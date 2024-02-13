using MyBlog.WebUI.Entity;

namespace MyBlog.WebUI.DataAccess.Abstract
{
    public interface IAboutDal:IGenericDal<About>
    {
        Task<About> GetAboutAsync();
        Task<List<Skill>> GetSkillsForAboutAsync();

        Task UpdateAboutAsync(About entity);

        Task<Skill> AddSkillToAboutAsync(Skill entity);

        Task EditSkillForAboutAsync(Skill skill);

        Task<Skill> GetSkillByIdAsync(int id);

        Task DeleteSkillForAboutAsync(int id);
    }
}

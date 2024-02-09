using MyBlog.WebUI.Entity;

namespace MyBlog.WebUI.DataAccess.Abstract
{
    public interface IAboutDal:IGenericDal<About>
    {
        Task<About> GetAboutAsync();
        Task<List<Skill>> GetSkillsForAboutAsync();

        Task UpdateAbout(About entity);

        Task<Skill> AddSkillToAbout(Skill entity);

        Task EditSkillForAbout(Skill skill);

        Task<Skill> GetSkillById(int id);

        Task DeleteSkillForAbout(int id);
    }
}

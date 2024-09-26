using System.ComponentModel.DataAnnotations;

namespace MyBlog.WebUI.Models.User
{
    public class EditViewModel
    {
        public string Id { get; set; }
        public string? UserName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public IList<string> SelectedRoles { get; set; } = new List<string>();
    }
}

namespace MyBlog.WebUI.Entity
{
    public class Resume
    {
        public int Id { get; set; }
        public About About { get; set; }
        public int MyProperty { get; set; }
    }

    public class BaseEdExep
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? DateBetween { get; set; }
        public string? Adress { get; set; }
    }

    public class Education :BaseEdExep
    {
        public string? UniversityName { get; set; }
        public string? Description { get; set; }
    }

    public class Experience:BaseEdExep
    {
        public string? CompanyName { get; set; }
        public List<String> CompanyDetails { get; set; }
    }
}

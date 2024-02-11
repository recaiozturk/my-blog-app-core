namespace MyBlog.WebUI.Entity
{
    public class About
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime Birthday { get; set; }
        public string ? Website { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Age { get; set; }
        public string? Email { get; set; }
        public string? Adress  { get; set; }

        public string? FavoriteMovie { get; set; }
        public string? FavoriteSerie { get; set; }
        public string? FavoriteMusic { get; set; }
        public string? FavoriteBook { get; set; }
        public string? Summary { get; set; }
        public string? Image { get; set; }

        public List<Skill>? Skills { get; set; }
    }
}

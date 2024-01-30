using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Data.Entity
{
    public class MyFavorite
    {
        public int Id { get; set; }
        public string? FavMovie { get; set; }
        public string? FavSerie { get; set; }
        public string? FavMusic { get; set; }
        public string? FavBook { get; set; }
    }
}

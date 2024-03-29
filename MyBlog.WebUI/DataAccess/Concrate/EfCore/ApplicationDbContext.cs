﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBlog.WebUI.Entity;

namespace MyBlog.WebUI.DataAccess.Concrate.EfCore
{
    public class ApplicationDbContext:IdentityDbContext<IdentityUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public DbSet<Education> Educations { get; set; }

        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Portfolio> Portfolios{ get; set; }
        public DbSet<ProjectImage> ProjectImages { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}

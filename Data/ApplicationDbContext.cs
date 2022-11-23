using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web_Programming_Project.Controllers.Utils;
using Web_Programming_Project.Models;

namespace Web_Programming_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private ImageManager _imageManager;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            _imageManager = new ImageManager();
        }

        public ImageManager ImgManager { get { return _imageManager; } }

        public DbSet<Web_Programming_Project.Models.Theme> Theme { get; set; }

        public DbSet<Web_Programming_Project.Models.BrickColor> BrickColor { get; set; }

        public DbSet<Web_Programming_Project.Models.Brick> Brick { get; set; }

        public DbSet<Web_Programming_Project.Models.BrickInBox> BrickInBox { get; set; }

        public DbSet<Web_Programming_Project.Models.Box> Box { get; set; }
    }
}
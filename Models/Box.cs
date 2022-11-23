using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Web_Programming_Project.Data.Enum;

namespace Web_Programming_Project.Models
{
    public class Box
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "text")]
        public string BoxName { get; set; }

        [ForeignKey(nameof(Theme))]
        public int BoxThemeId { get; set; }

        public virtual Theme? BoxTheme { get; set; }

        [EnumDataType(typeof(AgeCategoryEnum))]
        public AgeCategoryEnum BoxAgeCategory { get; set; }

        [Column(TypeName = "text")]
        public string? BoxImgName { get; set; }

        [NotMapped]
        [DisplayName("Image")]
        public IFormFile? BoxImgFile { get; set; }

        [Column(TypeName = "text")]
        public string BoxDescription { get; set; }

        [NotMapped]
        public virtual ICollection<BrickInBox> BoxBricksInBox { get; set; }

        public Box()
        {
            this.BoxBricksInBox = new HashSet<BrickInBox>();
        }
    }
}

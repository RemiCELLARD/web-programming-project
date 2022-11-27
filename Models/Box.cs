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

        [Column(TypeName = "varchar(255)")]
        [DisplayName("Name")]
        [MaxLength(255)]
        [Required]
        public string BoxName { get; set; }

        [ForeignKey(nameof(Theme))]
        [DisplayName("Theme")]
        [Required]
        public int BoxThemeId { get; set; }

        [DisplayName("Theme")]
        public virtual Theme? BoxTheme { get; set; }

        [EnumDataType(typeof(AgeCategoryEnum))]
        [DisplayName("Age Category")]
        [Required]
        public AgeCategoryEnum BoxAgeCategory { get; set; }

        [Column(TypeName = "text")]
        [DisplayName("Image")]
        public string? BoxImgName { get; set; }

        [NotMapped]
        [DisplayName("Image")]
        public IFormFile? BoxImgFile { get; set; }

        [Column(TypeName = "text")]
        [DisplayName("Description")]
        [Required]
        public string BoxDescription { get; set; }

        [NotMapped]
        public virtual ICollection<BrickInBox> BoxBricksInBox { get; set; }

        public Box()
        {
            this.BoxBricksInBox = new HashSet<BrickInBox>();
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Web_Programming_Project.Data.Enum;

namespace Web_Programming_Project.Models
{
    public partial class Theme
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        [MaxLength(50)]
        public string ThemeName { get; set; }

        [Column(TypeName = "text")]
        public string ThemeDescription { get; set; }

        [Column(TypeName = "text")]
        public string? ThemeImgName { get; set; }

        [NotMapped]
        [DisplayName("Image")]
        public IFormFile? ThemeImgFile { get; set; }

        [EnumDataType(typeof(AgeCategoryEnum))]
        public AgeCategoryEnum ThemeAgeCategory { get; set; }

    }
}

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Web_Programming_Project.Data.Enum;

namespace Web_Programming_Project.Models
{
    public partial class Brick
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Name")]
        [Column(TypeName = "varchar(50)")]
        [MaxLength(50)]
        public string BrickName { get; set; }

        [DisplayName("Color")]
        [ForeignKey(nameof(BrickColor))]
        [Column(TypeName = "int")]
        public int BrickColorId { get; set; }
        
        [DisplayName("Color")]
        public virtual BrickColor? BrickColorObj { get; set; }

        [DisplayName("Size")]
        [EnumDataType(typeof(BrickSizeEnum))]
        public BrickSizeEnum BrickSize { get; set; }

        [DisplayName("Price (EUR)")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "float")]
        public float BrickPrice { get; set; }
    }
}

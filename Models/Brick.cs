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

        [Column(TypeName = "text")]
        public string BrickName { get; set; }

        [ForeignKey(nameof(BrickColor))]
        [Column(TypeName = "int")]
        public int BrickColorId { get; set; }
        public virtual BrickColor? BrickColorObj { get; set; }

        [EnumDataType(typeof(BrickSizeEnum))]
        public BrickSizeEnum BrickSize { get; set; }

        [Column(TypeName = "float")]
        public float BrickPrice { get; set; }
    }
}

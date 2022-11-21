using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Programming_Project.Models
{
    public partial class BrickColor
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "text")]
        public string BrickColorName { get; set; }

        [Column(TypeName = "int")]
        public int BrickColorRed { get; set; }

        [Column(TypeName = "int")]
        public int BrickColorGreen { get; set; }

        [Column(TypeName = "int")]
        public int BrickColorBlue { get; set; }

        [Column(TypeName = "float")]
        public float BrickColorAlpha { get; set; }
    }
}

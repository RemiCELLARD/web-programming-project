using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Programming_Project.Models
{
    public partial class BrickColor
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Name")]
        [Column(TypeName = "varchar(50)")]
        [MaxLength(50)]
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

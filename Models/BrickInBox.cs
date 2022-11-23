using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Programming_Project.Models
{
    public partial class BrickInBox
    {
        [Key] // wtf microsoft
        public int Id { get; set; }

        [ForeignKey(nameof(Box))]
        public int BrickInBoxBoxId { get; set; }
        public virtual Box? BrickInBoxBox { get; set; }

        [ForeignKey(nameof(Brick))]
        public int BrickInBoxBrickId { get; set; }

        public virtual Brick? BrickInBoxBrick { get; set; }

        [Column(TypeName = "int")]
        public int BrickInBoxQuantity { get; set; }
    }
}

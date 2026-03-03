namespace Minigram.Core.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Relation : BaseModel
    {
        [Required]
        public User Sender { get; set; } = null!;

        [Required]
        public User Receiver { get; set; } = null!;

        public tRelationshipStatus Status { get; set; }
    }
}

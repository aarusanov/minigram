namespace Minigram.Core.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Profile : BaseModel
    {
        [Required]
        public string Login { get; set; } = string.Empty;

        [Url]
        public string? PhotoUrl { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public User User { get; set; } = null!;
    }
}

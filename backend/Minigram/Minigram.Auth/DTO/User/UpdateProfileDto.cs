namespace Minigram.Auth.Dto.User
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateProfileDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Url]
        public string? PhotoUrl { get; set; }
    }
}

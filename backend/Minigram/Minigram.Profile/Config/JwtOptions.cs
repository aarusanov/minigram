namespace Minigram.Profile.Options
{
    using System.ComponentModel.DataAnnotations;

    public class JwtOptions
    {
        public const string SectionName = "Jwt";

        [Required]
        public string Secret { get; set; } = string.Empty;

        [Required]
        public string Audience {get; set; } = string.Empty;

        [Required]
        public string Issuer {get; set; } = string.Empty;
    }
}
namespace Minigram.Auth.Dto.Profile
{
    public class ReadProfileDto
    {
        public string Login { get; set; } = string.Empty;

        public string? PhotoUrl { get; set; }

        public bool IsActive { get; set; }
    }
}

namespace Minigram.Profile.Dto
{
    public class ProfileResponseDto
    {
        public Guid UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? PhotoUrl { get; set; }
    }
}

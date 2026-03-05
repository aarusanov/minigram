namespace Minigram.Auth.Dto.User
{
    public class ReadUserDto
    {
        public Guid UserId { get; set; }

        public string Email { get; set; } = string.Empty;

        public bool IsEmailVerified { get; set; }

        public string? Name { get; set; }

        public string? PhotoUrl { get; set; }
    }
}

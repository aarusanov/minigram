namespace Minigram.Core.Models
{
    public class Profile : BaseModel
    {
        public Guid UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? PhotoUrl { get; set; }
    }
}

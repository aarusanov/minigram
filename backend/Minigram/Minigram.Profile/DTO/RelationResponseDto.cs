namespace Minigram.Profile.Dto
{
    using Minigram.Profile.Models;

    public class RelationResponseDto
    {
        public tRelationshipStatus Status { get; set; }

        public ProfileResponseDto Profile { get; set; } = null!;
    }
}

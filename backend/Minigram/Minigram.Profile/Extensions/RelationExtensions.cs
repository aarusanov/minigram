namespace Minigram.Profile.Extensions
{
    using Minigram.Profile.Dto;
    using Minigram.Profile.Models;

    internal static class RelationExtensions
    {
        public static RelationResponseDto ToDto(this Relation relation)
        {
            ArgumentNullException.ThrowIfNull(relation);

            return new RelationResponseDto
            {
                Status = relation.Status,
                Profile = relation.Receiver.ToDto(),
            };
        }
    }
}
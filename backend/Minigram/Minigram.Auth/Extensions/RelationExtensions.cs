namespace Minigram.Auth.Extensions
{
    using Minigram.Auth.Dto.Profile;
    using Minigram.Core.Models;

    public static class RelationExtensions
    {
        public static ReadRelationDto ToDto(this Relation relation)
        {
            ArgumentNullException.ThrowIfNull(relation);

            return new ReadRelationDto
            {
                Status = relation.Status,
                Profile = relation.Receiver.ToProfileDto(),
            };
        }
    }
}
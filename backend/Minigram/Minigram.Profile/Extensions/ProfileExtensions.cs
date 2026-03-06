namespace Minigram.Profile.Extensions
{
    using Minigram.Profile.Dto;
    using Minigram.Profile.Models;

    internal static class ProfileExtensions
    {
        public static ProfileResponseDto ToDto(this Profile profile)
        {
            ArgumentNullException.ThrowIfNull(profile);

            return new ProfileResponseDto
            {
                UserId = profile.UserId,
                Name = profile.Name,
                PhotoUrl = profile.PhotoUrl,
            };
        }
    }
}
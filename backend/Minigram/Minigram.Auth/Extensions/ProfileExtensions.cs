namespace Minigram.Auth.Extensions
{
    using Minigram.Auth.Dto.Profile;
    using Minigram.Core.Models;

    public static class ProfileExtensions
    {
        public static ReadProfileDto ToDto(this Profile profile)
        {
            ArgumentNullException.ThrowIfNull(profile);

            return new ReadProfileDto
            {
                Login = profile.Login,
                PhotoUrl = profile.PhotoUrl,
                IsActive = profile.IsActive,
            };
        }
    }
}
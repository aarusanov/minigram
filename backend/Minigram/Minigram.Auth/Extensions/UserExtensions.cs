namespace Minigram.Auth.Extensions
{
    using Minigram.Auth.Dto.User;
    using Minigram.Core.Models;

    public static class UserExtensions
    {
        public static ReadProfileDto ToProfileDto(this User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            return new ReadProfileDto
            {
                UserId = user.Id,
                Name = user.Profile.Name,
                PhotoUrl = user.Profile.PhotoUrl,
            };
        }

        public static ReadUserDto ToUserDto(this User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            return new ReadUserDto
            {
                UserId = user.Id,
                Email = user.Email,
                IsEmailVerified = user.IsEmailVerified,
                Name = user.Profile.Name,
                PhotoUrl = user.Profile.PhotoUrl,
            };
        }
    }
}
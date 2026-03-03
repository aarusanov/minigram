namespace Minigram.Auth.Extensions
{
    using Minigram.Auth.Dto.User;
    using Minigram.Core.Models;

    public static class UserExtensions
    {
        public static ReadUserDto ToDto(this User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            return new ReadUserDto
            {
                Email = user.Email,
                Phone = user.Phone,
                IsEmailVerified = user.IsEmailVerified,
                IsPhoneVerified = user.IsPhoneVerified,
                Profiles = user.Profiles.Select(p => p.ToDto()).ToList()
            };
        }
    }
}
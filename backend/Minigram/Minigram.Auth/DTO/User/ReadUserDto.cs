namespace Minigram.Auth.Dto.User
{
    using Minigram.Auth.Dto.Profile;

    public class ReadUserDto
    {
        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; }  = string.Empty;

        public bool IsEmailVerified { get; set; }

        public bool IsPhoneVerified { get; set; }

        public List<ReadProfileDto> Profiles { get; set; } = new List<ReadProfileDto>();
    }
}

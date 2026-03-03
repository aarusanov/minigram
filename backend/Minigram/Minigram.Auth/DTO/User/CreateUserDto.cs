namespace Minigram.Auth.Dto.User
{
    using System.ComponentModel.DataAnnotations;
    using Minigram.Auth.Dto.Profile;

    public class CreateUserDto : IValidatableObject
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; }  = string.Empty;

        public bool IsEmailVerified { get; set; }

        public bool IsPhoneVerified { get; set; }

        public List<ReadProfileDto> Profiles { get; set; } = new List<ReadProfileDto>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

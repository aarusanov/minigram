namespace Minigram.Core.Models
{
    using System.ComponentModel.DataAnnotations;

    public class User : BaseModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; }  = string.Empty;

        public bool IsEmailVerified { get; set; }

        public bool IsPhoneVerified { get; set; }

        public List<Profile> Profiles { get; set; } = new List<Profile>();
    }
}

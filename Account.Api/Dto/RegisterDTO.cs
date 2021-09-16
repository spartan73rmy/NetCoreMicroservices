using System.ComponentModel.DataAnnotations;

namespace Account.Api.Dto
{
    public class RegisterDTO
    {
        [Required]
        [MinLength(3)]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

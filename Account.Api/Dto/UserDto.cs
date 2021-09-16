using System.ComponentModel.DataAnnotations;

namespace Account.Api.Dto
{
    public class UserDto
    {
        [Required]
        [MinLength(3)]
        public string UserName { get; set; }

        [Required]
        public string Token { get; set; }
    }
}

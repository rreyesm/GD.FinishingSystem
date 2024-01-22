using System.ComponentModel.DataAnnotations;

namespace GD.Finishing.WebAPI.Models
{
    public class LoginInfo
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

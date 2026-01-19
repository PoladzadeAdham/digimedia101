using System.ComponentModel.DataAnnotations;

namespace digimedia101.ViewModel.UserViewModel
{
    public class LoginVm
    {
        [Required, MaxLength(256), MinLength(2), EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(256), MinLength(6), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace digimedia101.ViewModel.UserViewModel
{
    public class RegisterVm
    {
        [Required, MaxLength(256), MinLength(2)]
        public string Username { get; set; }
        [Required, MaxLength(256), MinLength(2)]
        public string Fullname { get; set; }
        [Required, MaxLength(256), MinLength(2), EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(256), MinLength(6), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MaxLength(256), MinLength(6), DataType(DataType.Password), Compare(nameof(Password))]                  
        public string ConfirmPassword { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ViewModels
{
    public class LoginVM
    {
        [Display(Name ="Ten dang nhap")]
        [Required(ErrorMessage ="*")]
        [MaxLength(20, ErrorMessage ="Toi da 20 ky tu")]
        public string UserName { get; set; }

        [Display(Name = "Mat Khau")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

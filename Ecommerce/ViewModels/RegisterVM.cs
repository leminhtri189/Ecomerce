using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Ten Dang Nhap")]
        [Required(ErrorMessage ="*")]
        [MaxLength(20,ErrorMessage ="Toi da 20 ky tu") ]
        public string MaKh { get; set; }

        [Display(Name = "Mat Khau")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="*")]
        public string MatKhau { get; set; }

        [Display(Name ="Ho ten")]
        [Required(ErrorMessage ="*")]
        [MaxLength(50,ErrorMessage ="Toi da 50 ky tu")]
        public string HoTen { get; set; }

        public bool GioiTinh { get; set; } = true;

        [Display(Name ="Ngay Sinh")]
        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }

        [MaxLength(60,ErrorMessage ="Toi da 60 ky tu")]
        [Display(Name ="Dia Chi")]
        public string DiaChi { get; set; }

        [MaxLength(24,ErrorMessage ="Toi da 24 ky tu")]
        [RegularExpression(@"0[9875]\d{8}",ErrorMessage = "Chua dung dinh dang di dong Viet Nam")]
        [Display(Name ="Dien Thoai")]
        public string DienThoai { get; set; }

        [EmailAddress(ErrorMessage = "Chu dung dinh dang email")]
        public string Email { get; set; } = null!;

        public string? Hinh { get; set; }

    }
}

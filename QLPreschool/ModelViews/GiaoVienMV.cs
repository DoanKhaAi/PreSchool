using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QLPreschool.ModelViews
{
    public class GiaoVienMV
    {
        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [DisplayName("Tên Giáo Viên")]
        [StringLength(50,MinimumLength = 3,ErrorMessage = "Độ dài {0} tối thiểu {2} đến {1}")]
        public string TenGV {  get; set; }

        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [DisplayName("Chức vụ")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Độ dài {0} tối thiểu {2} đến {1}")]
        public string ChucVu { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không đúng định dạng")]
        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [DisplayName("Số điện thoại")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [DisplayName("Hình đại diện")]
        public IFormFile AvatarGV { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [DisplayName("Email đăng nhập")]
        [EmailAddress(ErrorMessage = "Không đúng định dạng Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập {0}")]
        [DisplayName("Mật khẩu đăng nhập")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Tối thiểu 8 ký tự, ít nhất một in hoa, thường,một số,ký tự đặc biệt")]
        public string MatKhau { get; set; }
    }
}

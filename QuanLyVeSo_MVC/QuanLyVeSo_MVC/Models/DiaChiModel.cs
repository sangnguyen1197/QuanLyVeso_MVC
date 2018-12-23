using System.ComponentModel.DataAnnotations;

namespace QuanLyVeSo_MVC.Models
{
    public class DiaChiModel
    {
        [Required(ErrorMessage = "Yêu cầu nhập tỉnh/tp")]
        [Display(Name = "Tỉnh(TP)")]
        public string TinhTP { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập quận/huyện")]
        [Display(Name = "Quận(Huyện)")]
        public string QuanHuyen { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập phường/xã")]
        [Display(Name = "Phường(Xã)")]
        public string PhuongXa { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập đường/số nhà")]
        [Display(Name = "Đường/Số nhà")]
        public string DuongSoNha { get; set; }
    }
}

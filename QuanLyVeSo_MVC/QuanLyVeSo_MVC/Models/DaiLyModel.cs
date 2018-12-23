using System.ComponentModel.DataAnnotations;

namespace QuanLyVeSo_MVC.Models
{
    public class DaiLyModel
    {
        public string MaDaiLy { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập tên đại lý")]
        [Display(Name = "Tên đại lý")]
        public string TenDaiLy { get; set; }

        public DiaChiModel DiaChi { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập sđt")]
        [Display(Name = "SĐT")]
        public string SDT { get; set; }
    }
}

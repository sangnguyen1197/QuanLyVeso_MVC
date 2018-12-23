namespace QuanLyVeSo_MVC.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SoLuongDK")]
    public partial class SoLuongDK
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Mã số lượng đăng ký")]
        public string MaSoLuongDK { get; set; }

        [StringLength(20)]
        public string MaDaiLy { get; set; }

        [Required(ErrorMessage = "Phải chọn ngày đăng ký")]
        [Display(Name = "Ngày đăng ký")]
        [Column(TypeName = "date")]
        public DateTime? NgayDK { get; set; }

        [Display(Name = "Số lượng đăng ký")]
        [Required(ErrorMessage = "Phải nhập số lượng đăng ký")]
        [Column("SoLuongDK")]
        public int? SoLuongDK1 { get; set; }

        public bool? Flag { get; set; }

        [ForeignKey("MaDaiLy")]
        public virtual DaiLy DaiLy { get; set; }
    }
}

namespace QuanLyVeSo_MVC.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PhieuThu")]
    public partial class PhieuThu
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Mã phiếu thu")]
        public string MaPhieuThu { get; set; }

        [StringLength(20)]
        public string MaDaiLy { get; set; }

        [Display(Name = "Ngày nộp")]
        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Phải nhập ngày nộp")]
        public DateTime? NgayNop { get; set; }

        [Display(Name = "Số tiền nộp")]
        [Required(ErrorMessage = "Phải nhập số tiền nộp")]
        public decimal? SoTienNop { get; set; }

        public bool? Flag { get; set; }

        [ForeignKey("MaDaiLy")]
        public virtual DaiLy DaiLy { get; set; }
    }
}

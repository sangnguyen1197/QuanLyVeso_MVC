namespace QuanLyVeSo_MVC.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CongNo")]
    public partial class CongNo
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Mã công nợ")]
        public string MaCongNo { get; set; }

        [StringLength(20)]
        public string MaDaiLy { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày nợ")]
        [Required(ErrorMessage = "Phải nhập ngày nợ")]
        public DateTime? NgayNo { get; set; }

        [Display(Name = "Số tiền nợ")]
        [Required(ErrorMessage = "Phải nhập số tiền nợ")]
        public decimal? SoTienNo { get; set; }

        public bool? Flag { get; set; }

        [ForeignKey("MaDaiLy")]
        public virtual DaiLy DaiLy { get; set; }
    }
}

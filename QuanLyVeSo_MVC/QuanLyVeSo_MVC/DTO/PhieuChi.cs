namespace QuanLyVeSo_MVC.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuChi")]
    public partial class PhieuChi
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Mã phiếu chi")]
        public string MaPhieuChi { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Ngày chi")]
        public DateTime NgayChi { get; set; }

        [StringLength(200)]
        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Bạn phải nhập nội dung")]
        public string NoiDung { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Số tiền")]
        public decimal? SoTien { get; set; }

        public bool? Flag { get; set; }
    }
}

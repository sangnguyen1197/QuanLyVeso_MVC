﻿namespace QuanLyVeSo_MVC.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhatHanh")]
    public partial class PhatHanh
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Mã đợt phát hành")]
        public string MaPhatHanh { get; set; }

        [StringLength(20)]
        [Display(Name = "Mã đại lý")]
        public string MaDaiLy { get; set; }

        [StringLength(20)]
        [Display(Name = "Mã loại vé số")]
        public string MaLoaiVeSo { get; set; }

        [Display(Name = "Số lượng")]
        public int? SoLuong { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày nhận")]
        public DateTime NgayNhan { get; set; }

        [Display(Name = "Số lượng bán được")]
        public int? SLBan { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Doanh thu")]
        public decimal? DoanhThuDPH { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Hoa hồng")]
        public decimal? HoaHong { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Tiền thanh toán")]
        public decimal? TienThanhToan { get; set; }

        public bool? Flag { get; set; }

        public virtual DaiLy DaiLy { get; set; }

        public virtual LoaiVeso LoaiVeso { get; set; }
    }
}

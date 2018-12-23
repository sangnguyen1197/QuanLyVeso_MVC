namespace QuanLyVeSo_MVC.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DaiLy")]
    public partial class DaiLy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DaiLy()
        {
            CongNo = new HashSet<CongNo>();
            PhatHanh = new HashSet<PhatHanh>();
            PhieuThu = new HashSet<PhieuThu>();
            SoLuongDK = new HashSet<SoLuongDK>();
        }

        [Key]
        [StringLength(20)]
        [Display(Name = "Mã đại lý")]
        public string MaDaiLy { get; set; }

        [StringLength(50, ErrorMessage = "Tên đại lý không dài hơn 50 ký tự")]
        [Display(Name = "Tên đại lý")]
        [Required(ErrorMessage = "Yêu cầu nhập tên đại lý")]
        public string TenDaiLy { get; set; }

        [StringLength(100)]
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Yêu cầu nhập địa chỉ")]
        public string DiaChi { get; set; }

        [StringLength(15)]
        [Display(Name = "SĐT")]
        [MinLength(10, ErrorMessage = "Số điện thoại phải trên 10 số")]
        [Required(ErrorMessage = "Yêu cầu nhập SĐT")]
        public string SDT { get; set; }

        public bool? Flag { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CongNo> CongNo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhatHanh> PhatHanh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuThu> PhieuThu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SoLuongDK> SoLuongDK { get; set; }
    }
}

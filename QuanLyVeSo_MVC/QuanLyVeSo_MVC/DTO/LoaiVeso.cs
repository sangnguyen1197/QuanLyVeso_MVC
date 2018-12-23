namespace QuanLyVeSo_MVC.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiVeso")]
    public partial class LoaiVeso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiVeso()
        {
            KetQuaSoXo = new HashSet<KetQuaSoXo>();
            PhatHanh = new HashSet<PhatHanh>();
        }

        [Key]
        [StringLength(20)]
        [Display(Name = "Mã loại vé số")]
        public string MaLoaiVeSo { get; set; }

        [StringLength(20)]
        [Display(Name = "Tỉnh")]
        public string Tinh { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Giá bán")]
        public decimal? GiaBan { get; set; }

        public bool? Flag { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KetQuaSoXo> KetQuaSoXo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhatHanh> PhatHanh { get; set; }
    }
}

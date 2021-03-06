//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BiShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class LoaiSanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiSanPham()
        {
            this.SanPhams = new HashSet<SanPham>();
            this.TinTucs = new HashSet<TinTuc>();
        }
    
        [Key]
        [Display(Name = "Mã loại sản phẩm")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Bạn hãy nhập tên loại sản phẩm")]
        [Display(Name = "Tên loại")]
        public string TenLoai { get; set; }

        [Required(ErrorMessage = "Bạn hãy nhập miêu tả")]
        [Display(Name = "Miêu tả")]
        public string MieuTa { get; set; }

        [Required(ErrorMessage = "Bạn hãy nhập xuất xứ")]
        [Display(Name = "Xuất xứ")]
        public string XuatXu { get; set; }

        public Nullable<int> MaKM { get; set; }

        [Display(Name = "Ảnh")]
        public string LinkAnh { get; set; }
    
        public virtual KhuyenMai KhuyenMai { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TinTuc> TinTucs { get; set; }
    }
}

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

    public partial class KHACHHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHACHHANG()
        {
            this.DonHangs = new HashSet<DonHang>();
            this.Reviews = new HashSet<Review>();
        }
    
        [Key]
        [Display(Name = "Mã khách hàng")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Bạn hãy nhập tên khách hàng")]
        [Display(Name = "Tên khách hàng")]
        public string TenKH { get; set; }

        [Display(Name = "Ngày sinh")]
        public Nullable<System.DateTime> NgaySinh { get; set; }

        [Required(ErrorMessage = "Bạn hãy nhập địa chỉ")]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Giới tính")]
        public string GioiTinh { get; set; }

        [Required(ErrorMessage = "Bạn hãy nhập email")]
        public string Email { get; set; }

        public string Vip { get; set; }

        [Required(ErrorMessage = "Bạn hãy nhập số điện thoại")]
        public string Phone { get; set; }

        [Display(Name = "Ảnh")]
        public string Photo { get; set; }

        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> NgayTao { get; set; }
    
        public virtual Account Account { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHangs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}

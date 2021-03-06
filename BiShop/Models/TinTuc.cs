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

    public partial class TinTuc
    {
        [Key]
        [Display(Name = "Mã tin tức")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Bạn hãy nhập tiêu đề")]
        [Display(Name = "Tiêu đề")]
        public string TieuDe { get; set; }

        [Display(Name = "Ảnh")]
        public string LinkAnh { get; set; }

        [Required(ErrorMessage = "Bạn hãy nhập nội dung")]
        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }

        [Display(Name = "Ngày tạo")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> NgayTao { get; set; }

        [Required(ErrorMessage = "Bạn hãy nhập người tạo")]
        [Display(Name = "Người tạo")]
        public string NguoiTao { get; set; }

        [Display(Name = "Lượt xem")]
        public Nullable<int> LuotXem { get; set; }

        public Nullable<int> LoaiSP { get; set; }
        public Nullable<int> MaLoaiTin { get; set; }
    
        public virtual LoaiSanPham LoaiSanPham { get; set; }
        public virtual LoaiTin LoaiTin { get; set; }
    }
}

﻿//------------------------------------------------------------------------------
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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class CTDonHang
    {
        [Key]
        [Column(Order = 1)]
        public int MaDH { get; set; }

        [Key]
        [Column(Order = 2)]
        public int MaSP { get; set; }

        [Display(Name = "Số lượng")]
        [DefaultValue("1")]
        public Nullable<int> SoLuong { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }
    
        public virtual DonHang DonHang { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
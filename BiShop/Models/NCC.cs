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
    using System.ComponentModel.DataAnnotations;

    public partial class NCC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NCC()
        {
            this.SanPhams = new HashSet<SanPham>();
        }

        [Key]
        [Display(Name = "Mã nhà cung cấp")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Bạn hãy nhập tên nhà cung cấp")]
        [Display(Name = "Tên nhà cung cấp")]
        public string TenNCC { get; set; }

        [Required(ErrorMessage = "Bạn hãy nhập mô tả")]
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}

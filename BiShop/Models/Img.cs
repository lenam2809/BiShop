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

    public partial class Img
    {
        [Key]
        [Display(Name = "Mã ảnh")]
        public int Id { get; set; }

        [Display(Name = "Ảnh 1")]
        public string Link1 { get; set; }

        [Display(Name = "Ảnh 2")]
        public string Link2 { get; set; }

        [Display(Name = "Ảnh 3")]
        public string Link3 { get; set; }

        [Display(Name = "Ảnh 4")]
        public string Link4 { get; set; }
    }
}

using BiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiShop.ViewModel
{
    public class ReviewModel
    {
        public Review review { get; set; }

        public string PhoToCustomer 
        { 
            get
            {
                return review.KHACHHANG.Photo;
            }
        }

        public string TenCustomer
        {
            get
            {
                return review.KHACHHANG.TenKH;
            }
        }

        public DateTime ThoiGian
        {
            get
            {
                return (DateTime)review.NgayTao;
            }
        }

        public string NoiDung
        {
            get
            {
                return review.NoiDung;
            }
        }
    }
}
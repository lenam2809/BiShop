using BiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiShop.ViewModel
{
    public class SearchProducts
    {
        public List<SanPhamViewModel> sanPhams { get; set; }

        public int Count 
        { 
            get
            {
                return sanPhams.Count;
            }
        }

    }
}
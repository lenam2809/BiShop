using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiShop.Models
{
    public class SearchResult
    {
        public int Id { get; set; }

        public string Link { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }
    }
}
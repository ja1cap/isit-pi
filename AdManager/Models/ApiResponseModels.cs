using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdManager.Models
{
    public class BannerResponse
    {
        public int CPC { get; set; }
        public string Currency { get; set; }
        public int BannerWidth { get; set; }
        public int BannerHeight { get; set; }
        public string BannerHtml { get; set; }
    }
}
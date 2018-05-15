using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdManager.Models
{

    public class ClickRevenueCurrency
    {
        public int Revenue { get; set; }
        public string Currency { get; set; }
    }

    public class Click
    {
        public int ID { get; set; }
        public int ZoneID { get; set; }
        public Zone Zone { get; set; }
        public int CampaignID { get; set; }
        public Campaign Campaign { get; set; }
        public int Revenue { get; set; }
        public string Currency { get; set; }
        public DateTime CreateAt { get; set; }
        public Click()
        {
            Revenue = 0;
            Currency = "BYN";
            CreateAt = DateTime.Now;
        }
    }
}
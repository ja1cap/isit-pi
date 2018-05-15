using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdManager.Models
{
    public class Campaign
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Пользователь")]
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        [Range(1, 99)]
        [Required]
        [Display(Name = "Цена перехода")]
        public int Revenue { get; set; }
        [Range(1, 99999)]
        [Required]
        [Display(Name = "Бюджет")]
        public int Budget { get; set; }
        [Required]
        [Display(Name = "Валюта")]
        public string Currency { get; set; }
        [Required]
        [Url]
        [Display(Name = "Адрес страницы перехода")]
        public string ClickUrl { get; set; }
        [Required]
        [Url]
        [Display(Name = "Адрес изображения")]
        public string BannerImageUrl { get; set; }
        [Required]
        [Display(Name = "Ширина изображения(px)")]
        public int BannerImageWidth { get; set; }
        [Required]
        [Display(Name = "Высота изображения(px)")]
        public int BannerImageHeight { get; set; }
    }
}

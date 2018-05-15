using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdManager.Models
{
    public class Zone
    {
        public int ID { get; set; }
        [Display(Name = "Пользователь")]
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
        [Display(Name = "Веб-сайт")]
        public int WebsiteID { get; set; }
        public Website Website { get; set; }
        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Ширина блока(px)")]
        public int AdPlacementWidth { get; set; }
        [Required]
        [Display(Name = "Высота блока(px)")]
        public int AdPlacementHeight { get; set; }
    }
}

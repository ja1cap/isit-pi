using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdManager.Models
{
    public class Website
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
        public virtual ICollection<Zone> Zones { get; set; }
        [Required]
        [Url]
        [Display(Name = "Адрес")]
        public string Url { get; set; }
        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Контактное лицо")]
        public string ContactName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Контактный e-mail")]
        public string ContactEmail { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristGuide.INF.Models
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public string Url { get; set; }
        public string Descripton { get; set; }
        public int TypeOfPhoto { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}

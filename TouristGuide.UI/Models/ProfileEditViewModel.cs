using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristGuide.UI.Models
{
    public class ProfileEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public double XCoord { get; set; }
        public double YCoord { get; set; }
        public string CompanyId { get; set; }
        public int ProfileType { get; set; }
    }
}
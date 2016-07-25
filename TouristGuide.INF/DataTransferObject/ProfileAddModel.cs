﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristGuide.INF.Models;
using TouristGuide.INF.Enums;

namespace TouristGuide.INF.DataTransferObject
{
    public class ProfileAddModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public double XCoord { get; set; }
        public double YCoord { get; set; }
        public string CompanyId { get; set; }
        public ProfileTypeEnum ProfileType { get; set; }
    }
}

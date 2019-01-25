using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Driftservice_Registration.Models
{
    public class ServiceTypeVM
    {
        public int ServiceTypeID { get; set; }

        public string Description { get; set; }

        public bool? PublicServiceType { get; set; }
    }
}
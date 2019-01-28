using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Driftservice_Registration.Models
{
    public class AdminVM
    {
        public List<Contact> contact { get; set; }
        public List<ServiceType> serviceType { get; set; }
    }
}
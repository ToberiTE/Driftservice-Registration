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
        public virtual ICollection<Contact> contact { get; set; }
        public virtual ICollection<ServiceType> serviceType { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Driftservice_Registration.Models
{
    public class ServiceTypeVM
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public bool IsChecked { get; set; }

    }
}
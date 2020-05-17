namespace Driftservice_Registration.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Script.Serialization;

    public partial class ServiceType
    {
        public ServiceType()
        {
            Contacts = new HashSet<Contact>();
        }

        public int ServiceTypeID { get; set; }

        public string Description { get; set; }

        public bool? PublicServiceType { get; set; }

        [ScriptIgnore]
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}

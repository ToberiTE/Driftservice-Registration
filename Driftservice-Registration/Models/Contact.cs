namespace Driftservice_Registration.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Contact
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contact()
        {
            ContactGuid = Guid.NewGuid();
            ServiceTypes = new HashSet<ServiceType>();
        }

        public int ContactID { get; set; }

        [Required(ErrorMessage = "* Firstname required.")]
        [DisplayName("Firstname *")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* Lastname required.")]
        [DisplayName("Lastname *")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "* Business field must not be empty.")]
        [DisplayName("Business *")]
        public string Business { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [DisplayName("Phone")]
        public string PhoneNumber { get; set; }

        [NotMapped]
        public bool SmsChecked { get; set; }

        [NotMapped]
        public bool EmailChecked{ get; set; }

        [DisplayName("Contact method *")]
        public int NotificationType { get; set; }

        public Guid ContactGuid { get; set; }

        public int? Language { get; set; }

        [Required(ErrorMessage = "* Select a date.")]
        [DisplayName("Date *")]
        public DateTime? RegDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceType> ServiceTypes { get; set; }
    }
}

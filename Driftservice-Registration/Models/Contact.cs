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

        [Required(ErrorMessage = "* F�rnamn kr�vs.")]
        [DisplayName("F�rnamn *")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* Efternamn kr�vs.")]
        [DisplayName("Efternamn *")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "* F�retag kr�vs.")]
        [DisplayName("F�retag *")]
        public string Business { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "En giltig email kr�vs.")]
        [DisplayName("Email *")]
        public string Email { get; set; }

        [Phone]
        [DisplayName("Telefon")]
        public string PhoneNumber { get; set; }

        [DisplayName("Kontaktmetod")]
        public int NotificationType { get; set; }

        [NotMapped]
        public bool SmsChecked { get; set; }

        [NotMapped]
        public bool EmailChecked{ get; set; }

        public Guid ContactGuid { get; set; }

        [DisplayName("Spr�k")]
        public int? Language { get; set; }

        [DisplayName("Datum")]
        public DateTime? RegDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceType> ServiceTypes { get; set; }
    }
}

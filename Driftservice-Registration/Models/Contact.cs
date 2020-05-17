namespace Driftservice_Registration.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Script.Serialization;

    public partial class Contact
    {
        public Contact()
        {
            ServiceTypes = new HashSet<ServiceType>();
        }
        
        public int ContactID { get; set; }

        [DisplayName("F�rnamn *")]
        public string FirstName { get; set; }

        [DisplayName("Efternamn *")]
        public string LastName { get; set; }

        [DisplayName("F�retag *")]
        public string Business { get; set; }

        [DisplayName("Email *")]
        [Required(ErrorMessage = "En riktig email kr�vs.")]
        [EmailAddress(ErrorMessage = "En riktig email kr�vs.")]
        public string Email { get; set; }

        [DisplayName("Mobilnr *")]
        [Required(ErrorMessage = "Ett riktigt mobilnr kr�vs.")]
        [RegularExpression("^([+46]?)(([0-9]{10,11})*$)", ErrorMessage = "Ett riktigt mobilnr kr�vs.")]
        public string PhoneNumber { get; set; }

        [NotMapped]
        public bool EmailChecked { get; set; }

        [NotMapped]
        public bool SmsChecked { get; set; }

        [DisplayName("Kontaktmetod")]
        public int NotificationType { get; set; }

        public Guid ContactGuid { get; set; }

        [DisplayName("Spr�k")]
        public int? Language { get; set; }

        [DisplayName("Datum")]
        public DateTime? RegDate { get; set; }

        [ScriptIgnore]
        public virtual ICollection<ServiceType> ServiceTypes { get; set; }

    }
}

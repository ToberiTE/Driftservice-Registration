using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Driftservice_Registration.Models
{
    public partial class ViewModel
    {
        public ViewModel()
        {
            ContactGuid = Guid.NewGuid();
            ServiceTypes = new HashSet<ServiceType>();
        }

        public int ContactID { get; set; }

        [Required(ErrorMessage = "* Förnamn krävs.")]
        [DisplayName("Förnamn *")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* Efternamn krävs.")]
        [DisplayName("Efternamn *")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "* Företag krävs.")]
        [DisplayName("Företag *")]
        public string Business { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "En giltig email krävs.")]
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
        public bool EmailChecked { get; set; }

        public Guid ContactGuid { get; set; }

        [DisplayName("Språk")]
        public int? Language { get; set; }

        [DisplayName("Datum")]
        public DateTime? RegDate { get; set; }

        public int ServiceTypeID { get; set; }

        public virtual ICollection<ServiceType> ServiceTypes { get; set; }
    }
}
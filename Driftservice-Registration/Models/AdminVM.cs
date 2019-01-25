using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Driftservice_Registration.Models
{
    public class AdminVM
    {
        public AdminVM()
        {
        }

        public int ContactID { get; set; }

        [DisplayName("Förnamn")]
        public string FirstName { get; set; }

        [DisplayName("Efternamn")]
        public string LastName { get; set; }

        [DisplayName("Företag")]
        public string Business { get; set; }

        [EmailAddress]
        [DisplayName("Email")]
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

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [DisplayName("Datum")]
        public DateTime? RegDate { get; set; }

        public int ServiceTypeID { get; set; }

        public string Description { get; set; }

        public bool? PublicServiceType { get; set; }
    }
}
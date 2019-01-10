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

        //public ServiceType MapVMToServiceType()
        //{
        //    return new ServiceType()
        //    {
        //        ServiceTypeID = ServiceTypeID
        //    };
        //}

        //public Contact MapVMToContact()
        //{
        //    return new Contact()
        //    {
        //        ContactID = ContactID,
        //        FirstName = FirstName,
        //        LastName = LastName,
        //        Business = Business,
        //        Email = Email,
        //        PhoneNumber = PhoneNumber,
        //        NotificationType = NotificationType,
        //        ContactGuid = ContactGuid,
        //        Language = Language,
        //        RegDate = RegDate,
        //    };
        //}

        public void MapVMToModel(ref Contact contact, ServiceType serviceType)
        {
            contact.ContactID = ContactID;
            contact.FirstName = FirstName;
            contact.LastName = LastName;
            contact.Business = Business;
            contact.Email = Email;
            contact.PhoneNumber = PhoneNumber;
            contact.NotificationType = NotificationType;
            contact.ContactGuid = ContactGuid;
            contact.Language = Language;
            contact.RegDate = RegDate;
            serviceType.ServiceTypeID = ServiceTypeID;
        }DBNull.save

        public static implicit operator ViewModel(Contact contact)
        {
            return new ViewModel()
            {
                ContactID = contact.ContactID,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Business = contact.Business,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                NotificationType = contact.NotificationType,
                ContactGuid = contact.ContactGuid,
                Language = contact.Language,
                RegDate = contact.RegDate
            };
        }

        public static implicit operator Contact(ViewModel viewModel)
        {
            return new Contact()
            {
                ContactID = viewModel.ContactID,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Business = viewModel.Business,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
                NotificationType = viewModel.NotificationType,
                ContactGuid = viewModel.ContactGuid,
                Language = viewModel.Language,
                RegDate = viewModel.RegDate
            };
        }

    }
}
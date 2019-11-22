using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rent_a_Car.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Onthoud deze browser.")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [Display(Name = "Onthoud dit account op deze computer.")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Het {0} moet ten minste {2} karakters lang zijn.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestigings wachtwoord")]
        [Compare("Password", ErrorMessage = "Het wachtwoord en bevestigings wachtwoord komen niet overeen.")]
        public string ConfirmPassword { get; set; }


        //persoonlijke data
        [Required]
        [Display(Name = "Voornaam")]
        public string Voornaam { get; set; }

        [Required]
        [Display(Name = "Tussenvoegsel")]
        public string Tussenvoegsel { get; set; }

        [Required]
        [Display(Name = "Achternaam")]
        public string Achternaam { get; set; }

        [Required]
        [Display(Name = "Geboortedatum")]
        [DataType(DataType.Date)]
        public DateTime Geboortedatum { get; set; }

        [Display(Name = "Telefoonnummer")]
        [Phone]
        public string PhoneNumber { get; set; }


        //Adres
        [Required]
        [Display(Name = "Straat")]
        public string Straat { get; set; }

        [Required]
        [Display(Name = "Huis nummer")]
        public int Huisnummer { get; set; }

        [Required]
        [Display(Name = "Toevoeging")]
        public string Toevoeging { get; set; }

        [Required]
        [Display(Name = "Postcode")]
        public string PostCode { get; set; }

        [Required]
        [Display(Name = "Plaats")]
        public string Plaats { get; set; }

        [Required]
        [Display(Name = "Provincie")]
        public string Provincie { get; set; }

        [Required]
        [Display(Name = "Land")]
        public string Land { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Het {0} moet ten minste {2} karakters lang zijn.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestigings wachtwoord")]
        [Compare("Password", ErrorMessage = "Het wachtwoord en bevestigings wachtwoord komen niet overeen.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

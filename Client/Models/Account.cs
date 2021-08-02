using System;
using System.ComponentModel.DataAnnotations;
namespace ClothingShop.Client.Models
{
    class Account
    {
        [Required]
        public string Name {get;set;}
        [Required]
        public string Adress {get; set;}
        [Required, EmailAddress]
        public string Email {get; set;}
        [Required, MinLength(7, ErrorMessage="PhoneNumber must be from 7 to 11 digits"), MaxLength(11, ErrorMessage="PhoneNumber must be from 7 to 11 digits")]
        public string PhoneNumber {get; set;}
    }
}
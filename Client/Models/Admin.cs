using System.ComponentModel.DataAnnotations;
namespace ClothingShop.Client.Models
{
    public class Admin
    {
        [RegularExpression("admin",ErrorMessage ="Username not found")]
        public string Username{get;set;}
        [RegularExpression("admin",ErrorMessage ="Password is not correct")]
        public string Password{get;set;}
    }
}
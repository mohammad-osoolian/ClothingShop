using System;
using System.IO;

namespace ClothingShop.Shared.Models
{
    public abstract class Product
    {
        public int Id{get;set;}
        public string Name{get;set;}
        public int Price{get;set;}
        public string Size{get;set;}
        public string Image{get;set;}
        public int Number{get;set;}
        public void SetValues(Product product)
        {
            if (this.Id != product.Id)
                throw new InvalidDataException("id doesn't match");
            this.Name = product.Name;
            this.Price = product.Price;
            if(product.Image == string.Empty)
                this.Image = "Images/NoImage.png";
            else
                this.Image = product.Image;
            this.Size = product.Size;
            this.Number = product.Number;
        }
        public override string ToString()
        {
            return $"{Id} - {Name} - {Price} - {Size} - {Image} - {Number}";
        }
    }
}
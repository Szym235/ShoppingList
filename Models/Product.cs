using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace ShoppingList.Models
{
    public partial class Product : ObservableObject
    {
        public static Product getProductFromXmlNode(XmlNode node)
        {
            Boolean isBought = Boolean.Parse(getAttributeIfExists(node, "IsBought", "false"));
            string name = getAttributeIfExists(node, "Name", "Name");
            int quantity = int.Parse(getAttributeIfExists(node, "Quantity", "1"));
            string unit = getAttributeIfExists(node, "Unit", "units");
            string shop = getAttributeIfExists(node, "Shop", "No shop specified");
            Boolean isOptional = Boolean.Parse(getAttributeIfExists(node, "IsOptional", "false"));
            Product newProduct = new Product(isBought, name, quantity, unit, shop, isOptional);
            return newProduct;
        }

        public static XmlElement getXmlElementFromProduct(Product product, XmlDocument document)
        {
            XmlElement newProduct = document.CreateElement("Product");
            newProduct.Attributes.Append(document.CreateAttribute("IsBought")).Value = product.IsBought.ToString();
            newProduct.Attributes.Append(document.CreateAttribute("Name")).Value = product.Name;
            newProduct.Attributes.Append(document.CreateAttribute("Quantity")).Value = product.Quantity.ToString();
            newProduct.Attributes.Append(document.CreateAttribute("Unit")).Value = product.Unit;
            newProduct.Attributes.Append(document.CreateAttribute("Shop")).Value = product.Shop;
            newProduct.Attributes.Append(document.CreateAttribute("IsOptional")).Value = product.IsOptional.ToString();
            return newProduct;
        }
        public static string getAttributeIfExists(XmlNode node, String name, String defaultValue)
        {
            if (node.Attributes[name] != null) return node.Attributes[name].Value;
            else return defaultValue;
        }

        [ObservableProperty]
        private Boolean isBought;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private double quantity;
        [ObservableProperty]
        private string unit;
        [ObservableProperty]
        private string shop;
        [ObservableProperty]
        private Boolean isOptional;

        public Product(Boolean isBought, string name, double quantity, string unit, string shop, Boolean isOptional)
        {
            this.isBought = isBought;
            this.name = name;
            this.quantity = quantity;
            this.unit = unit;
            this.shop = shop;
            this.isOptional = isOptional;
        }

        [RelayCommand]
        private void IncrementQuantity()
        {
            Quantity++;
        }

        [RelayCommand]
        private void DecrementQuantity()
        {
            double oldQuantity = Quantity;
            Quantity--;
            if(Quantity < 0) Quantity = oldQuantity;
        }
    }
}

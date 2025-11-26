using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingList.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Xml;


namespace ShoppingList.ViewModels
{
    public partial class AllProducts : ObservableObject
    {
        public static AllProducts Instance { get; } = new AllProducts();
        string saveFilePath;

        [ObservableProperty]
        private string newName;
        [ObservableProperty]
        private string newUnit;
        [ObservableProperty]
        private string newQuantity;

        [ObservableProperty]
        private ObservableCollection<Product> products = new();
        AllProducts()
        {
            saveFilePath = Path.Combine(FileSystem.AppDataDirectory, "ProductsSaveFile.xml");
            LoadProducts();
            Debug.WriteLine(saveFilePath);
        }

        [RelayCommand]
        private void AddProduct()
        {
            double quantityDouble = double.Parse(NewQuantity);
            Products.Add(new Product(false, NewName, quantityDouble, NewUnit));
        }

        [RelayCommand]
        private void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }

        [RelayCommand]
        private void SaveProducts()
        {
            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement("ShoppingList");
            document.AppendChild(root);
            foreach (Product product in products)
            {
                XmlElement newProduct = document.CreateElement("Product");
                newProduct.Attributes.Append(document.CreateAttribute("Name")).Value = product.Name;
                newProduct.Attributes.Append(document.CreateAttribute("Quantity")).Value = product.Quantity.ToString();
                newProduct.Attributes.Append(document.CreateAttribute("Unit")).Value = product.Unit.ToString();
                newProduct.Attributes.Append(document.CreateAttribute("IsBought")).Value = product.IsBought.ToString();
                root.AppendChild(newProduct);
            }
            document.Save(saveFilePath);
            Debug.WriteLine(saveFilePath);
        }
        private void LoadProducts()
        {
            XmlDocument document = new XmlDocument();

            try
            {
                document.Load(saveFilePath);
                XmlElement root = document.DocumentElement;
                foreach (XmlNode node in root.ChildNodes)
                {
                    Boolean isBought;
                    Boolean.TryParse(getAttributeIfExists(node, "IsBought", "false"), out isBought);
                    string name = getAttributeIfExists(node, "Name", "Name");
                    int quantity = int.Parse(getAttributeIfExists(node, "Quantity", "1"));
                    string unit = getAttributeIfExists(node, "Unit", "units");
                    Products.Add(new Product(isBought, name, quantity, unit));
                }
            }
            catch
            {
                Debug.WriteLine("Error while loading");
            }
}

        private string getAttributeIfExists(XmlNode node, String name, String defaultValue)
        {
            if (node.Attributes[name] != null) return node.Attributes[name].Value;
            else return defaultValue;
        }
    }
}

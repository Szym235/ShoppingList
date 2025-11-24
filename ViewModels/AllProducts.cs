using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingList.Models;
using System.Collections.ObjectModel;


namespace ShoppingList.ViewModels
{
    public partial class AllProducts : ObservableObject
    {
        public static AllProducts Instance { get; } = new AllProducts();

        [ObservableProperty]
        private string newName;
        [ObservableProperty]
        private string newUnit;
        [ObservableProperty]
        private string newQuantity;

        [ObservableProperty]
        private ObservableCollection<Product> products = new();

        [RelayCommand]
        private void AddProduct()
        {
            double quantity = double.Parse(NewQuantity);
            Products.Add(new Product(NewName, NewUnit, false, quantity));
        }

        [RelayCommand]
        private void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }
    }
}

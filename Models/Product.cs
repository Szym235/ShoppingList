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

namespace ShoppingList.Models
{
    public partial class Product : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string unit;

        [ObservableProperty]
        private Boolean isBought;

        [ObservableProperty]
        private double quantity;

        public Product(string name, string unit, Boolean isBought, double quantity)
        {
            this.name = name;
            this.unit = unit;
            this.isBought = isBought;
            this.quantity = quantity;
        }

        [RelayCommand]
        private void IncrementQuantity()
        {
            Quantity++;
            Debug.WriteLine("Incremented " + Quantity);
        }

        [RelayCommand]
        private void DecrementQuantity()
        {
            Quantity--;
            Debug.WriteLine("Decremented" + Quantity);
        }
    }
}

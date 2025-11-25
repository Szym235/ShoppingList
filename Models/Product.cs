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
        private Boolean isBought;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private double quantity;

        [ObservableProperty]
        private string unit;





        public Product(Boolean isBought, string name, double quantity, string unit )
        {
            this.isBought = isBought;
            this.name = name;
            this.quantity = quantity;
            this.unit = unit;
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

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Models
{
    internal partial class Product : ObservableObject
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
    }
}

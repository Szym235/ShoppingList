using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingList.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Models
{
    public partial class Recipe : ObservableObject
    {
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private ObservableCollection<Category> categories = new();

        public Recipe(string name)
        {
            this.name = name;
        }

        public Recipe(string name, ObservableCollection<Category> categories)
        {
            this.name = name;
            this.categories = categories;
        }

        [RelayCommand]
        private void AddRecipeToList()
        {
            AllProducts.Instance.AddRecipe(this);
        }
    }
}

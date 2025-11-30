using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.ViewModels
{
    partial class RecipeCreator : ObservableObject
    {
        public static RecipeCreator Instance { get; } = new RecipeCreator();

        [ObservableProperty]
        private Recipe recipeForCreation;

        RecipeCreator()
        {
            RecipeForCreation = new Recipe("New Recipe", new ObservableCollection<Category>());
        }

        [RelayCommand]
        private void AddProduct()
        {
            string firstCategoryName = AllProducts.Instance.Categories[0].Name;
            if (!Category.CheckIfCategoryExistsInCollection(RecipeForCreation.Categories, firstCategoryName))
            {
                RecipeForCreation.Categories.Add(new Category(firstCategoryName));
            }
            Category.FindCategoryByNameInCollection(RecipeForCreation.Categories, firstCategoryName).Products.Add(new Product(false, "", 1, "", "No shop specified", false));
        }
    }
}

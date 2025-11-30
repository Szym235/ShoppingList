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
        [ObservableProperty]
        private Category newCategoryFromPicker;
        [ObservableProperty]
        private string newShopFromPicker;
        [ObservableProperty]
        private ObservableCollection<Category> categories;
        [ObservableProperty]
        private ObservableCollection<string> shops;
        RecipeCreator()
        {
            RecipeForCreation = new Recipe("New Recipe", new ObservableCollection<Category>());
            Categories = AllProducts.Instance.Categories;
            Shops = AllProducts.Instance.Shops;
        }

        [RelayCommand]
        private void AddProduct()
        {
            string firstCategoryName = NewCategoryFromPicker.Name;
            if (!Category.CheckIfCategoryExistsInCollection(RecipeForCreation.Categories, firstCategoryName))
            {
                RecipeForCreation.Categories.Add(new Category(firstCategoryName));
            }
            Category.FindCategoryByNameInCollection(RecipeForCreation.Categories, firstCategoryName).Products.Add(new Product(false, "", 0, "", NewShopFromPicker, false));
        }


        [RelayCommand]
        private void RemoveProduct(Product productForRemoval)
        {
            foreach (Category category in recipeForCreation.Categories)
            {
                foreach (Product product in category.Products)
                {
                    if (product == productForRemoval)
                    {
                        category.Products.Remove(product);
                        break;
                    }
                }
            }
        }

        [RelayCommand]
        private void AddRecipe()
        {
            AllRecipes.Instance.Recipes.Add(RecipeForCreation);
            RecipeForCreation = new Recipe("New Recipe", new ObservableCollection<Category>());
        }
    }
}

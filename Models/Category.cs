using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Xml;

namespace ShoppingList.Models
{
    public partial class Category : ObservableObject
    {
        public static Boolean CheckIfCategoryExistsInCollection(ObservableCollection<Category> categories, string categoryName)
        {
            foreach (Category category in categories)
            {
                if (category.Name == categoryName)
                {
                    return true;
                }
            }
            return false;
        }

        public static Category FindCategoryByNameInCollection(ObservableCollection<Category> categories, string categoryName)
        {
            foreach (Category category in categories)
            {
                if (category.Name == categoryName)
                {
                    return category;
                }
            }
            return null;
        }

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private ObservableCollection<Product> products;

        [ObservableProperty]
        private Boolean isVisible;

        public Category(string name) 
        {
            this.name = name;
            products = new ObservableCollection<Product>();
            isVisible = false;
        }

        public Category(string name, ObservableCollection<Product> products)
        {
            this.name = name;
            this.products = products;
            isVisible = false;
        }
    }
}

using ShoppingList.Models;
using System.Diagnostics;
using ShoppingList.ViewModels;

namespace ShoppingList.Views
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = AllProducts.Instance;
        }

    }

}

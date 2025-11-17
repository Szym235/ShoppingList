using ShoppingList.Models;

namespace ShoppingList.Views
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new AllProducts();
        }
    }

}

using ShoppingList.Models;
using ShoppingList.ViewModels;

namespace ShoppingList.Views;

public partial class AddProductPage : ContentPage
{
	public AddProductPage()
	{
		InitializeComponent();
        BindingContext = AllProducts.Instance;
    }
}
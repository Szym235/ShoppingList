using ShoppingList.ViewModels;

namespace ShoppingList.Views;

public partial class ListForShopPage : ContentPage
{
	public ListForShopPage()
	{
		InitializeComponent();
        BindingContext = AllProducts.Instance;
    }
}
using ShoppingList.ViewModels;

namespace ShoppingList.Views;

public partial class recipesPage : ContentPage
{
	public recipesPage()
	{
		InitializeComponent();
		BindingContext = AllRecipes.Instance;
	}
}
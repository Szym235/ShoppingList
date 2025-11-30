using ShoppingList.ViewModels;

namespace ShoppingList.Views;

public partial class RecipesPage : ContentPage
{
	public RecipesPage()
	{
		InitializeComponent();
		BindingContext = AllRecipes.Instance;
	}
}
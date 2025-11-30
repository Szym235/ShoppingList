using ShoppingList.ViewModels;

namespace ShoppingList.Views;

public partial class RecipeCreatorPage : ContentPage
{
	public RecipeCreatorPage()
	{
		InitializeComponent();
		BindingContext = RecipeCreator.Instance;
    }
}
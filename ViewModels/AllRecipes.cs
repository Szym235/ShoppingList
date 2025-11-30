using CommunityToolkit.Mvvm.ComponentModel;
using ShoppingList.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Xml;

namespace ShoppingList.ViewModels
{
    public partial class AllRecipes : ObservableObject
    {
        public static AllRecipes Instance { get; } = new AllRecipes();
        string recipesSaveFilePath;

        [ObservableProperty]
        private ObservableCollection<Recipe> recipes = new();
        private AllRecipes()
        {
            recipesSaveFilePath = Path.Combine(FileSystem.AppDataDirectory, "recipesSaveFile.xml");
            Recipes.CollectionChanged += Recipes_CollectionChanged;
            LoadRecipes();
        }
        private void Recipes_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            SaveRecipes();
        }

        private void LoadRecipes()
        {
            if (!File.Exists(recipesSaveFilePath))
            {
                Recipes.Add(new Recipe("Chocolate cake", new ObservableCollection<Category>(){
                     new Category("Flour", new ObservableCollection<Product>{
                        new Product(false, "Cake flour", 300, "g", "No shop specified", false)}),
                     new Category("Sweets", new ObservableCollection<Product>{
                        new Product(false, "Chocolate", 30, "g", "No shop specified", true)}),
                }));
                Recipes.Add(new Recipe("Biedronka's Omlette", new ObservableCollection<Category>(){
                     new Category("Dairy", new ObservableCollection<Product>{
                        new Product(false, "Egg", 3, "item", "Biedronka", false)}),
                     new Category("Sweets", new ObservableCollection<Product>{
                        new Product(false, "Sugar", 100, "g", "Biedronka", true)}),
                }));
                SaveRecipes();
            }
            else
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(recipesSaveFilePath);
                    XmlElement root = document.DocumentElement;
                    foreach (XmlNode recipeNode in root.ChildNodes)
                    {
                        Recipe newRecipe = new Recipe(Product.getAttributeIfExists(recipeNode, "Name", "Recipe").ToString());
                        foreach (XmlNode categoryNode in recipeNode.ChildNodes)
                        {
                            string newCategoryName = Product.getAttributeIfExists(categoryNode, "Name", "Error with category name");
                            if (!Category.CheckIfCategoryExistsInCollection(newRecipe.Categories, newCategoryName))
                            {
                                newRecipe.Categories.Add(new Category(newCategoryName));
                                newRecipe.Categories.Last().IsVisible = true;
                            }
                            foreach (XmlNode node in categoryNode.ChildNodes)
                            {
                                Product newProduct = Product.getProductFromXmlNode(node);
                                Category.FindCategoryByNameInCollection(newRecipe.Categories, newCategoryName).Products.Add(newProduct);
                            }
                        }
                        Recipes.Add(newRecipe);
                    }
                }
                catch
                {
                    Debug.WriteLine("Error while loading");
                }
            }
        }

        private void SaveRecipes()
        {
            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement("Recipes");
            document.AppendChild(root);
            foreach (Recipe recipe in Recipes)
            {
                XmlElement newRecipe = document.CreateElement("Recipe");
                newRecipe.Attributes.Append(document.CreateAttribute("Name")).Value = recipe.Name;
                foreach (Category category in recipe.Categories)
                {
                    XmlElement newCategory = document.CreateElement("Category");
                    newCategory.Attributes.Append(document.CreateAttribute("Name")).Value = category.Name;
                    foreach (Product product in category.Products)
                    {
                        newCategory.AppendChild(Product.getXmlElementFromProduct(product, document));
                    }
                    newRecipe.AppendChild(newCategory);
                }
            root.AppendChild(newRecipe);
            }
            document.Save(recipesSaveFilePath);
        }
    }
}

using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingList.Models;
using ShoppingList.Views;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml;


namespace ShoppingList.ViewModels
{
    public partial class AllProducts : ObservableObject
    {
        public static AllProducts Instance { get; } = new AllProducts();
        string productsSaveFilePath;

        [ObservableProperty]
        private string newNameFromEntry;
        [ObservableProperty]
        private string newUnitFromEntry;
        [ObservableProperty]
        private string newQuantityFromEntry;
        [ObservableProperty]
        private Category newCategoryFromPicker;
        [ObservableProperty]
        private string newCategoryFromEntry;
        [ObservableProperty]
        private string newShopFromPicker;
        [ObservableProperty]
        private string filteredShopFromPicker;
        [ObservableProperty]
        private Boolean newIsOptionalFromCheckBox;
        [ObservableProperty]
        private ObservableCollection<String> sortOptions = new ObservableCollection<string>()
        {
            "Category", "Name", "Quantity",
        };
        [ObservableProperty]
        private string sortByFromPicker;

        [ObservableProperty]
        private ObservableCollection<Product> products = new();
        [ObservableProperty]
        private ObservableCollection<Category> categories = new ObservableCollection<Category>();
        [ObservableProperty]
        private ObservableCollection<string> shops = new ObservableCollection<string>() 
        { 
            "No shop specified", "Aldi", "Biedronka", "Carrefour", "Leroy Merlin", "Lidl", "Żabka" 
        };
        AllProducts()
        {
            productsSaveFilePath = Path.Combine(FileSystem.AppDataDirectory, "ProductsSaveFile.xml");
            LoadProducts(productsSaveFilePath);
            NewShopFromPicker = Shops[0];
            FilteredShopFromPicker = Shops[0];
            SortByFromPicker = SortOptions[0];
        }

        private void Products_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateProducts();
            SaveProducts();
        }
        private void Product_Changed(object? sender, PropertyChangedEventArgs e)
        {
            SaveProducts();
        }
        partial void OnFilteredShopFromPickerChanged(string? oldValue, string newValue)
        {
            UpdateProducts();
        }
        partial void OnSortByFromPickerChanged(string? oldValue, string newValue)
        {
            UpdateProducts();
        }

        [RelayCommand]
        private void AddProduct()
        {
            double quantityDouble;
            if (!double.TryParse(NewQuantityFromEntry, out quantityDouble) || quantityDouble <= 0)
            {
                App.Current.MainPage.DisplayAlert("Wrong value", "Quantity must be a number bigger than 0", "Ok");
                return;
            }
            else if (NewCategoryFromPicker == null || NewNameFromEntry == string.Empty || quantityDouble == 0 || NewUnitFromEntry == string.Empty)
            {
                App.Current.MainPage.DisplayAlert("Wrong value", "Every product field must be specified", "Ok");
                return;
            }
            Product newProduct = new Product(false, NewNameFromEntry, quantityDouble, NewUnitFromEntry, newShopFromPicker, newIsOptionalFromCheckBox);
            newProduct.PropertyChanged += Product_Changed;
            NewCategoryFromPicker.Products.Add(newProduct);
            NewNameFromEntry = string.Empty;
            NewQuantityFromEntry = string.Empty;
            NewUnitFromEntry = string.Empty;
        }


        [RelayCommand]
        private void AddCategory()
        {
            if(NewCategoryFromEntry == string.Empty)
            {
                App.Current.MainPage.DisplayAlert("Wrong value", "Category name cannot be empty", "Ok");
                return;
            }
            Categories.Add(new Category(NewCategoryFromEntry));
            Categories.Last().Products.CollectionChanged += Products_CollectionChanged;
            NewCategoryFromEntry = string.Empty;
            SortCategories();
        }

        private void SortCategories()
        {
            Categories = new ObservableCollection<Category>(Categories.OrderBy(category => category.Name));
        }

        [RelayCommand]
        private void RemoveProduct(Product productForRemoval)
        {
            foreach (Category category in Categories)
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

        public void AddRecipe(Recipe recipe)
        {
            foreach (Category recipeCategory in recipe.Categories)
            {
                Category destinedCategory;
                Boolean isItNewCategory = false;
                if (!Category.CheckIfCategoryExistsInCollection(Categories, recipeCategory.Name))
                {
                    Categories.Add(new Category(recipeCategory.Name));
                    isItNewCategory = true;
                }
                destinedCategory = Category.FindCategoryByNameInCollection(Categories, recipeCategory.Name);
                foreach (Product recipeProduct in recipeCategory.Products)
                {
                    Product newProduct = new Product(false, recipeProduct.Name, recipeProduct.Quantity, recipeProduct.Unit, recipeProduct.Shop, recipeProduct.IsOptional);
                    newProduct.PropertyChanged += Product_Changed;
                    destinedCategory.Products.Add(newProduct);
                }
                if (isItNewCategory) destinedCategory.Products.CollectionChanged += Products_CollectionChanged;
            }
            SaveProducts();
        }

        private XmlDocument CreateSaveFile()
        {
            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement("ShoppingList");
            document.AppendChild(root);
            foreach (Category category in Categories)
            {
                XmlElement newCategory = document.CreateElement("Category");
                newCategory.Attributes.Append(document.CreateAttribute("Name")).Value = category.Name;
                foreach (Product product in category.Products)
                {
                    newCategory.AppendChild(Product.getXmlElementFromProduct(product, document));
                }
                root.AppendChild(newCategory);
            }
            Debug.WriteLine(productsSaveFilePath);
            return document;
        }

        [RelayCommand]
        private void SaveProducts()
        {
            XmlDocument document = CreateSaveFile();
            document.Save(productsSaveFilePath);
        }

        private async void UpdateProducts()
        {
            Products.Clear();
            foreach (Category category in categories)
            {
                foreach (Product product in category.Products)
                {
                    if (FilteredShopFromPicker == "No shop specified" || product.Shop == FilteredShopFromPicker) Products.Add(product);
                }
            }
            switch (SortByFromPicker)
            {
                case "Name":
                    Products = new ObservableCollection<Product>(Products.OrderBy(product => product.Name));
                    break;
                case "Quantity":
                    Products = new ObservableCollection<Product>(Products.OrderBy(product => product.Quantity));
                    break;
                default:
                    break;
            }
        }

        private void LoadProducts(string path)
        {
            XmlDocument document = new XmlDocument();
            if (!File.Exists(path))
            {
                Categories.Add(new Category("Miscellanous"));
                Categories.Add(new Category("Dairy"));
                Categories.Add(new Category("Meat"));
                SaveProducts();
            }
            else
            {
                try
                {
                    document.Load(path);
                    XmlElement root = document.DocumentElement;
                    foreach (XmlNode category in root.ChildNodes)
                    {
                        string newCategoryName = Product.getAttributeIfExists(category, "Name", "Error with category name");
                        if (!Category.CheckIfCategoryExistsInCollection(Categories, newCategoryName))
                        {
                            Categories.Add(new Category(newCategoryName));
                        }
                        foreach (XmlNode node in category.ChildNodes)
                        {
                            Debug.WriteLine("Getting product from node...");
                            Product newProduct = Product.getProductFromXmlNode(node);
                            Debug.WriteLine("Got product from node. " + newProduct.Name + " Getting destined category...");
                            Category destinedCategory = Category.FindCategoryByNameInCollection(Categories, newCategoryName);
                            Debug.WriteLine("Got destined category. " + destinedCategory.Name + " Adding product to category...");
                            destinedCategory.Products.Add(newProduct);
                            Debug.WriteLine(destinedCategory.Products.Count);
                        }
                    }
                    SortCategories();
                    foreach (Category category in Categories)
                    {
                        category.Products.CollectionChanged += Products_CollectionChanged;
                        foreach (Product product in category.Products)
                        {
                            product.PropertyChanged += Product_Changed;
                        }
                    }
                }
                catch
                {
                    Debug.WriteLine("Error while loading");
                }
            }
        }

        [RelayCommand]
        private async Task ExportProducts()
        {
            FolderPickerResult folder = await FolderPicker.Default.PickAsync();;
            if (folder.IsSuccessful)
            {
                XmlDocument document = CreateSaveFile();
                Debug.WriteLine(Path.Combine(folder.Folder.Path, "exportedList.xml"));
                document.Save(Path.Combine(folder.Folder.Path, "exportedList.xml"));
            }
        }

        [RelayCommand]
        private async Task ImportProducts()
        {
            FileResult file = await FilePicker.Default.PickAsync();
            if (file != null)
            {
                LoadProducts(file.FullPath);
            }
            SaveProducts();
        }
    }
}

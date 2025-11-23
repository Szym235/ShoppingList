namespace ShoppingList.ViewsPartials;

public partial class ProductView : ContentView
{
    public static readonly BindableProperty NameProperty = BindableProperty.Create(nameof(Name), typeof(string), typeof(ProductView), string.Empty);
    public string Name
    {
        get => (string)GetValue(NameProperty);
        set => SetValue(NameProperty, value);
    }

    public static readonly BindableProperty UnitProperty = BindableProperty.Create(nameof(Unit), typeof(string), typeof(ProductView), string.Empty);
    public string Unit
    {
        get => (string)GetValue(UnitProperty);
        set => SetValue(UnitProperty, value);
    }

    public static readonly BindableProperty IsBoughtProperty = BindableProperty.Create(nameof(IsBought), typeof(bool), typeof(ProductView), false);
    public bool IsBought
    {
        get => (bool)GetValue(IsBoughtProperty);
        set {
            SetValue(IsBoughtProperty, value);
            if (value)
            {
                BackgroundColor = Color.FromRgb(0, 0, 0);
                TextColor = Color.FromRgb(255, 255, 255);
            }
            else
            {
                BackgroundColor = Color.FromRgb(255, 255, 255);
                TextColor = Color.FromRgb(0, 0, 0);
            }
        }
    }

    public static readonly BindableProperty QuantityProperty = BindableProperty.Create(nameof(Quantity), typeof(double), typeof(ProductView), 0.0);
    public double Quantity
    {
        get => (double)GetValue(QuantityProperty);
        set => SetValue(QuantityProperty, value);
    }

    public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(ProductView), Color.FromRgb(255, 255, 255));
    public Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(ProductView), Color.FromRgb(0, 0, 0));
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public ProductView()
    {
        Name = "name";
        InitializeComponent();
    }
}
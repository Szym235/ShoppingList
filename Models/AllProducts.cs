using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Models
{
    class AllProducts
    {
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>()
        {
            new Product("Milk", "litres", false, 0)
        };
    }
}

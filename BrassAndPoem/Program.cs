using System.Globalization;
using System.Threading.Tasks.Dataflow;
using Microsoft.Win32.SafeHandles;

List<Product> products = new List<Product>()
{
    new Product()
    {
        Name = "Loads of Odes",
        Price = 30.00M,
        ProductTypeId = 0
    },
    new Product()
    {
        Name = "Bonnetfull of Sonnets",
        Price = 35.99M,
        ProductTypeId = 0
    },
    new Product()
    {
        Name = "Decidedly Un-French Horn",
        Price = 399.99M,
        ProductTypeId = 1
    },
    new Product()
    {
        Name = "Concealed Flute Sword",
        Price = 500.00M,
        ProductTypeId = 1
    },
    new Product()
    {
        Name = "In-A-Pickle Piccolo",
        Price = 250.00M,
        ProductTypeId = 1
    }
};

List<ProductType> productTypes = new List<ProductType>()
{
    new ProductType()
    {
        Title = "Books of Poetry",
        Id = 0
    },
    new ProductType()
    {
        Title = "Totally Normal Musical Instruments",
        Id = 1
    }
};


string greeting = @"Hello and welcome to Brass and Poem

We Are A Specific Shop For Specific People";
string choice = null;
while (choice != "0")
{
    Console.WriteLine(@$"
 ______   _______  _______  _______  _______      __      _______  _______  _______  _______ 
(  ___ \ (  ____ )(  ___  )(  ____ \(  ____ \    /__\    (  ____ )(  ___  )(  ____ \(       )
| (   ) )| (    )|| (   ) || (    \/| (    \/   ( \/ )   | (    )|| (   ) || (    \/| () () |
| (__/ / | (____)|| (___) || (_____ | (_____     \  /    | (____)|| |   | || (__    | || || |
|  __ (  |     __)|  ___  |(_____  )(_____  )    /  \/\  |  _____)| |   | ||  __)   | |(_)| |
| (  \ \ | (\ (   | (   ) |      ) |      ) |   / /\  /  | (      | |   | || (      | |   | |
| )___) )| ) \ \__| )   ( |/\____) |/\____) |  (  \/  \  | )      | (___) || (____/\| )   ( |
|/ \___/ |/   \__/|/     \|\_______)\_______)   \___/\/  |/       (_______)(_______/|/     \|
                                                                                             
{greeting}
");
    DisplayMenu();

    choice = Console.ReadLine().Trim();

    switch (choice)
    {
        case "1":

            DisplayAllProducts(products, productTypes);
            break;
        case "2":
            AddProduct(products, productTypes);
            break;
        case "3":
            UpdateProduct(products, productTypes);
            break;
        case "4":
            DeleteProduct(products, productTypes);
            break;
        case "5":

            Console.WriteLine(@"May the wind bless and carry you...
Far From Here!
        
Press Any Key To Return To Your Realm");
            Console.ReadKey();

            return;

    }

} //end of Main Menu While Loop


//put your greeting here

//implement your loop here

void DisplayMenu()
{
    Console.WriteLine(@"Choose An Option:
1. Display All Products
2. Add Product
3. Update Product
4. Delete Product
5. Exit");
}

void DisplayAllProducts(List<Product> products, List<ProductType> productTypes)
{
    //iterate products with name and price
    for (int i = 0; i < products.Count; i++)
    {
        List<ProductType> itemType = productTypes.Where(p => products[i].ProductTypeId == p.Id).ToList();
        //start line with index
        //add product type title to display using Linq
        string productTypeTitle = itemType.Count > 0 ? itemType[0].Title : "Items";
        Console.WriteLine(@$"{i + 1}. {products[i].Name} - ${products[i].Price} in {itemType[0].Title}");
    }
    Console.WriteLine("Press Any Key To Return To Main Menu");
    Console.ReadKey();
    return;


}

void DeleteProduct(List<Product> products, List<ProductType> productTypes)
{
    string userInput = null;

    Console.WriteLine("Here are the items able to be deleted:");
    for (int i = 0; i < products.Count; i++)
    {
        Console.WriteLine(@$"{i + 1}. {products[i].Name} - {products[i].Price}");
    }
    Console.WriteLine("Please tell me the number of the item to delete");
    while (true)
    {
        userInput = Console.ReadLine().Trim();
        if (int.TryParse(userInput, out int unindexedItemToDelete))
        {
            products.RemoveAt(unindexedItemToDelete - 1);
            break;
        }
    }
    Console.WriteLine(@"Your item has been deleted
    Hit the slay button (or any button really) to return to main menu");
    Console.ReadKey();
    return;
}

void AddProduct(List<Product> products, List<ProductType> productTypes)
{
    string ProductName = null;
    decimal ProductPrice = 0.0M;
    int ProductCat;
    string userInput = null;
    //prompt user for name and price
    Console.WriteLine(@"You Wish To Add An Item
Tell Me It's Name
");
    while (true)
    {
        userInput = Console.ReadLine().Trim();
        //needed the if to keep the method running
        if (!string.IsNullOrEmpty(userInput))
        {
            ProductName = userInput;
            break;
        }



    }
    Console.WriteLine("That was nice. Now tell me its price");
    while (true)
    {
        userInput = Console.ReadLine().Trim();

        if (decimal.TryParse(userInput, out _))
        {
            ProductPrice = decimal.Parse(userInput);
            break;
        }

    }
    Console.WriteLine("Choose a category:");
    // display item types
    for (int i = 0; i < productTypes.Count; i++)
    {
        Console.WriteLine(@$"{i + 1}. {productTypes[i].Title}");
    }
    while (true)
    {
        //user selects type
        userInput = Console.ReadLine().Trim();
        if (int.TryParse(userInput, out _))
        {
            ProductCat = int.Parse(userInput);
            //create new product instance
            Product newProduct = new Product()
            {
                Name = ProductName,
                Price = ProductPrice,
                ProductTypeId = ProductCat
            };
            //add new product to list
            products.Add(newProduct);
        }
        else
        {
            break;
        }
        Console.WriteLine(@"Your Item Has Been Added
    
    Press Any Key");
        Console.ReadKey();
        return;

    }

}

void UpdateProduct(List<Product> products, List<ProductType> productTypes)
{
    string userInput = null;
    Product itemToUpdate;
    //display products
    Console.WriteLine(@"Updating an item...
    Choose an item from the list:");
    foreach (Product product in products)
    {
        Console.WriteLine(@$"{products.IndexOf(product) + 1}. {product.Name}");
    }
    //userinput
    while (true)
    {
        //find product with provided num
        userInput = Console.ReadLine().Trim();
        if (int.TryParse(userInput, out int unindexedItemToUpdateID))
        {
            itemToUpdate = products[unindexedItemToUpdateID - 1];
            break;

        }
    }
    //prompt user to enter name, price, and type.
    Console.WriteLine(@"What would you like its name to be?
You can enter an empty value to leave it unchanged.");
    while (true)
    {
        userInput = Console.ReadLine().Trim();
        //if any is empty string, move on to next property
        if (userInput == "")
        {
            break;
        }
        else
        //update product
        {
            itemToUpdate.Name = userInput;
            break;
        }
    }
    Console.WriteLine("Now tell me how much it costs");
    while (true)
    {
        userInput = Console.ReadLine().Trim();
        if (userInput == "")
        {
            break;
        }
        else
        {
            itemToUpdate.Price = decimal.Parse(userInput);
            break;
        }
    }
    Console.WriteLine("Select a category from the following:");
    foreach (ProductType productType in productTypes)
    {
        Console.WriteLine($"{productTypes.IndexOf(productType) + 1}. {productType.Title}");
    }
    while (true)
    {
        userInput = Console.ReadLine().Trim();
        if (userInput == "")
        {
            break;
        }
        else
        {
            itemToUpdate.ProductTypeId = int.Parse(userInput) - 1;
            break;
        }
    }
    Console.WriteLine(@"Your item has been updated
Hit the slay button (or any button) to return to main menu");
    Console.ReadKey();
    return;

}

// don't move or change this!
public partial class Program { }
// Lab-3 

Product IceCream = new Product(null, 1, null);
Product Coke = new Product("Coke-in-can", 2, "asd");
Product Coffee = new Product("Coffee-in-can", 1, "zxc");

VendingMachine LobbyVM = new VendingMachine(null);

LobbyVM.StockItem(IceCream, -20);
LobbyVM.StockItem(Coke, 10);
LobbyVM.StockItem(IceCream, 5);
LobbyVM.StockFLoat(1, 200);
LobbyVM.StockFLoat(1, 150);
LobbyVM.StockFLoat(5, 120);
//LobbyVM.StockFLoat(10, 100);
//LobbyVM.StockFLoat(20, 90);
//LobbyVM.StockFLoat(50, 50);
//LobbyVM.StockFLoat(100, 50);

List<int> money = new List<int>();
money.Add(10);
money.Add(5);
money.Add(1);

LobbyVM.VendItem(null, money);

class VendingMachine
{
    public VendingMachine(string barcode)
    {
        try
        {
            if (barcode == null)
            {
                throw new ArgumentNullException("ERROR: Null Exception");
            }
            Barcode = barcode;
            SerialNumber++;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public string Barcode { get; }
    public int SerialNumber { get; set; } = 1;
    public Dictionary<int, int> MoneyFloat { get; set; } = new Dictionary<int, int>();
    public Dictionary<Product, int> Inventory { get; set; } = new Dictionary<Product, int>();

    public string StockItem(Product product, int quantity)
    {
        int newValue = 0;
        bool productExists = false;

        try
        {
            if(quantity < 0)
            {
                throw new Exception("ERROR: Argument value cannot be negative");
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        // check if product is in the inventory
        foreach(KeyValuePair<Product, int> pair in Inventory)
        

        // if the product is not yet in the inventory
        if (!productExists)
        {
            Inventory.Add(product, quantity);
            newValue = quantity;

        }
        Console.WriteLine($"Product: {product.Name}, Code: {product.Code}, Price: {product.Price}, Added Quantity: {quantity}, Total Quantity: {newValue}");
        return $"{product.Name}, {product.Code}, {product.Price}, {newValue}";
    }

    public string StockFLoat(int moneyDenomination, int quantity)
    {
        bool denominationExists = false;
        foreach (KeyValuePair<int, int> pair in MoneyFloat)
        {
            if (moneyDenomination == pair.Key)
            {
                MoneyFloat[pair.Key] = pair.Value + quantity;
                denominationExists = true;
            }
        }
        if (!denominationExists)
        {
            MoneyFloat.Add(moneyDenomination, quantity);
        }

        Console.WriteLine($"New money: {moneyDenomination} added {quantity} pcs.");
        foreach(KeyValuePair<int, int> pair in MoneyFloat)
        {
            Console.WriteLine($"{pair.Key} has {pair.Value} pcs.");
        }
        return $"Great job!";
    }

    public Dictionary<int, int> GetDenominationOfChange(int totalChange, Dictionary<int, int> moneyFloat)
    {
        Dictionary<int, int> change = new Dictionary<int, int>();
        // list the money denomination change
        for(int i = moneyFloat.Count - 1; i >= 0; i--)
        {
            
            int integer = moneyFloat.ElementAt(i).Key;
            int pieces = 0;
            int subtractor = 0;
            if(totalChange / integer >= 2)
            {
                pieces = totalChange / integer;
                subtractor -= pieces * integer;
                Console.WriteLine($"======================{integer}---{pieces}");
                change.Add(integer, pieces);
            }
            else
            {
                pieces = totalChange - subtractor;
                change.Add(integer, pieces);
            }
        }   
        return change;
    }

    public string VendItem(string code, List<int> insertedMoney)
    {
        try
        {
            if (code == null || insertedMoney == null)
            {
                throw new Exception("INVALID ARGUMENT: Cannot be null");
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


        int cashIn = insertedMoney[0];
        
        Console.WriteLine("----------------------------------------------------------------");
        // get the total amount of cashed in and the denomination
        for (int i = 0; i < insertedMoney.Count; i++)
        {
            // compute total money cashed in
            if(insertedMoney.Count - 1 == i)
            {
                cashIn = cashIn;
            }
            else {
                cashIn +=  insertedMoney[i + 1];
            }
            Console.WriteLine($"Successfully inserted {insertedMoney[i]}");

            foreach (KeyValuePair<int, int> money in MoneyFloat)
            {
                // update the denomination in MoneyFloat
                if (insertedMoney[i] == money.Key)
                {
                    MoneyFloat[money.Key] += 1;
                }
            }
        }
        Console.WriteLine($"You've inserted a total of ${cashIn}");
        int totalChange = 0;
        foreach (KeyValuePair<Product, int> pair in Inventory)
        {
            //Console.WriteLine("sample");
            // if the product exist in the inventory or in-stock
            if (pair.Key.Code == code)
            {
                Console.WriteLine("================================================");
                Console.WriteLine($"You have selected {pair.Key.Name}");
                Console.WriteLine($"Price is ${pair.Key.Price} each");

                // updates the inventory
                Console.WriteLine($"00000000000000000000 {Inventory[pair.Key] - 1}");
                Inventory[pair.Key] -= 1;
                
                List<int> change = new List<int>();
                Console.WriteLine($"There are {pair.Value} pc(s) remaining for ");
                foreach (KeyValuePair<int, int> moneyPair in MoneyFloat)
                {
                    // update the money float, vend change
                    if (cashIn > pair.Key.Price)
                    {
                        totalChange = cashIn - pair.Key.Price;

                        for(int i = 0; i < change.Count; i++)
                        {
                            if (moneyPair.Key == change[i])
                            {
                                MoneyFloat[moneyPair.Key] -= 1;
                                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                                Console.WriteLine($"Successful Vend: {moneyPair.Key} has remaining {moneyPair.Value} pc(s)");
                            }
                        }
                    }
                    else if(cashIn == pair.Key.Price)
                    {
                        Console.WriteLine($"Thank you for entering the exact amount");
                    }

                }
                
                
                Console.WriteLine($"You have received a total of ${totalChange} for your change");
            }
        }
        return "Vend complete";
    }
}



class Product
{
    public string Name { get; set; }
    public int Price { get; set; }
    public string Code { get; set; }
    public Product(string name, int price, string code)
    {
        
        try
        {
            if (name == null || price == null || code == null)
            {
                throw new ArgumentNullException("ERROR: Null Exception");
            }
            Name = name;
            Price = price;
            Code = code;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
    }
}


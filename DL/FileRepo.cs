using System.Text.Json;
//Data Library
/*
This serves the functions of the interfacing with the backend, performing regular functions that the website needs done.
*/
namespace DL;

//This class reads and writes to the file
public class FileRepo : IRepo
{
    public FileRepo(){ }

    private string filePath = "../DL/Stores.json";

    //Stores
    public List<Store> GetAllStores()
    {
        //string jsonString = File.ReadAllText(filePath);

        string jsonString = "";
        try
        {
            jsonString = File.ReadAllText(filePath);
        }    
        catch(FileNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return JsonSerializer.Deserialize<List<Store>>(jsonString) ?? new List<Store>();

    }

    // public Store GetStoreByIndex(int index)
    // {
    //     List<Store> allStores = GetAllStores();
    //     // for(int i = 0; i < allStores.Count; i++)
    //     // {
    //     //     if(i == index) return allStores[i];
    //     // }

    //     return allStores[index];
    // }

    public void AddStore(Store storeToAdd)
    {
        
        /*Serialization in C# is the process of bringing an object into a form that it can be written on stream. 
        It's the process of converting the object into a form so that it can be stored on a file, database, or 
        memory; or, it can be transferred across the network. Its main purpose is to save the state of the object 
        so that it can be recreated when needed.

        As the name suggests, deserialization in C# is the reverse process of serialization. It is the process of 
        getting back the serialized object so that it can be loaded into memory. It resurrects the state of the 
        object by setting properties, fields etc.
        */

        //First, we're going to grab all the Stores from the file
        //Second, we'll deserialize as List<Store>
        List<Store> allStores = GetAllStores();
        //Third, we'll use List's Add method to add our new Store
        allStores.Add(storeToAdd);

        //Lastly, we'll serialize that List<Store> and then write it to the file
        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);
    }
    public void ChangeStoreInfo(int storeIndex, string name, string city, string state)
    {
        List<Store> allStores = GetAllStores();
        
        allStores[storeIndex].StoreName = name;
        allStores[storeIndex].City = city;
        allStores[storeIndex].State = state;

        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);
    }

    public void RemoveStore(int storeToRemove)//Store storeToRemove)
    {
        
        List<Store> allStores = GetAllStores();
        allStores.RemoveAt(storeToRemove);

        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);
    }

    //Inventory
    private string filePathInv = "../DL/Inventory.json";
    public List<ProdDetails> GetAllInventory()
    {
        //string jsonString = File.ReadAllText(filePath);

        string jsonString = "";
        try
        {
            jsonString = File.ReadAllText(filePathInv);
        }    
        catch(FileNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return JsonSerializer.Deserialize<List<ProdDetails>>(jsonString) ?? new List<ProdDetails>();

    }

    public void AddInventory(int storeIndex, ProdDetails invToAdd) //storeIndex not used?
    {
        List<ProdDetails> allInv = GetAllInventory(); 
        
        allInv.Add(invToAdd);
        
        string jsonString = JsonSerializer.Serialize(allInv);
        File.WriteAllText(filePathInv, jsonString);
    }
    public void ChangeInventory(int storeIndex, int itemIndex, int itemQty)
    {
        //1. Grab all inventory
        List<ProdDetails> allInv = GetAllInventory();


        for(int i = 0; i < allInv.Count; i++)
        {    
            //Console.WriteLine($"allInv[i].StoreAt: {allInv[i].StoreAt}, storeIndex: {storeIndex}, i: {i}, itemIndex: {itemIndex}");
            if(allInv[i].StoreAt == storeIndex && i == itemIndex)
            {
                allInv[i].OnHand = itemQty; 
                Console.WriteLine($"\n{allInv[i].Name} qty changed to {itemQty} for this store\n");
            }
        }
        
        string jsonString = JsonSerializer.Serialize(allInv);
        File.WriteAllText(filePathInv, jsonString); 
    }

    public void RemoveInventory(int invIndexToRemove)
    {
        List<ProdDetails> allInv = GetAllInventory();
        //Loop through inventory and delete all instances of this object
        // for(int i = 0; i < allInv.Count; i++)
        // { 
        //     if(allInv[i].APN == invItemToRemove)
        //     {allInv.RemoveAt(i);}
        // }
        allInv.RemoveAt(invIndexToRemove);

        string jsonString = JsonSerializer.Serialize(allInv);
        File.WriteAllText(filePathInv, jsonString);
    }

    //=======================<<<<  CUSTOMER SECTION  >>>>===========================\\
    
    private string filePathC = "../DL/Customers.json";

    //public List<Customers> CustomerList = new List<Customers>(); //Local Customer
    public List<Customers> GetAllCustomers()
    {
        string jsonString = File.ReadAllText(filePathC);

        return JsonSerializer.Deserialize<List<Customers>>(jsonString) ?? new List<Customers>(); 
    }

    // public Customers GetCustomerByIndex(int index)
    // {
    //     List<Customers> allCustomers = GetAllCustomers();
    //     return allCustomers[index];
    // }

    public void AddCustomer(int custNum, string userName, string pass)
    {
        int custNumbAssg = 0;
        bool canMake = true; //Can make new account

        //1. Grab all customers
        List<Customers> allCustomers = GetAllCustomers();
        custNumbAssg = allCustomers.Count; //Get next customer number

        if(canMake == true)
        {
            //2. Set new customer data
            Customers newCust = new Customers {
                CustNumb = custNumbAssg,
                UserName = userName,
                Pass = pass
            };

            //3. Append Customers 
            allCustomers.Add(newCust);

            //4. Write to file
            string jsonString = JsonSerializer.Serialize(allCustomers);
            File.WriteAllText(filePathC, jsonString);
        }
    }


    //=======================<<<<   CARRIED ITEMS   >>>>===========================\\

    private string filePathIC = "../DL/Carried.json";

    //AddCarried
    public List<ProdDetails> GetAllCarried()
    {
        string jsonString = File.ReadAllText(filePathIC);
        return JsonSerializer.Deserialize<List<ProdDetails>>(jsonString) ?? new List<ProdDetails>(); 
    }

    public void AddCarried(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight)
    {
        //_dl.AddCarried(itemNum, itemName, itemType, itemDesc, itemCost, itemWeight);
        
        int carrNumbAssg = 0;
        bool canMake = true; //Can make new account

        //1. Grab all customers
        List<ProdDetails> allCarried = GetAllCarried();
        carrNumbAssg = allCarried.Count; //Get next customer number

        if(canMake == true)
        {
            //2. Set new customer data
            ProdDetails newCarry = new ProdDetails {
                APN = carrNumbAssg,
                Name = itemName,
                ItemType = itemType,
                Desc = itemDesc,
                Cost = itemCost,
                Weight = itemWeight
            };
            //void AddCarried(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight);

            //3. Append Carried 
            allCarried.Add(newCarry);

            string jsonString = JsonSerializer.Serialize(allCarried);
            //SaveCarried(jsonString);
            //4. Write to file
            File.WriteAllText(filePathIC, jsonString);
        }

    }
    public void ChangeCarried(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight)
    {
        //_dl.AddCarried(itemNum, itemName, itemType, itemDesc, itemCost, itemWeight);
        
        int carrNumbAssg = 0;
        bool canMake = true; //Can make new account

        //1. Grab all customers
        List<ProdDetails> allCarried = GetAllCarried();
        carrNumbAssg = allCarried.Count; //Get next customer number

        if(canMake == true)
        {
            //2. Set new customer data
            // ProdDetails newCarry = new ProdDetails {
            //     APN = carrNumbAssg,
            //     Name = itemName,
            //     ItemType = itemType,
            //     Desc = itemDesc,
            //     Cost = itemCost,
            //     Weight = itemWeight
            // };
            //void AddCarried(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight);

            //3. Change Carried
            allCarried[itemNum].Name = itemName;
            allCarried[itemNum].ItemType = itemType;
            allCarried[itemNum].Desc = itemDesc;
            allCarried[itemNum].Cost = itemCost;
            allCarried[itemNum].Weight = itemWeight;
            //allCarried.Add(newCarry);

            string jsonString = JsonSerializer.Serialize(allCarried);
            //SaveCarried(jsonString);
            //4. Write to file
            File.WriteAllText(filePathIC, jsonString);
        }

    }

    public void RemoveCarried(int carriedIndexToRemove)
    {
        List<ProdDetails> allcarry= GetAllCarried();
        //Loop through all carried items and remove the cancelled item
        // for(int i = 0; i < allcarry.Count; i++)
        // { 
        //     if(allcarry[i].APN == carriedToRemove)
        //     {allcarry.RemoveAt(i);}
        // }
        allcarry.RemoveAt(carriedIndexToRemove);

        string jsonString = JsonSerializer.Serialize(allcarry);
        File.WriteAllText(filePathIC, jsonString);
    }

    // public void SaveCarried(int entryIndex, int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight) //string jsongStr
    // {
    //         //4. Write to file
    //         File.WriteAllText(path, entryIndex, itemNum, itemName, itemType, itemDesc, itemCost, itemWeight);
    // }
}
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

    public List<Store> GetAllStores()
    {
        string jsonString = File.ReadAllText(filePath);

        return JsonSerializer.Deserialize<List<Store>>(jsonString); //Bug
    }

    public Store GetStoreByIndex(int index)
    {
        List<Store> allStores = GetAllStores();
        // for(int i = 0; i < allStores.Count; i++)
        // {
        //     if(i == index) return allStores[i];
        // }

        return allStores[index];
    }

    public void AddStore(Store restToAdd)
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
        allStores.Add(restToAdd);

        //Lastly, we'll serialize that List<Store> and then write it to the file
        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);
    }

    public void AddClay(int storeIndex, Clay clayToAdd)
    {
        //1. Grab all Stores
        List<Store> allStores = GetAllStores();

        //2. Find the Store by its index
        Store selectedStore = allStores[storeIndex];
        
        //3. Append Clay
        if(selectedStore.locClay == null)
        {
            selectedStore.locClay = new List<Clay>();
        }
        selectedStore.locClay.Add(clayToAdd);

        //4. Write to file
        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);
    }

    public void AddTools(int storeIndex, Tools toolsToAdd)
    {
        //1. Grab all Stores
        List<Store> allStores = GetAllStores();

        //2. Find the Store by its index
        Store selectedStore = allStores[storeIndex];
        
        //3. Append Tools
        if(selectedStore.locTools == null)
        {
            selectedStore.locTools = new List<Tools>();
        }
        selectedStore.locTools.Add(toolsToAdd);

        //4. Write to file
        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);
    }

    public void AddEquip(int storeIndex, Equip equipToAdd)
    {
        //1. Grab all Stores
        List<Store> allStores = GetAllStores();
        
        //2. Find the Store by its index
        Store selectedStore = allStores[storeIndex];
        
        //3. Append Equip
        if(selectedStore.locEquip == null)
        {
            selectedStore.locEquip = new List<Equip>();
        }
        selectedStore.locEquip.Add(equipToAdd);

        //4. Write to file
        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);
    }


    //=======================<<<< CUSTOMER SECTION  >>>>===========================\\
    
    //public List<Customers> CustomerList = new List<Customers>(); //Local Customer
    public List<Customers> GetAllCustomers()
    {
        string jsonString = File.ReadAllText(filePath);

        return JsonSerializer.Deserialize<List<Customers>>(jsonString); //Bug
    }

    public Customers GetCustomerByIndex(int index)
    {
        List<Customers> allCustomers = GetAllCustomers();
        return allCustomers[index];
    }

    public void AddCustomer(string userName, string pass)
    {
        int custNumbAssg = 0;

        //1. Grab all Customers
        List<Customers> allCustomers = GetAllCustomers();
        custNumbAssg = allCustomers.Count+1; //Get next customer number

        Customers newCust = new Customers {
            CustNumb = custNumbAssg,
            UserName = userName,
            Pass = pass
        };

        allCustomers.Add(newCust);

        //4. Write to file
        string jsonString = JsonSerializer.Serialize(allCustomers);
        File.WriteAllText(filePath, jsonString);
    }
}
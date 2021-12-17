using System.Text.Json;
//Data Library
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
        //First, we're going to grab all the Stores from the file
        //Second, we'll deserialize as List<Store>
        //Third, we'll use List's Add method to add our new Store
        //Lastly, we'll serialize that List<Store> and then write it to the file

        List<Store> allStores = GetAllStores();
        allStores.Add(restToAdd);

        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);
    }

    public void AddClay(int storeIndex, Clay clayToAdd)
    {
        //1. Grab all Stores
        //2. Find the Store by its index
        //3. Append Clay
        //4. Write to file

        List<Store> allStores = GetAllStores();

        Store selectedStore = allStores[storeIndex];
        
        if(selectedStore.locClay == null)
        {
            selectedStore.locClay = new List<Clay>();
        }
        selectedStore.locClay.Add(clayToAdd);

        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);
    }

    public void AddTools(int storeIndex, Tools toolsToAdd)
    {
        //1. Grab all Stores
        //2. Find the Store by its index
        //3. Append Tools
        //4. Write to file

        List<Store> allStores = GetAllStores();

        Store selectedStore = allStores[storeIndex];
        
        if(selectedStore.locTools == null)
        {
            selectedStore.locTools = new List<Tools>();
        }
        selectedStore.locTools.Add(toolsToAdd);

        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);
    }

    public void AddEquip(int storeIndex, Equip equipToAdd)
    {
        //1. Grab all Stores
        //2. Find the Store by its index
        //3. Append Equip
        //4. Write to file

        List<Store> allStores = GetAllStores();

        Store selectedStore = allStores[storeIndex];
        
        if(selectedStore.locEquip == null)
        {
            selectedStore.locEquip = new List<Equip>();
        }
        selectedStore.locEquip.Add(equipToAdd);

        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);
    }
}
using Models;
using System.Text.Json;

namespace DL;

//Repository Design Pattern

//Read/Write to file
public class FileRepo 
{
    public FileRepo(){}

    private string filePath = "../DL/Rest.json";
    //GetAllRest from file
    public List<Store> GetAllStore()
    {
        //Returns all Rest
        string jsonString = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<Store>>(jsonString);
    }

    public Store GetStoreByIndex(int index)
    {
        List<Rest>allStore = GetAllStore();
        return allStore[index];
    }

    //AddRest to a file
    public void AddStore(Store StoreToAdd)
    {
        List<Rest> allStore = GetAllRest();
        allStore.Add(restToAdd);
        string jsonStr = JsonSerializer.Serialize(allRest);
        File.WriteAllText(filePath, jsonStr);
    }

    //Add Product to file
    //This will add what stores have what items and how many
    public void AddProduct(int StoreIndex, Store StoreToAdd)
    {
        //StaticStorage._allRest[restIndex].Reviews.Add(reviewToAdd);
        List<Store> allStore = GetAllStore();

        Store selectStore = allStore[StoreIndex];//Access the store we want to add product to

        if(selectStore.locClay == null)
        {selectStore.locClay = new List<Clay>();}//If List is Null make it
        selectStore.locClay.Add(StoreToAdd);//Add Clay product to Store

        if(selectStore.locTools == null)
        {selectStore.locTools = new List<Tools>();}//If List is Null make it
        selectStore.locTools.Add(StoreToAdd);//Add Clay product to Store

        if(selectStore.locEquip == null)
        {selectStore.locEquip = new List<Equip>();}//If List is Null make it
        selectStore.locEquip.Add(StoreToAdd);//Add Clay product to Store

        string jsonString = JsonSerializer.Serialize(allStore); //Save to File
        File.WriteAllText(filePath, jsonString);
    }

} 



namespace DL;
public class StaticStorage : IRepo
{
    //Will make only one list: static
    private static List<Store> _allStore = new List<Store>(); 
    
    /// <summary>
    /// Returns all stores from _allStore List
    /// </summary>
    /// <returns>_allStore</returns>
    public List<Store> GetAllStores()
    {
        return StaticStorage._allStore;
    }

    /// <summary>
    /// Adds a new store to the list
    /// </summary>
    /// <param name="storeToAdd"></param>
    public void AddStore(Store storeToAdd)
    {
        StaticStorage._allStore.Add(storeToAdd);
    }

    /// <summary>
    /// Adds clay object to the clay List that the user has selected
    /// </summary>
    /// <param name="clayIndex"></param>
    /// <param name="clayToAdd"></param>
    public void AddClay(int clayIndex, Clay clayToAdd)
    {
        StaticStorage._allStore[clayIndex].locClay.Add(clayToAdd);
    }
    public void AddTools(int toolsIndex, Tools toolsToAdd)
    {
        StaticStorage._allStore[toolsIndex].locTools.Add(toolsToAdd);
    }
    public void AddEquip(int equipIndex, Equip equipToAdd)
    {
        StaticStorage._allStore[equipIndex].locEquip.Add(equipToAdd);
    }
    
    
 
    
    //Customers
    private static List<Customers> _allCust = new List<Customers>(); 

    public List<Customers> GetAllCustomers()
    {
        return StaticStorage._allCust;
    }
    
    public void AddCustomer(int custNum, string userName, string pass)
    {
        if(userName != null){
        StaticStorage._allCust[custNum].UserName = userName;
        StaticStorage._allCust[custNum].Pass = pass;}
    }

    //Carried Items
    private static List<ProdDetails> _allCarr = new List<ProdDetails>(); 

    public List<ProdDetails> GetAllCarried()
    {
        return StaticStorage._allCarr;
    }

    public void AddCarried(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight)
    {
        //_dl.AddCarried(itemNum, itemName, itemType, itemDesc, itemCost, itemWeight);
        if(itemName != null)
        {
            StaticStorage._allCarr[itemNum].Name = itemName;
            StaticStorage._allCarr[itemNum].ItemType = itemType;
            StaticStorage._allCarr[itemNum].Desc = itemDesc;
            StaticStorage._allCarr[itemNum].Cost = itemCost;
            StaticStorage._allCarr[itemNum].Weight = itemWeight;
        }
    }
}
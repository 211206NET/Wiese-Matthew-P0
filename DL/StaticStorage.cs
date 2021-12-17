
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
}
//Business Library
/*
This serves the functions of the business needs, performing regular functions that the management needs done.
*/
namespace BL;
public class CSBL : IBL
{

    private IRepo _dl;

    public CSBL(IRepo repo)
    {
        _dl = repo;
    }

    public List<Store> GetAllStores()
    {
        return _dl.GetAllStores();
    }

    /// <summary>
    /// Adds a Store to the list of Stores
    /// </summary>
    /// <param name="storeToAdd">Store object to add</param>
    public void AddStore(Store storeToAdd)
    {
        _dl.AddStore(storeToAdd);
    }

    /// <summary>
    /// Adds a new clay item to Clay list
    /// </summary>
    /// <param name="storeIndex">Index of Store to add clay inventory to</param>
    /// <param name="clayToAdd">A clay object to be added to store object</param>
    public void AddClay(int storeIndex, Clay clayToAdd)
    {
        _dl.AddClay(storeIndex, clayToAdd);
    }

    /// <summary>
    /// Adds a new Tools item to Tools list
    /// </summary>
    /// <param name="storeIndex">Index of Store to add tools inventory to</param>
    /// <param name="toolsToAdd">A tools object to be added to store object</param>
    public void AddTools(int storeIndex, Tools toolsToAdd)
    {
        _dl.AddTools(storeIndex, toolsToAdd);
    }

    /// <summary>
    /// Adds a new Equipment item to Tools list
    /// </summary>
    /// <param name="storeIndex">Index of Store to add equipment inventory to</param>
    /// <param name="equipToAdd">A equipment object to be added to store object</param>
    public void AddEquip(int storeIndex, Equip equipToAdd)
    {
        _dl.AddEquip(storeIndex, equipToAdd);
    }
}

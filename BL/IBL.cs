namespace BL;

//Interface for Stores
public interface IBL
{
    // Store SearchStore(string searchString);

    List<Store> GetAllStores();

    void AddStore(Store storeToAdd);

    void AddClay(int storeIndex, Clay clayToAdd);
    void AddTools(int storeIndex, Tools toolsToAdd);
    void AddEquip(int storeIndex, Equip equipToAdd);
}
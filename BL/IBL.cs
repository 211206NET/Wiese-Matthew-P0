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

    //Customers
    
    List<Customers> GetAllCustomers();

    void AddCustomer(int custNum, string userName, string pass);

    //Carried Items

    //AddCarried
    List<ProdDetails> GetAllCarried();

    void AddCarried(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight);
    //void AddCarried(ProdDetails addCarry);


}
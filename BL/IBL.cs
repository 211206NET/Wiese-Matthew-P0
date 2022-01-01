namespace BL;

//Interface for Stores
public interface IBL
{
    // Store SearchStore(string searchString);

    List<Store> GetAllStores();
    List<Inventory> GetAllInventory();

    void AddStore(Store storeToAdd);
    void ChangeStoreInfo(int storeIndex, string name, string city, string state);
    void RemoveStore(int storeToRemove);


    void AddInventory(Inventory invToAdd);
    void AddItem(int invIndex, ProdDetails invToAdd);
    void ChangeInventory(int invIndex, int apn, int itemQty);    
    void RemoveInventory(int invIndex, int invIndexToRemove);

    //Customers
    List<Customers> GetAllCustomers();

    void AddCustomer(int custNum, string userName, string pass);

    //Carried Items

    //AddCarried
    List<ProdDetails> GetAllCarried();

    void AddCarried(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight);
    //void AddCarried(ProdDetails addCarry);

    void ChangeCarried(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight);
    void RemoveCarried(int carriedIndexToRemove);

    //Line Items
    List<LineItems> GetAllLineItem();

    void AddLineItem(int apn, string name, int qty, Decimal costPerItem, Decimal salesTax);

    void RemoveLineItem(int lineItemIndexToRemove);


}
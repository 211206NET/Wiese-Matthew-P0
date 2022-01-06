namespace BL;

//Interface for Stores
public interface IBL
{
    // Store SearchStore(string searchString);

    List<Store> GetAllStores();
    List<Inventory> GetAllInventory();

    void AddStore(Store storeToAdd);
    void ChangeStoreInfo(int storeIndex, Store changeStoreInfo);//(int storeIndex, string name, string city, string state);
    void RemoveStore(int storeToRemove);


    void AddInventory(Inventory invToAdd);
    void AddItem(int invIndex, ProdDetails invToAdd);
    void ChangeInventory(int invIndex, int apn, int itemQty);    
    void RemoveInventory(int invIndex);
    void RemoveItem(int invIndex, int invIndexToRemove);

    //Customers
    List<Customers> GetAllCustomers();
    void AddCustomer(Customers addCust);//int custNum, string userName, string pass, bool employee);

    //Carried Items
    List<ProdDetails> GetAllCarried();
    void AddCarried(ProdDetails itemNew);//(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight);
    void ChangeCarried(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight);
    void RemoveCarried(int carriedIndexToRemove);

    //Line Items
    List<LineItems> GetAllLineItem();
    void AddLineItem(LineItems newLI);//int apn, string name, int qty, Decimal costPerItem, Decimal salesTax);
    void RemoveLineItem(int lineItemIndexToRemove);

    List<Orders> GetAllOrders();
    void AddOrder(Orders orderItems);


}
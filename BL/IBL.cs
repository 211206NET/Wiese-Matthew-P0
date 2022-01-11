namespace BL;

//Interface for Stores
public interface IBL
{
    // Store SearchStore(string searchString);

    List<Store> GetAllStores();
    List<Inventory> GetAllInventory();

    void AddStore(Store storeToAdd);
    void ChangeStoreInfo(Store changeStoreInfo);//(int storeIndex, string name, string city, string state);
    void RemoveStore(int storeToRemove);


    void AddInventory(Inventory invToAdd);
    void AddItem(ProdDetails invToAdd); //int invIndex, 
    void ChangeInventory(int invIndex, int qtyToChange);//int invIndex, int apn, int itemQty);    
    void RemoveInventory(int invId);
    void RemoveOrphanInventory(int storePK);
    void RemoveItem(int apnToRemove);

    //Customers
    List<Customers> GetAllCustomers();
    void AddCustomer(Customers addCust);//int custNum, string userName, string pass, bool employee);

    //Carried Items
    List<ProdDetails> GetAllCarried();
    void AddCarried(ProdDetails itemNew);//(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight);
    void ChangeCarried(ProdDetails changeCarriedItem);//int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight);

    //Line Items
    List<LineItems> GetAllLineItem();
    void AddLineItem(LineItems newLI);//int apn, string name, int qty, Decimal costPerItem, Decimal salesTax);
    void RemoveLineItem(int lineItemIndexToRemove);
    void RemoveOrphanLineItem(int storePK);
    void FinalizeLineItem(LineItems finalLineItem);
    List<Orders> GetAllOrders();
    void AddOrder(Orders orderItems);

    void FinalizeOrder(int orderIndex, Orders finalDetails);

    //In the event a store is closed, all it's records are deleted
    void DeleteOrders(int ordersToDelete);


}
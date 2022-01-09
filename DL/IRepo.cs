namespace DL;

//What's an interface?
//Interface is a contract, in essence
//It enforces that any type that implements the interface
//must also implement all of the interface's members as public methods
//We use interface to define/enforce a certain set of behavior on a type (such as class)
//This is an example of Abstraction (one of 4 OOP Pillars)
//Other OOP Pillars are Polymorphism, Inheritance, Encapsulation (A PIE!)
//Interface only has methods
public interface IRepo
{
    //Notice, how we're lacking access modifiers
    //interface members are implicitely public
    //they also lack method body

    //Store
    List<Store> GetAllStores();
    void AddStore(Store StoreToAdd);
    void ChangeStoreInfo(int storeIndex, Store changeStoreInfo);//(int storeIndex, string name, string city, string state);
    void RemoveStore(int StoreToRemove);

    //Inventory
    List<Inventory> GetAllInventory();
    void AddInventory(Inventory invToAdd);
    void AddItem(int invIndex, ProdDetails invToAdd);
    void ChangeInventory(int invIndex, int qtyToChange);//int invIndex, int apn, int itemQty);    
    void RemoveInventory(int invIndex);
    void RemoveItem(int apnToRemove);

    //Customers
    List<Customers> GetAllCustomers();
    void AddCustomer(Customers addCust);//int custNum, string userName, string pass);

    //Carried Items
    List<ProdDetails> GetAllCarried();
    void AddCarried(ProdDetails itemNew);//(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight);
    void ChangeCarried(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight);
    void RemoveCarried(int apnToRemove);

    //Line Items
    List<LineItems> GetAllLineItem();
    void AddLineItem(LineItems newLI);//(int apn, string name, int qty, Decimal costPerItem, Decimal salesTax);
    void RemoveLineItem(int lineItemIndexToRemove);

    //Orders
    List<Orders> GetAllOrders();
    void AddOrder(Orders orderItems);

}
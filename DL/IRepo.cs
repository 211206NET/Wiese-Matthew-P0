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
    List<Store> GetAllStores();
    List<ProdDetails> GetAllInventory();

    void AddStore(Store StoreToAdd);
    void ChangeStoreInfo(int storeIndex, string name, string city, string state);
    void RemoveStore(int StoreToRemove);
    void AddInventory(int storeIndex, ProdDetails invToAdd);
    void ChangeInventory(int storeIndex, int apn, int itemQty);

    void AddClay(int StoreIndex, Clay clayToAdd);
    void AddTools(int StoreIndex, Tools toolsToAdd);
    void AddEquip(int StoreIndex, Equip equipToAdd);

    List<Customers> GetAllCustomers();

    //void AddCustomer(int customerIndex, Customers customerToAdd);
    void AddCustomer(int custNum, string userName, string pass);

    //Carried Items

    //AddCarried
    List<ProdDetails> GetAllCarried();

    void AddCarried(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight);

    
    void ChangeCarried(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight);

}
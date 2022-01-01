
namespace DL;
public class StaticStorage : IRepo
{
    //Will make only one list: static
    private static List<Store> _allStore = new List<Store>(); 
    private static List<Inventory> _allInventory = new List<Inventory>(); 
    private static List<ProdDetails> _allCarried = new List<ProdDetails>(); 
    private static List<LineItems> _allLineItems= new List<LineItems>(); 
    
    /// <summary>
    /// Returns all stores from _allStore List
    /// </summary>
    /// <returns>_allStore</returns>
    public List<Store> GetAllStores()
    {
        return StaticStorage._allStore;
    }
    public List<Inventory> GetAllInventory()
    {
        return StaticStorage._allInventory;
    }

    /// <summary>
    /// Adds a new store to the list
    /// </summary>
    /// <param name="storeToAdd"></param>
    public void AddStore(Store storeToAdd)
    {
        StaticStorage._allStore.Add(storeToAdd);
    }
    public void ChangeStoreInfo(int storeIndex, string name, string city, string state)
    {
        if(name != null)
        {
            StaticStorage._allStore[storeIndex].StoreName = name;
            StaticStorage._allStore[storeIndex].City = city;
            StaticStorage._allStore[storeIndex].State = state;
        }
    }
    public void RemoveStore(int storeToRemove)
    {
        StaticStorage._allStore.RemoveAt(storeToRemove);
    }
    public void AddInventory(Inventory invToAdd)
    {
        StaticStorage._allInventory.Add(invToAdd);
    }
    public void AddItem(int invIndex, ProdDetails invToAdd)
    {
        StaticStorage._allInventory[invIndex].Items.Add(invToAdd);
    }
    public void ChangeInventory(int invIndex, int itemIndex, int itemQty)
    {
        StaticStorage._allInventory[invIndex].Items[itemIndex].OnHand = itemQty;
    }    
    public void RemoveInventory(int invIndex, int invIndexToRemove)
    {
        StaticStorage._allInventory[invIndex].Items.RemoveAt(invIndexToRemove);
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

    public void ChangeCarried(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight)
    {
        if(itemName != null)
        {
            StaticStorage._allCarr[itemNum].Name = itemName;
            StaticStorage._allCarr[itemNum].ItemType = itemType;
            StaticStorage._allCarr[itemNum].Desc = itemDesc;
            StaticStorage._allCarr[itemNum].Cost = itemCost;
            StaticStorage._allCarr[itemNum].Weight = itemWeight;
        }
    }

    public void RemoveCarried(int carriedIndexToRemove)//Store storeToRemove)
    {
        StaticStorage._allCarried.RemoveAt(carriedIndexToRemove);
    }

    //Line Items
    public List<LineItems> GetAllLineItem()
    {
        return StaticStorage._allLineItems;
    }

    public void AddLineItem(int apn, string name, int qty, Decimal costPerItem, Decimal salesTax)
    {
        // if(name != null)
        // {
        //     StaticStorage._allLineItems[itemIndex].Name = name;
        //     StaticStorage._allLineItems[itemIndex].Qty = qty;
        //     StaticStorage._allLineItems[itemIndex].CostPerItem = costPerItem;
        //     StaticStorage._allLineItems[itemIndex].SalesTax = salesTax;
        // }
    }

    public void RemoveLineItem(int lineItemIndexToRemove)
    {
        StaticStorage._allLineItems.RemoveAt(lineItemIndexToRemove);
    }
}
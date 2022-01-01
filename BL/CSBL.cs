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

    ///<>Stores

    public List<Store> GetAllStores()
    {
        return _dl.GetAllStores();
    }
    public List<Inventory> GetAllInventory()
    {
        return _dl.GetAllInventory();
    }

    /// <summary>
    /// Adds a Store to the list of Stores
    /// </summary>
    /// <param name="storeToAdd">Store object to add</param>
    public void AddStore(Store storeToAdd)
    {
        _dl.AddStore(storeToAdd);
    }
    public void ChangeStoreInfo(int storeIndex, string name, string city, string state)
    {
       _dl.ChangeStoreInfo(storeIndex, name, city, state);
    }
    
    public void RemoveStore(int storeToRemove)
    {
        _dl.RemoveStore(storeToRemove);
    }
    public void AddInventory(Inventory invToAdd)
    {
        _dl.AddInventory(invToAdd);
    }
    public void AddItem(int invIndex, ProdDetails invToAdd)
    {
        _dl.AddItem(invIndex, invToAdd);
    }
    public void ChangeInventory(int invIndex, int itemIndex, int itemQty)
    {
        _dl.ChangeInventory(invIndex, itemIndex, itemQty);
    }
    public void RemoveInventory(int invIndex, int invIndexToRemove)
    {
        _dl.RemoveInventory(invIndex, invIndexToRemove);
    }

    ///<>Customers
    
    public List<Customers> GetAllCustomers()
    {
        return _dl.GetAllCustomers();
    }
    public void AddCustomer(int custNum, string userName, string pass)
    {
        _dl.AddCustomer(custNum, userName, pass);
    }

    ///<>Carried Items

    //AddCarried
    public List<ProdDetails> GetAllCarried()
    {
        return _dl.GetAllCarried();
    }


    //public void AddCarried(ProdDetails addDetails)
    public void AddCarried(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight)
    {
        _dl.AddCarried(itemNum, itemName, itemType, itemDesc, itemCost, itemWeight);
        //_dl.AddCarried(addDetails);
    }

    //public void AddCarried(ProdDetails addDetails)
    public void ChangeCarried(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight)
    {
        _dl.ChangeCarried(itemNum, itemName, itemType, itemDesc, itemCost, itemWeight);
    }

    public void RemoveCarried(int carriedIndexToRemove)
    {
        _dl.RemoveCarried(carriedIndexToRemove);
    }  

    //Line Items
    public List<LineItems> GetAllLineItem()
    {
        return _dl.GetAllLineItem();
    }

    public void AddLineItem(int apn, string name, int qty, Decimal costPerItem, Decimal salesTax)
    {
        _dl.AddLineItem(apn, name, qty, costPerItem, salesTax);
    }

    public void RemoveLineItem(int lineItemIndexToRemove)
    {
        _dl.RemoveLineItem(lineItemIndexToRemove);
    }

   

}

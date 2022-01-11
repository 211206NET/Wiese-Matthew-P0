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
    public void ChangeStoreInfo(Store changeStoreInfo)//(int storeIndex, string name, string city, string state)
    {
       _dl.ChangeStoreInfo(changeStoreInfo);
    }
    
    public void RemoveStore(int storeToRemove)
    {
        _dl.RemoveStore(storeToRemove);
    }
    public void AddInventory(Inventory invToAdd)
    {
        _dl.AddInventory(invToAdd);
    }
    public void AddItem(ProdDetails invToAdd) //int invIndex, 
    {
        _dl.AddItem(invToAdd);
    }
    public void ChangeInventory(int invIndex, int qtyToChange)//int invIndex, int itemIndex, int itemQty)
    {
        _dl.ChangeInventory(invIndex, qtyToChange);
    }
    public void RemoveInventory(int invId)
    {
        _dl.RemoveInventory(invId);
    }
    public void RemoveOrphanInventory(int storePK)
    {
        _dl.RemoveOrphanInventory(storePK);
    }
    public void RemoveItem(int apnToRemove)
    {
        _dl.RemoveItem(apnToRemove);
    }

    ///<>Customers
    
    public List<Customers> GetAllCustomers()
    {
        return _dl.GetAllCustomers();
    }
    public void AddCustomer(Customers addCust)//int custNum, string userName, string pass, bool employee)
    {
        _dl.AddCustomer(addCust);//custNum, userName, pass, employee);
    }

    ///<>Carried Items

    //AddCarried
    public List<ProdDetails> GetAllCarried()
    {
        return _dl.GetAllCarried();
    }


    //public void AddCarried(ProdDetails addDetails)
    public void AddCarried(ProdDetails itemNew)//(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight)
    {
        //_dl.AddCarried(itemNum, itemName, itemType, itemDesc, itemCost, itemWeight);
        _dl.AddCarried(itemNew);
    }

    //public void AddCarried(ProdDetails addDetails)
    public void ChangeCarried(ProdDetails changeCarriedItem)//int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight)
    {
        _dl.ChangeCarried(changeCarriedItem);//itemNum, itemName, itemType, itemDesc, itemCost, itemWeight);
    }

    //Line Items
    public List<LineItems> GetAllLineItem()
    {
        return _dl.GetAllLineItem();
    }

    public void AddLineItem(LineItems newLI)//(int apn, string name, int qty, Decimal costPerItem, Decimal salesTax)
    {
        _dl.AddLineItem(newLI);//(apn, name, qty, costPerItem, salesTax);
    }

    public void RemoveLineItem(int lineItemIndexToRemove)
    {
        _dl.RemoveLineItem(lineItemIndexToRemove);
    }   

    public void RemoveOrphanLineItem(int storePK)
    {
        _dl.RemoveOrphanLineItem(storePK);
    }

    public void FinalizeLineItem(LineItems finalLineItem)
    {
        _dl.FinalizeLineItem(finalLineItem);
    }

    //AddOrder
    public List<Orders> GetAllOrders()
    {
        return _dl.GetAllOrders();
    }

    //Add a new order to order history
    public void AddOrder(Orders orderItems)
    {
        _dl.AddOrder(orderItems);
    }

    public void FinalizeOrder(int orderIndex, Orders finalDetails)
    {
        _dl.FinalizeOrder(orderIndex, finalDetails);
    }

    //In the event a store is closed, all it's records are deleted
    public void DeleteOrders(int ordersToDelete)
    {
        _dl.DeleteOrders(ordersToDelete);
    }
   

}

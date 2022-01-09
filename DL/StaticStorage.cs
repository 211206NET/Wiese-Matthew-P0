
// namespace DL;
// A static class in C# is a class that cannot be instantiated.
// A static class can only contain static data members including static methods, static constructors, and static properties.
// In C#, a static class is a class that cannot be instantiated. ... You can't create an object for the static class.
// public class StaticStorage : IRepo
// {
//     //Will make only one list: static
//     private static List<Store> _allStore = new List<Store>(); 
//     private static List<Inventory> _allInventory = new List<Inventory>(); 
//     private static List<ProdDetails> _allCarried = new List<ProdDetails>(); 
//     private static List<LineItems> _allLineItems= new List<LineItems>(); 
    
//     /// <summary>
//     /// Returns all stores from _allStore List
//     /// </summary>
//     /// <returns>_allStore</returns>
//     public List<Store> GetAllStores()
//     {
//         return StaticStorage._allStore;
//     }
//     public List<Inventory> GetAllInventory()
//     {
//         return StaticStorage._allInventory;
//     }

//     /// <summary>
//     /// Adds a new store to the list
//     /// </summary>
//     /// <param name="storeToAdd"></param>
//     public void AddStore(Store storeToAdd)
//     {
//         StaticStorage._allStore.Add(storeToAdd);
//     }
//     public void ChangeStoreInfo(int storeIndex, Store changeStoreInfo)//(int storeIndex, string name, string city, string state)
//     {
//         StaticStorage._allStore[storeIndex] = changeStoreInfo;
//         // if(name != null)
//         // {
//         //     StaticStorage._allStore[storeIndex].StoreName = name;
//         //     StaticStorage._allStore[storeIndex].City = city;
//         //     StaticStorage._allStore[storeIndex].State = state;
//         // }
//     }
//     public void RemoveStore(int storeToRemove)
//     {
//         StaticStorage._allStore.RemoveAt(storeToRemove);
//     }
//     public void AddInventory(Inventory invToAdd)
//     {
//         StaticStorage._allInventory.Add(invToAdd);
//     }
//     public void AddItem(ProdDetails invToAdd) //int invIndex, 
//     {
//         StaticStorage._allCarried.Add(invToAdd);
//     }
//     public void ChangeInventory(int invIndex, int qtyToChange)//int invIndex, int itemIndex, int itemQty)
//     {
//         //StaticStorage._allInventory[invIndex].Items[itemIndex].OnHand = itemQty;
//         //StaticStorage._allInventory[invIndex].Qty += qtyToChange;
//     }    
//     public void RemoveInventory(int invIndex)
//     {
//         StaticStorage._allInventory.RemoveAt(invIndex);
//     }
//     public void RemoveItem(int invIndex, int apnToRemove)
//     {
//         StaticStorage._allInventory.RemoveAt(invIndex);
//     }

//     //Customers
//     private static List<Customers> _allCust = new List<Customers>(); 

//     public List<Customers> GetAllCustomers()
//     {
//         return StaticStorage._allCust;
//     }
    
//     public void AddCustomer(Customers addCust)
//     {
//         StaticStorage._allCust.Add(addCust);
//         // if(userName != null){
//         // StaticStorage._allCust[custNum].UserName = userName;
//         // StaticStorage._allCust[custNum].Pass = pass;}
//     }

//     //Carried Items
//     private static List<ProdDetails> _allCarr = new List<ProdDetails>(); 

//     public List<ProdDetails> GetAllCarried()
//     {
//         return StaticStorage._allCarr;
//     }

//     public void AddCarried(ProdDetails itemNew)//(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight)
//     {
//         StaticStorage._allCarried.Add(itemNew);
//         //_dl.AddCarried(itemNum, itemName, itemType, itemDesc, itemCost, itemWeight);
//         // if(itemName != null)
//         // {
//         //     StaticStorage._allCarr[itemNum].Name = itemName;
//         //     StaticStorage._allCarr[itemNum].ItemType = itemType;
//         //     StaticStorage._allCarr[itemNum].Desc = itemDesc;
//         //     StaticStorage._allCarr[itemNum].Cost = itemCost;
//         //     StaticStorage._allCarr[itemNum].Weight = itemWeight;
//         // }
//     }

//     public void ChangeCarried(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight)
//     {
//         if(itemName != null)
//         {
//             StaticStorage._allCarr[itemNum].Name = itemName;
//             StaticStorage._allCarr[itemNum].ItemType = itemType;
//             StaticStorage._allCarr[itemNum].Descr = itemDesc;
//             StaticStorage._allCarr[itemNum].Cost = itemCost;
//             StaticStorage._allCarr[itemNum].Weight = itemWeight;
//         }
//     }

//     public void RemoveCarried(int apnToRemove)//Store storeToRemove)
//     {
//         StaticStorage._allCarried.RemoveAt(apnToRemove);
//     }

//     //Line Items
//     public List<LineItems> GetAllLineItem()
//     {
//         return StaticStorage._allLineItems;
//     }

//     public void AddLineItem(LineItems newLI)//int apn, string name, int qty, Decimal costPerItem, Decimal salesTax)
//     {
//         StaticStorage._allLineItems.Add(newLI);
//         // if(name != null)
//         // {
//         //     StaticStorage._allLineItems[itemIndex].Name = name;
//         //     StaticStorage._allLineItems[itemIndex].Qty = qty;
//         //     StaticStorage._allLineItems[itemIndex].CostPerItem = costPerItem;
//         //     StaticStorage._allLineItems[itemIndex].SalesTax = salesTax;
//         // }
//     }

//     public void RemoveLineItem(int lineItemIndexToRemove)
//     {
//         StaticStorage._allLineItems.RemoveAt(lineItemIndexToRemove);
//     }

// //AddOrder
// private static List<Orders> _allOrders = new List<Orders>(); 
// public List<Orders> GetAllOrders()
// {
//         return StaticStorage._allOrders;
// }

// //Add a new order to order history
// public void AddOrder(Orders orderItems)
// {
//     StaticStorage._allOrders.Add(orderItems);
// }

// public void FinalizeOrder(int orderIndex, Orders finalDetails)//int storeIndex, Inventory changeInv)//int storeIndex, int apn, int qtyToAdjust
// {
//     StaticStorage._allOrders[orderIndex].TotalQty = finalDetails.TotalQty;
//     StaticStorage._allOrders[orderIndex].TotalCost = finalDetails.TotalCost;
//     StaticStorage._allOrders[orderIndex].OrderId = finalDetails.OrderId;
//     StaticStorage._allOrders[orderIndex].OrderCompleted = finalDetails.OrderCompleted;
//     StaticStorage._allOrders[orderIndex].OrderDate = finalDetails.OrderDate;
// }


// }
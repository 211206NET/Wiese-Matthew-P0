// using System.Text.Json;
// //Data Library
// /*
// This serves the functions of the interfacing with the backend, performing regular functions that the website needs done.
// */
// namespace DL;

// //This class reads and writes to the file
// public class FileRepo //: IRepo
// {
// public FileRepo(){ }

// private string filePath = "../DL/Stores.json";

// //Stores
// public List<Store> GetAllStores()
// {
//     //string jsonString = File.ReadAllText(filePath);

//     string jsonString = "";
//     try
//     {
//         jsonString = File.ReadAllText(filePath);
//     }    
//     catch(FileNotFoundException ex)
//     {
//         Console.WriteLine(ex.Message);
//     }
//     catch(Exception ex)
//     {
//         Console.WriteLine(ex.Message);
//     }

//     return JsonSerializer.Deserialize<List<Store>>(jsonString) ?? new List<Store>();

// }

// // public Store GetStoreByIndex(int index)
// // {
// //     List<Store> allStores = GetAllStores();
// //     // for(int i = 0; i < allStores.Count; i++)
// //     // {
// //     //     if(i == index) return allStores[i];
// //     // }

// //     return allStores[index];
// // }

// public void AddStore(Store storeToAdd)
// {
    
//     /*Serialization in C# is the process of bringing an object into a form that it can be written on stream. 
//     It's the process of converting the object into a form so that it can be stored on a file, database, or 
//     memory; or, it can be transferred across the network. Its main purpose is to save the state of the object 
//     so that it can be recreated when needed.

//     As the name suggests, deserialization in C# is the reverse process of serialization. It is the process of 
//     getting back the serialized object so that it can be loaded into memory. It resurrects the state of the 
//     object by setting properties, fields etc.
//     */

//     //First, we're going to grab all the Stores from the file
//     //Second, we'll deserialize as List<Store>
//     List<Store> allStores = GetAllStores();
//     //Third, we'll use List's Add method to add our new Store
//     allStores.Add(storeToAdd);

//     //Lastly, we'll serialize that List<Store> and then write it to the file
//     string jsonString = JsonSerializer.Serialize(allStores);
//     File.WriteAllText(filePath, jsonString);
// }
// public void ChangeStoreInfo(int storeIndex, Store changeStoreInfo)///(int storeIndex, string name, string city, string state)
// {
//     List<Store> allStores = GetAllStores();
    
//     allStores[storeIndex] = changeStoreInfo;
//     // allStores[storeIndex].StoreName = name;
//     // allStores[storeIndex].City = city;
//     // allStores[storeIndex].State = state;

//     string jsonString = JsonSerializer.Serialize(allStores);
//     File.WriteAllText(filePath, jsonString);
// }

// public void RemoveStore(int storeToRemove)//Store storeToRemove)
// {
    
//     List<Store> allStores = GetAllStores();
//     allStores.RemoveAt(storeToRemove);

//     string jsonString = JsonSerializer.Serialize(allStores);
//     File.WriteAllText(filePath, jsonString);
// }

// //Inventory
// private string filePathInv = "../DL/Inventory.json";
// // public List<ProdDetails> GetAllInventory()
// // {
// //     //string jsonString = File.ReadAllText(filePath);

// //     string jsonString = "";
// //     try
// //     {
// //         jsonString = File.ReadAllText(filePathInv);
// //     }    
// //     catch(FileNotFoundException ex)
// //     {
// //         Console.WriteLine(ex.Message);
// //     }
// //     catch(Exception ex)
// //     {
// //         Console.WriteLine(ex.Message);
// //     }

// //     return JsonSerializer.Deserialize<List<ProdDetails>>(jsonString) ?? new List<ProdDetails>();

// // }
// public List<Inventory> GetAllInventory()
// {
//     //string jsonString = File.ReadAllText(filePath);
//     string jsonString = "";
//     try
//     {
//         jsonString = File.ReadAllText(filePathInv);
//     }    
//     catch(FileNotFoundException ex)
//     {
//         Console.WriteLine(ex.Message);
//     }
//     catch(Exception ex)
//     {
//         Console.WriteLine(ex.Message);
//     }

//     return JsonSerializer.Deserialize<List<Inventory>>(jsonString) ?? new List<Inventory>();
// }

// //Add new Inventory to Inventory List
// public void AddInventory(Inventory invToAdd) 
// {
//     List<Inventory> allInventory = GetAllInventory(); 
//     allInventory.Add(invToAdd); //Old way before Inventory Obj
    
//     string jsonString = JsonSerializer.Serialize(allInventory);
//     File.WriteAllText(filePathInv, jsonString);
// }

// //Add item to existing Inventory
// public void AddItem(int invIndex, ProdDetails invToAdd) 
// {
//     //List<ProdDetails> allInv = GetAllInventory(); //Old way before Inventory Obj
//     List<Inventory> allInventory = GetAllInventory(); 
//     //allInv.Add(invToAdd); //Old way before Inventory Obj
//     Inventory selectedInventory = allInventory[invIndex];
    
//     if(selectedInventory.Items == null)
//     {
//         selectedInventory.Items = new List<ProdDetails>();
//     }
//     selectedInventory.Items.Add(invToAdd);
    
//     string jsonString = JsonSerializer.Serialize(allInventory);
//     File.WriteAllText(filePathInv, jsonString);
// }

// public void ChangeInventory(int invIndex, int itemIndex, int itemQty)//Change store inventory
// {
//     List<Inventory> allInv = GetAllInventory();

//     if(allInv[invIndex].Items.Count > 0)//Must have an entry to change an entry
//     {
//         //allInv[invIndex].Items[itemIndex].OnHand = itemQty; //Override to new value
//         Console.WriteLine($"\n{allInv[invIndex].Items[itemIndex].Name} qty changed to {itemQty} for this store\n");
//     }
//     else
//     {
//         //Adding this item back in!
//         //Error
//     }

//     string jsonString = JsonSerializer.Serialize(allInv);
//     File.WriteAllText(filePathInv, jsonString); 
// }

// //Remove inventory from inventory list
// public void RemoveInventory(int invIndex)
// {
//     List<Inventory> allInv = GetAllInventory();
//     allInv.RemoveAt(invIndex);

//     string jsonString = JsonSerializer.Serialize(allInv);
//     File.WriteAllText(filePathInv, jsonString);
// }

// //Remove item from inventory
// public void RemoveItem(int invIndex, int invIndexToRemove)
// {
//     Console.WriteLine("Got here");
//     List<Inventory> allInv = GetAllInventory();
//     allInv[invIndex].Items.RemoveAt(invIndexToRemove);

//     string jsonString = JsonSerializer.Serialize(allInv);
//     File.WriteAllText(filePathInv, jsonString);
// }

// //=======================<<<<  CUSTOMER SECTION  >>>>===========================\\

// private string filePathC = "../DL/Customers.json";

// //public List<Customers> CustomerList = new List<Customers>(); //Local Customer
// public List<Customers> GetAllCustomers()
// {
//     string jsonString = File.ReadAllText(filePathC);

//     return JsonSerializer.Deserialize<List<Customers>>(jsonString) ?? new List<Customers>(); 
// }

// // public Customers GetCustomerByIndex(int index)
// // {
// //     List<Customers> allCustomers = GetAllCustomers();
// //     return allCustomers[index];
// // }

// public void AddCustomer(Customers addCust)//int custNum, string userName, string pass, bool employee)
// {
//     int custNumbAssg = 0;
//     //bool canMake = true; //Can make new account

//     //1. Grab all customers
//     List<Customers> allCustomers = GetAllCustomers();
//     custNumbAssg = allCustomers.Count; //Get next customer number

//     //if(canMake == true)
//     //{
//         //2. Set new customer data
//         // Customers newCust = new Customers {
//         //     CustNumb = custNumbAssg,
//         //     UserName = userName,
//         //     Pass = pass,
//         //     Employee = employee
//         // };

//         //3. Append Customers 
//         allCustomers.Add(addCust);

//         //4. Write to file
//         string jsonString = JsonSerializer.Serialize(allCustomers);
//         File.WriteAllText(filePathC, jsonString);
//     //}
// }


// //=======================<<<<   CARRIED ITEMS   >>>>===========================\\

// private string filePathIC = "../DL/Carried.json";

// //AddCarried
// public List<ProdDetails> GetAllCarried()
// {
//     string jsonString = File.ReadAllText(filePathIC);
//     return JsonSerializer.Deserialize<List<ProdDetails>>(jsonString) ?? new List<ProdDetails>(); 
// }

// public void AddCarried(ProdDetails itemNew)//(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight)
// {
//     //_dl.AddCarried(itemNum, itemName, itemType, itemDesc, itemCost, itemWeight);
    
//     //int carrNumbAssg = 0;

//     //1. Grab all customers
//     List<ProdDetails> allCarried = GetAllCarried();
//     //carrNumbAssg = allCarried.Count; //Get next customer number

//     //2. Set new customer data
//     // ProdDetails newCarry = new ProdDetails {
//     //     APN = carrNumbAssg,
//     //     Name = itemName,
//     //     ItemType = itemType,
//     //     Desc = itemDesc,
//     //     Cost = itemCost,
//     //     Weight = itemWeight
//     // };
//     //void AddCarried(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight);

//     //3. Append Carried 
//     allCarried.Add(itemNew);

//     string jsonString = JsonSerializer.Serialize(allCarried);
//     //SaveCarried(jsonString);
//     //4. Write to file
//     File.WriteAllText(filePathIC, jsonString);


// }
// public void ChangeCarried(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight)
// {
//     //_dl.AddCarried(itemNum, itemName, itemType, itemDesc, itemCost, itemWeight);
    
//     int carrNumbAssg = 0;
//     bool canMake = true; //Can make new account

//     //1. Grab all customers
//     List<ProdDetails> allCarried = GetAllCarried();
//     carrNumbAssg = allCarried.Count; //Get next customer number

//     if(canMake == true)
//     {
//         //2. Set new customer data
//         // ProdDetails newCarry = new ProdDetails {
//         //     APN = carrNumbAssg,
//         //     Name = itemName,
//         //     ItemType = itemType,
//         //     Desc = itemDesc,
//         //     Cost = itemCost,
//         //     Weight = itemWeight
//         // };
//         //void AddCarried(int itemNum, string itemName, int itemType, string itemDesc, Decimal itemCost, Double itemWeight);

//         //3. Change Carried
//         allCarried[itemNum].Name = itemName;
//         allCarried[itemNum].ItemType = itemType;
//         allCarried[itemNum].Descr = itemDesc;
//         allCarried[itemNum].Cost = itemCost;
//         allCarried[itemNum].Weight = itemWeight;
//         //allCarried.Add(newCarry);

//         string jsonString = JsonSerializer.Serialize(allCarried);
//         //SaveCarried(jsonString);
//         //4. Write to file
//         File.WriteAllText(filePathIC, jsonString);
//     }

// }

// public void RemoveCarried(int carriedIndexToRemove)
// {
//     List<ProdDetails> allcarry= GetAllCarried();
//     //Loop through all carried items and remove the cancelled item
//     // for(int i = 0; i < allcarry.Count; i++)
//     // { 
//     //     if(allcarry[i].APN == carriedToRemove)
//     //     {allcarry.RemoveAt(i);}
//     // }
//     allcarry.RemoveAt(carriedIndexToRemove);

//     string jsonString = JsonSerializer.Serialize(allcarry);
//     File.WriteAllText(filePathIC, jsonString);
// }

// //=======================<<<<   LINE ITEMS   >>>>===========================\\

// private string filePathLIL = "../DL/LineItemList.json";

// //AddLineItem
// public List<LineItems> GetAllLineItem()
// {
//     string jsonString = File.ReadAllText(filePathLIL);
//     return JsonSerializer.Deserialize<List<LineItems>>(jsonString) ?? new List<LineItems>(); 
// }

// public void AddLineItem(LineItems newLI)//(int id, string name, int qty, Decimal costPerItem, Decimal salesTax)
// {

//     List<LineItems> allLineItem = GetAllLineItem();
//     allLineItem.Add(newLI);

//     string jsonString = JsonSerializer.Serialize(allLineItem);
//     File.WriteAllText(filePathLIL, jsonString);

// }

// public void RemoveLineItem(int lineItemIndexToRemove)
// {
//     List<LineItems> allLI= GetAllLineItem();
//     allLI.RemoveAt(lineItemIndexToRemove);

//     string jsonString = JsonSerializer.Serialize(allLI);
//     File.WriteAllText(filePathLIL, jsonString);
// }

// //=======================<<<<   ORDERS   >>>>===========================\\

// private string filePathOrd = "../DL/Orders.json";

// //AddOrder
// public List<Orders> GetAllOrders()
// {
//     string jsonString = File.ReadAllText(filePathOrd);
//     return JsonSerializer.Deserialize<List<Orders>>(jsonString) ?? new List<Orders>(); 
// }

// //Add a new order to order history
// public void AddOrder(Orders orderItems)
// {
//     //(int orderId, int customerId, int storeId, DateOnly dateOfPurchase, int totalQty, ProdDetails orderItems)
//     int lilNumbAssg = 0;

//     //1. Grab all customers
//     List<Orders> allOrder = GetAllOrders();
//     lilNumbAssg = allOrder.Count; //Get next customer number

//     //2. Set new customer data
//     // Orders newLI = new Orders {
//     //     OrderId = orderId,    
//     //     CustomerId = customerId,
//     //     StoreId = storeId,
//     //     DateOfPurchase = dateOfPurchase,
//     //     TotalQty = totalQty,
//     //     OrderItems = orderItems
//     // };

//     //3. Append Order 
//     allOrder.Add(orderItems);

//     //4. Write to file
//     string jsonString = JsonSerializer.Serialize(allOrder);
//     File.WriteAllText(filePathOrd, jsonString);
    

// }



// }//End File Repo Class
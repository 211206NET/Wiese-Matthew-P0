
namespace UI;
public class Cart : IMenuUser {

private IBL _bl;
public Cart(IBL bl)
{
    _bl = bl;
}

//Because the current store the user is in is not transferred to the cart, this will be returned as the first store that 
//comes up while searching orders for the current customer
//public int chosenStore = 0; //Which store ID the user currently has chosen //Moved to start
//public int userId = 0; //Now passed from main menu

public void Start(int userId)//int storeId, int userId) 
{
/*            
OrderId = int.Parse(userId.ToString() + allOrders.Count.ToString()),   //Order Id is user Id concat with Order list count 
CustomerId = userId,
StoreId = chosenStore,
OrderDate = dateOfPurchase,//.ToString(),
lineQty = lineQty,
TotalCost = totalCostAfterTax,*/
bool exit = false; //Bool to control continuance of main while loop

//Information that will go into an order:
int ordId = 0; //Keep id associated with current order, these are unique to customer/store pair //<>
int ordIndex = 0; //For repo access
//Customer Id is userId parameter in Start()
//DateOnly dateOfPurchase = DateOnly.FromDateTime(DateTime.Now); //Date of purchase //<>
DateTime dateOfPurchase = DateTime.Now; //Date of purchase (SQL complains too much to use dateonly) //<>
int lineQty = 0; //Keep total quantity of all items in current line item
int tallyQty = 0; //Total Qty for all order
decimal cost = 0; //Keep cost data  //<>
decimal totalCostBeforeTax = 0; //Keep total cost data  
decimal totalCostAfterTax = 0; //Keep total cost data with tax

//Information from Inventory Object
int invID = 0; //Inventory ID for each inventory object, not a single object, not consistant
int intAPN = -1; //Default as -1 as an APN might be 0 //<>
int chosenStore = 0; //Which store ID the user currently has chosen //<>
int qty = 0; //Quantity of item at the current store //<>

//Information from Carried Objects
string? name = ""; //<>
double weight = 0; //<>

//Information from Store Object
string? storeName = ""; //<>
decimal salesTax = 0; //<>

//List of all models (except customer as we use userId parameter which is our pivoting data point)
List<Store> allStores = _bl.GetAllStores();  //Just learned about best practice for camelCaps vs PascalCaps, too late now (actually I didn't learn it)
List<Inventory> allInv = _bl.GetAllInventory();
List<Orders> allOrders = _bl.GetAllOrders();
List<ProdDetails> allCarried = _bl.GetAllCarried();
List<LineItems> lineItemsList = _bl.GetAllLineItem();

//Obsolete approaches
//List<Customers> allCustomers = _bl.GetAllCustomers(); //Don't think we need here
//int invIndex = -1; //Return index of inventory associated with currently shopped store, stores have multiple inventory objs now
// for(int i = 0; i < allInv.Count; i++)
// {if(allInv[i].Store == chosenStore){invIndex = i;}} //Stores have multiple inventory items now


while(!exit)//Cart mode is active = !exit, turns true to return to store menu or complete purchase
{

//Retrieve Order ID
//foreach(Orders ords in allOrders){if(ords.CustomerId == userId){ordId = ords.OrderId;}}  //changed to for, need index
for(int i = 0; i< allOrders.Count; i++)
{
    if(allOrders[i].CustomerId == userId && allOrders[i].OrderCompleted == 0 && ordId == 0)
    {ordId = allOrders[i].OrderId; ordIndex = i;}
}

//Reset tally stats (maybe redundant)
intAPN = -1;
lineQty = 0;
tallyQty = 0;
totalCostBeforeTax = 0;
totalCostAfterTax = 0;
lineItemsList = _bl.GetAllLineItem();
//List<LineItems> lineOrderList = new List<LineItems>(); //Obsolete, we are not sending a list to SQL

Console.WriteLine("Your cart:"); //Show contents of shopper's cart
foreach(LineItems lL in _bl.GetAllLineItem()) //Cycle through all lien items
{
    if(lL.PastOrder == false && lL.OrderId == ordId){
    lineQty = lL.Qty;
    //if(lL.PastOrder == false){

    //if(lL.OrderId == ordId){lineQty = lL.Qty;} //Set the quantity for how much is ordered of this item

    //Console.WriteLine($"lineQty: {lineQty}, lL.Qty: {lL.Qty}"); //DEBUG
    //First retrive the item ID from the corresponding 1 to 1 PK/FK inventory object
    foreach(Inventory invLI in allInv)
    {
        if(invLI.Id == lL.InvId)//Current line item matches a result in the inventory list
        {
            invID = invLI.Id;
            intAPN = invLI.Item;
            chosenStore = invLI.Store;
            qty = invLI.Qty;
        } 
    }

    //Go ahead and grab sales tax from store object
    foreach(Store storo in allStores)
    {
        if(chosenStore == storo.StoreID)//Current Lien item matches a result in the inventory list
        {
            storeName = storo.StoreName;
            salesTax = storo.SalesTax/100;
        } 
    }

    //Get information from product details about item    
    if(intAPN > -1)
    {                                                  
        foreach(ProdDetails prodD in allCarried)
        {
            if(prodD.APN == intAPN)//Display list of lien items
            {
                //inv2.ShowDesc(); //This should come from BL...?
                Console.WriteLine($"\n//------------------< Item >------------------\\\\\nCost: {prodD.Cost}, APN: [{prodD.APN}],"+
                $" Product: {prodD.Name}, Weight: {prodD.Weight}"+
                $"\nDescription: {prodD.Descr}, Quantity left: {qty}\n"); 
                name = prodD.Name ?? "";  weight = prodD.Weight;
                cost = prodD.Cost; 
            }
        }//End Loop

        // if(lL.OrderId == ordId)// && lL.StoreId == chosenStore //Redundant
        // { 
        //apn  / item name   /  qty  /   individual item cost   /    total cost for line
        //DEBUG LINE
        //Console.WriteLine($"APN [{lL.Id}], Name: [{lL.Name}], Qty: [{lL.Qty}], Cost: [{lL.CostPerItem}], Total Line Cost: [{lL.CostPerItem*lL.Qty}]" );
   
        //Establish the total cost and after tax
        //Console.WriteLine($"tallyQty: {tallyQty}, totalCostBeforeTax: {totalCostBeforeTax}, salesTax {salesTax}, cost: {cost}, lineQty: {lineQty}");
        if(lineQty > 0){
            totalCostBeforeTax += cost*lineQty;
            totalCostAfterTax = totalCostBeforeTax+(totalCostBeforeTax*salesTax);
            tallyQty += lineQty;
        }
        //Console.WriteLine($"tallyQty: {tallyQty}"); //DEBUG
        //lineOrderList.Add(lL); //Add item to local list for storing in Order object at the end, json contains all customer lineitems//OBSOLETE
    
        /*Here I must remove the purchased items from inventory.. But put back in if removed from list... 
        So multiple customers don't try to order same thing, not a perfect solution though... [UNDER CONSTRUCTION]*/
        //Re-get item index every time as list shrinks and doesn't match old index numbers
        // for(int i = 0; i < allInv.Count; i++)
        // {if(allInv[i].Id == lL.InvId){invIndex = i;}} //Old attempt

        //Console.WriteLine($"allInv.Count: {allInv.Count}"); //DEBUGGING
        //Console.WriteLine($"allInv.Qty: {qty}");//Items list shows 0 qty even thought I just opened it to buy //DEBUGGING
        int beforeQty = qty;//Store Qty before so we can make the change method adjustment

        //Console.WriteLine($"invIndex: {invIndex}, lL.InvId: {lL.InvId}"); //OLD DEBUGGING
        //DISABLED [UNDER CONSTRUCTION]
        //if(allInv.Count>=invIndex+1){_bl.ChangeInventory(invIndex,lL.InvId,beforeQty-lL.Qty);}
        //allInv = _bl.GetAllInventory();

        //int invId, int qtyToChange: Do math before calling function, value will overwrite!
        int qtyToChange = beforeQty-lineQty;
        if(qtyToChange > -1){
        _bl.ChangeInventory(invID,qtyToChange);
        //Console.WriteLine("Inventory items moved to order"); //DEBUGGING
        }else{
        Console.WriteLine("Attempting to generate order in cart but insufficient inventory!");}
      //} //End lL.OrderId == ordId check, defunct

    }//End check intAPN > -1
    else{Console.WriteLine("Error: No matching item found in inventory for selected lineitem.");}
    }//End check if Line item is ordered past
}//End for each line item loop

Console.WriteLine($"\nTotal items: {tallyQty}, total cost before tax: {totalCostBeforeTax}, total cost after tax: {totalCostAfterTax}");
Console.WriteLine("\n[0] Remove item from cart.");
Console.WriteLine("[1] Return to shopping.");
Console.WriteLine("[2] Complete purchase.\n");

string choose = Console.ReadLine() ?? "";
switch(choose)
{
    case "0":
        //Select an item to remove [will remove all qty]
        Console.WriteLine("Enter the APN number of the item you want to remove \n[Removes all quantity of the item from cart]");
        string tryPar = Console.ReadLine() ?? ""; int a; bool res; //input validation present!
        res = Int32.TryParse(tryPar, out a);
        if(res && lineItemsList.Count > 0)
        {
            int chsDlyInt = Int32.Parse(tryPar);
            Console.WriteLine($"You chose to remove APN: {chsDlyInt}");

            //Get specific Inventory ID for this line item
            int invItemId = -1; int invIndex = 0;
            for(int i = 0; i < allInv.Count; i++)
            {
                if(allInv[i].Item == chsDlyInt){invItemId = allInv[i].Id; invIndex = i;}
            }

            //Remove APN chsDlyInt from line items
            for(int i = 0; i < lineItemsList.Count; i++)
            {
                if(lineItemsList.Count > 0){ //Add back the quantity to inventory and remove the line item
                if(lineItemsList[i].PastOrder == false){
                if(lineItemsList[i].InvId == invItemId)
                { 
                    //Console.WriteLine($"Removing here!!!!!!!!!!!!!!!!"); 
                    allInv[invIndex].Qty += lineItemsList[i].Qty; _bl.RemoveLineItem(invItemId);
                }
                else{Console.WriteLine($"Error, total line items: {lineItemsList.Count}");}}}

                //It's possible a manager could remove an item from inventory right before a customer 
                //goes to remove same item from order, this would result in a fatal error.
                //To fully solidify this, there would need to be a check if the item exist still, if not remake it
            }
        }
        else{Console.WriteLine("Wrong input");}

    break;
    case "1":
        //Return to main shopping menu
        exit = true;
    break;
    case "2":
        //Confirm purchase, check out
        Console.WriteLine("Thank you for your purchase!");
        //Payment processesing here

        //________________<> Make Order Object <>________________\\
        //Dictionary<int, List<Orders>> allOrdersDict = new Dictionary<int,List<Orders>>();//This will need to be declared higher up to add in orders
        //foreach(KeyValuePair<int, List<Orders>> kv in allOrdersDict){

        //Finalize Line Items
        foreach(LineItems finishLI in lineItemsList)
        {
            if(finishLI.OrderId == ordId)
            {
                LineItems saveLI = new LineItems {
                    PastOrder = true,   
                    OrderId = ordId
                };
                _bl.FinalizeLineItem(saveLI);
            }
        }

        //Finalize order object before deleting line item objects
        Orders newLI = new Orders {
            OrderId = ordId,   //Order Id is user Id concat with Order list count 
            OrderDate = DateTime.Now,//.ToString(),
            TotalQty = tallyQty,
            TotalCost = totalCostAfterTax,
            OrderCompleted = 1
            //OrderItems = lineOrderList //I don't think we send this to SQL...
        };

        _bl.FinalizeOrder(ordIndex, newLI);
        Log.Information("Order ID: {0} has been generated by {1}",ordId,userId);

        // List<Orders> allOrders = _bl.GetAllOrders();
        // Orders newLI = new Orders {
        //     OrderId = int.Parse(userId.ToString() + allOrders.Count.ToString()),   //Order Id is user Id concat with Order list count 
        //     CustomerId = userId,
        //     StoreId = chosenStore,
        //     OrderDate = dateOfPurchase,//.ToString(),
        //     lineQty = lineQty,
        //     TotalCost = totalCostAfterTax,
        //     //OrderItems = lineOrderList //I don't think we send this to SQL...
        // };
        //I have already made an order object though...

        //Console.WriteLine("Order Data Ready!");
        // _bl.AddOrder(newLI);
        
        //}//End dictionary attempt
        //Console.WriteLine("Order Creation Complete!");
        //______________<> End Make Order Object <>______________\\

        //Disabled this as perhaps I want to keep line items in memory to reference for order lookups
        //LineItems list cleared
        // int toDelete = 0; //What line items to delete
        // for(int i = lineItemsList.Count-1; i > -1; i--)//Delete from botton to prevent list shifting from messing up deletion sequence
        // {
        //     if(lineItemsList[i].OrderId == ordId){ //lineItemsList[i].CustomerId == userId && 
        //         //Console.WriteLine($"Index: {i}, lineItemsList.Count: {lineItemsList.Count}");//DEBUG
        //         toDelete = lineItemsList[i].InvId;
        //         if(lineItemsList.Count>0){_bl.RemoveLineItem(toDelete);}
        //         lineItemsList = _bl.GetAllLineItem();
        //     }
        // }
        
        exit = true;

    break;
    default:
            Console.WriteLine("Wrong input");
    break;
}//End Main Switch






}//End While
}//End of Start
}//End Of Class
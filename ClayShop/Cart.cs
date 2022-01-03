
namespace UI;
public class Cart {

private IBL _bl;
public Cart(IBL bl)
{
    _bl = bl;
}

public int chosenStore = 0; //Which store ID the user currently has chosen
public int userId = 0;

public void Start() 
{

bool exit = false;
int totalQty = 0;
decimal totalCostBeforeTax = 0;
decimal totalCostAfterTax = 0;
List<Store> allStores = _bl.GetAllStores();  //Just learned about best practice for camelCaps vs PascalCaps, too late now
List<Customers> allCustomers = _bl.GetAllCustomers(); //Don't think we need here
int invIndex = -1; //Return index of inventory associated with currently shopped store
List<Inventory> allInv = _bl.GetAllInventory();
for(int i = 0; i < allInv.Count; i++)
{if(allInv[i].Store == chosenStore){invIndex = i;}}

DateOnly dateOfPurchase = DateOnly.FromDateTime(DateTime.Now); //Date of purchase
Console.WriteLine($"dateOnlyVar: {dateOfPurchase}");

while(!exit)
{

//Reset tally stats
totalQty = 0;
totalCostBeforeTax = 0;
totalCostAfterTax = 0;
List<LineItems> lineItemsList = _bl.GetAllLineItem();
List<LineItems> lineOrderList = new List<LineItems>();
Console.WriteLine("Your cart:");
//Show contents of shopper's cart
foreach(LineItems lL in _bl.GetAllLineItem())
{
    if(lL.CustomerId == userId && lL.StoreId == chosenStore){
    //apn  / item name   /  qty  /   individual item cost   /    total cost for line
    Console.WriteLine($"APN [{lL.Id}], Name: [{lL.Name}], Qty: [{lL.Qty}], Cost: [{lL.CostPerItem}], Total Line Cost: [{lL.CostPerItem*lL.Qty}]" );
    totalQty += lL.Qty;
    totalCostBeforeTax += lL.CostPerItem*lL.Qty;
    totalCostAfterTax = totalCostBeforeTax+(totalCostBeforeTax*lL.SalesTax);
    lineOrderList.Add(lL); //Add item to local list for storing in Order object at the end, json contains all customer lineitems
    
    /*Here I must remove the purchased items from inventory.. But put back in if removed from list... 
    So multiple customers don't try to order same thing, not a perfect solution though... [UNDER CONSTRUCTION]*/
        //Re-get item index every time as list shrinks and doesn't match old index numbers
        for(int i = 0; i < allInv[invIndex].Items.Count; i++)
        {if(allInv[invIndex].Items[i].Name == lL.Name){lL.InvId = i;}}

        Console.WriteLine($"allInv[invIndex].Items.Count: {allInv.Count}");
        Console.WriteLine($"allInv[invIndex]: {allInv[invIndex].Items[lL.InvId].OnHand}");//Items list shows 0 qty even thought I just opened it to buy
        int beforeQty = allInv[invIndex].Items[lL.InvId].OnHand;

        Console.WriteLine($"invIndex: {invIndex}, lL.InvId: {lL.InvId}");
        if(allInv.Count>=invIndex+1){_bl.ChangeInventory(invIndex,lL.InvId,beforeQty-lL.Qty);}
        allInv = _bl.GetAllInventory();
        Console.WriteLine("Inventory Deleted");
    }

}
Console.WriteLine($"\nTotal items: {totalQty}, total cost before tax: {totalCostBeforeTax}, total cost after tax: {totalCostAfterTax}");

Console.WriteLine("\n[0] Remove item from cart.");
Console.WriteLine("[1] Return to shopping.");
Console.WriteLine("[2] Complete purchase.\n");

string choose = Console.ReadLine() ?? "";
switch(choose)
{
    case "0":
        //Select an item to remove [will remove all qty]
        Console.WriteLine("Enter the APN number of the item you want to remove \n[Removes all quantity of the item from cart]");
        string tryPar = Console.ReadLine() ?? ""; int a; bool res;
        res = Int32.TryParse(tryPar, out a);
        if(res && lineItemsList.Count > 0)
        {
            int chsDlyInt = Int32.Parse(tryPar);
            Console.WriteLine($"You chose to remove APN: {chsDlyInt}");
            //Remove APN chsDlyInt from line items
            for(int i = 0; i < lineItemsList.Count; i++)
            {
                if(lineItemsList.Count > 0){
                if(lineItemsList[i].Id == chsDlyInt){_bl.RemoveLineItem(i);}}   
                /*Here I must add the removed items back to inventory [UNDER CONSTRUCTION]*/
                //But this below condition checks that the idex isn't null, not that the item is contained within the list or not.....
                //Solution is we never delete items from inventory, just set to 0
                //if(allInv[invIndex].Items[lineItemsList[i].InvId] != null){//Check that this item exists in a list
                int beforeQty = allInv[invIndex].Items[lineItemsList[i].InvId].OnHand;
                if(allInv.Count>0){_bl.ChangeInventory(invIndex,lineItemsList[i].InvId,beforeQty+lineItemsList[i].Qty);}
                allInv = _bl.GetAllInventory();
                //}
                //else
                //{
                    // ProdDetails addStock = new ProdDetails {
                    //     //StoreAt = allStores[chosenStoreIndex].StoreID, 
                    //     OnHand = choiceInt2, 
                    //     APN = getAllCarried[choiceInt].APN,
                    //     Name = getAllCarried[choiceInt].Name,
                    //     ItemType = getAllCarried[choiceInt].ItemType,
                    //     Desc = getAllCarried[choiceInt].Desc,
                    //     Cost = getAllCarried[choiceInt].Cost,
                    //     Weight = getAllCarried[choiceInt].Weight
                    // };

                    // //allInventory[targetInv].Items.Add(addStock);  //DO I NEED THIS EVEN?
                    // //Now to Save it
                    // _bl.AddItem(targetInv, addStock);
                //}
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
        
        List<Orders> allOrders = _bl.GetAllOrders();
        Orders newLI = new Orders {
            OrderId = int.Parse(userId.ToString() + allOrders.Count.ToString()),   //Order Id is user Id concat with Order list count 
            CustomerId = userId,
            StoreId = chosenStore,
            DateOfPurchase = dateOfPurchase.ToString(),
            TotalQty = totalQty,
            TotalCost = totalCostAfterTax,
            OrderItems = lineOrderList
        };

        Console.WriteLine("Order Data Ready!");
        _bl.AddOrder(newLI);
        Console.WriteLine("Order Creation Complete!");
        //______________<> End Make Order Object <>______________\\

        //LineItems list cleared
        for(int i = lineItemsList.Count-1; i > -1; i--)//Delete from botton to prevent list shifting from messing up deletion sequence
        {
            if(lineItemsList[i].CustomerId == userId && lineItemsList[i].InvId == chosenStore){
                Console.WriteLine($"Index: {i}, lineItemsList.Count: {lineItemsList.Count}");
                if(lineItemsList.Count>0){_bl.RemoveLineItem(i);}
                lineItemsList = _bl.GetAllLineItem();
            }
        }
        

        exit = true;

    break;
    default:
            Console.WriteLine("Wrong input");
    break;
}//End Main Switch




}//End While




}//End of main class and start
}
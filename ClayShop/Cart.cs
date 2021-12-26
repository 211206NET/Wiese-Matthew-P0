
namespace UI;
public class Cart {

private IBL _bl;
public Cart(IBL bl)
{
    _bl = bl;
}

public int chosenStore = 0; //Which store the user currently has chosen


public void Start() 
{

bool exit = false;
int totalQty = 0;
decimal totalCostBeforeTax = 0;
decimal totalCostAfterTax = 0;
List<Store> allStores = _bl.GetAllStores();  //Just learned about best practice for camelCaps vs PascalCaps, too late now
List<Customers> allCustomers = _bl.GetAllCustomers();

DateOnly dateOfPurchase = DateOnly.FromDateTime(DateTime.Now); //Date of purchase
Console.WriteLine($"dateOnlyVar: {dateOfPurchase}");

while(!exit)
{

//Reset tally stats
totalQty = 0;
totalCostBeforeTax = 0;
totalCostAfterTax = 0;
List<LineItems> lineItemsList = _bl.GetAllLineItem();
Console.WriteLine("Your cart:");
//Show contents of shopper's cart
foreach(LineItems lL in _bl.GetAllLineItem())
{
    //apn  / item name   /  qty  /   individual item cost   /    total cost for line
    Console.WriteLine($"APN [{lL.APN}], Name: [{lL.Name}], Qty: [{lL.Qty}], Cost: [{lL.CostPerItem}], Total Line Cost: [{lL.CostPerItem*lL.Qty}]" );
    totalQty += lL.Qty;
    totalCostBeforeTax += lL.CostPerItem*lL.Qty;
    totalCostAfterTax = totalCostBeforeTax+(totalCostBeforeTax*lL.SalesTax);
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
                if(lineItemsList[i].APN == chsDlyInt){_bl.RemoveLineItem(i);}}
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

        //LineItems list cleared
        for(int i = 0; i < lineItemsList.Count-1; i++)
        {
            if(lineItemsList.Count>0){_bl.RemoveLineItem(i);}
            //This does not account for multiple users orders the same limited qty, a reseve stock would need to be added
            //Remove items from inventory
        }
        exit = true;
        Console.WriteLine("Here I must remove the purchased items from inventory.. [UNDER CONSTRUCTION]");
    break;
    default:
            Console.WriteLine("Wrong input");
    break;
}//End Main Switch




}//End While




}//End of main class and start
}
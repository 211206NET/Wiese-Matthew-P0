

namespace UI;

//This class is for adding/removing items from carried list and stocking stores based on items on carried list
public class InventoryCtrl : IMenu
{

    private IBL _bl;
    public InventoryCtrl(IBL bl)
    {
        _bl = bl;
    }

    bool exit = false;

    public void Start()
    {
        while(!exit)
        {
            //Management Menu
            Console.WriteLine("Select an action:");
            Console.WriteLine("[0] Add item to list of carried items:");//
            Console.WriteLine("[1] Remove item from list of carried items and purge from all stores:");//
            Console.WriteLine("[2] Make changes to item on carried items list:");//
            Console.WriteLine("[x] Return to management menu:");//

            string choose = Console.ReadLine() ?? "";
            switch(choose)
            {
                //Add item to list of carried items:
                case "0":
                    List<ProdDetails> getAllCarried = _bl.GetAllCarried(); //Needs to be in each case ir won't update in while loop

                    //Get user input
                    Console.WriteLine("Enter an item name:");
                    string itemName = Console.ReadLine() ?? "";
                    Console.WriteLine("Enter what type of item it is [0 = clay, 1 = tools, 2 = equip]:");
                    int itemType = Int32.Parse(Console.ReadLine() ?? "");
                    Console.WriteLine("Enter a description of the item:");
                    string itemDesc = Console.ReadLine() ?? "";
                    Console.WriteLine("Enter the cost of the item");
                    Decimal itemCost = Convert.ToDecimal(Int32.Parse(Console.ReadLine() ?? ""));
                    Console.WriteLine("Enter the weight of the item");
                    Double itemWeight = Convert.ToDouble(Int32.Parse(Console.ReadLine() ?? ""));

                    int carrNumbAssg = 0;
                    //Get unique APN for new item
                    foreach(ProdDetails newAPN in getAllCarried)
                    {
                        carrNumbAssg = 100+getAllCarried.Count; //Get next customer number
                        if(newAPN.APN == carrNumbAssg){carrNumbAssg++;}
                    }

                    //Add user data to new carried items list
                    ProdDetails itemNew = new ProdDetails
                    {
                        APN = carrNumbAssg,
                        Name = itemName ?? "",
                        ItemType = itemType,
                        Weight = itemWeight,
                        Cost = itemCost,
                        Descr = itemDesc ?? ""
                    };

                    _bl.AddCarried(itemNew);
                    //_bl.AddCarried(0,itemName ?? "",itemType,itemDesc ?? "",itemCost,itemWeight);

                    Console.WriteLine($"[{itemName}] successfully created and added to the list of carried items for this franchise.\n");
                break;
                
                //Remove item from list of carried items:
                case "1":
                    List<ProdDetails> allCarriedDelete = _bl.GetAllCarried();

                    //Show list of carried items
                    string itemTypeStrD; itemTypeStrD = "";
                    //Show list of all carried items, manager can select which one to modify
                    for(int i = 0; i < allCarriedDelete.Count; i++)  
                    {
                        if(allCarriedDelete[i].ItemType == 0){itemTypeStrD = "Clay";}
                        if(allCarriedDelete[i].ItemType == 1){itemTypeStrD = "Tool";}
                        if(allCarriedDelete[i].ItemType == 2){itemTypeStrD = "Equipment";}
                        Console.WriteLine($"\n[{i}], APN: {allCarriedDelete[i].APN}, "+
                        $"Item Name: {allCarriedDelete[i].Name}, "+
                        $"Item Type: {itemTypeStrD}, "+
                        $"Item Cost: {allCarriedDelete[i].Cost}, "+
                        $"Item Weight: {allCarriedDelete[i].Weight}, \n"+
                        $"\tItem Description: {allCarriedDelete[i].Descr}");
                    }

                    Console.WriteLine("Choose from the above items in the carried list to permanently delete.\n"+
                    "Note: This will remove this item from all store inventories.");
                    string? chooseDlt = Console.ReadLine();
                    bool resD; int aD;
                    resD = Int32.TryParse(chooseDlt, out aD);
                    if(resD)
                    {
                        int chsDlyInt = Int32.Parse(chooseDlt ?? "");
                        //Ask manager if they are sure they want to remove this carried item
                        Console.WriteLine($"Are you sure you want to remove the item: {allCarriedDelete[chsDlyInt].Name}? [y,n]");
                        string decideD = Console.ReadLine() ?? "";
                        if(decideD == "y")
                        {
                            //If this item is currently being purchased, the manager must wait before deleting it
                            List<LineItems> allLineItems = _bl.GetAllLineItem(); //Get a list of the inventory
                            List<Inventory> allInventoryDelete = _bl.GetAllInventory(); //Get a list of the inventory
                            //List<Orders> allOrders = _bl.GetAllOrders(); //Get a list of the inventory
                            bool abort = false;
                            foreach(LineItems li in allLineItems)
                            {
                                foreach(Inventory allInv in allInventoryDelete)
                                {
                                    if(allInv.Id == li.InvId)//Find APN of any current line items
                                    {
                                        if(allInv.Item == allCarriedDelete[chsDlyInt].APN)
                                        {
                                            abort = true; //This item is currently being purchased, you can't remove it right now you maniac!
                                        }
                                    }
                                }      
                            }

                            // Console.WriteLine($"The item: {allCarriedDelete[chsDlyInt].Name} has been removed."+
                            // "\nIt has been purged from all store inventories.\n");
                            //A nested loop is used here to delete all of this item from the inventory of all stores
                            if(!abort){

                                //Remove the inventory rows 
                                foreach(Inventory allInv2 in allInventoryDelete)
                                {
                                    if(allInv2.Item == allCarriedDelete[chsDlyInt].APN) //Find the same item in the inventory list
                                    {
                                        _bl.RemoveInventory(allInv2.Id); //Delete the item from store inventories as well
                                    }
                                }

                                //Remove the carried row
                                for(int i = 0; i < allInventoryDelete.Count; i++)
                                {
                                    if(allInventoryDelete[i].Item == allCarriedDelete[chsDlyInt].APN) //Find the same item in the inventory list
                                    {
                                        _bl.RemoveItem(allCarriedDelete[chsDlyInt].APN); //Delete the item from store inventories as well
                                    }
                                }
                            }
                            
                           // _bl.RemoveCarried(chsDlyInt); //Removes from carried list, delete last as previous delete references this list
                        }
                    }
                break;

                //Make changes to item on carried items list:
                case "2":
                    List<ProdDetails> getAllCarried2 = _bl.GetAllCarried(); //Needs to be in each case ir won't update in while loop

                    string itemTypeStr; itemTypeStr = "";
                    //Show list of all carried items, manager can select which one to modify
                    for(int i = 0; i < getAllCarried2.Count; i++)  
                    {
                        if(getAllCarried2[i].ItemType == 0){itemTypeStr = "Clay";}
                        if(getAllCarried2[i].ItemType == 1){itemTypeStr = "Tool";}
                        if(getAllCarried2[i].ItemType == 2){itemTypeStr = "Equipment";}
                        Console.WriteLine($"\n[{i}], APN: {getAllCarried2[i].APN}, "+
                        $"Item Name: {getAllCarried2[i].Name}, "+
                        $"Item Type: {itemTypeStr}, "+
                        $"Item Cost: {getAllCarried2[i].Cost}, "+
                        $"Item Weight: {getAllCarried2[i].Weight}, \n"+
                        $"\tItem Description: {getAllCarried2[i].Descr}");
                    }

                    //Selecting item to change from list code
                    //Ask manager to choose entry to modify
                    string change = ""; int changeInt = 0; decimal changeDec = 0; double changeDoub = 0; //Vars to record manager changes
                    Console.WriteLine("\nSelect which number you want to modify.");
                    int selChangeCarried = Int32.Parse(Console.ReadLine() ?? "");
                    //Show manager what item was selected
                    Console.WriteLine($"\nYou chose: {getAllCarried2[selChangeCarried].Name} "+
                    "\nEnter a new value for each field or leave blank to keep the same.\n");

                    //Storing default values of selected item code (so no blank overwrite when hitting space below)
                    string itemNameC = getAllCarried2[selChangeCarried].Name ?? "";
                    int itemTypeC = getAllCarried2[selChangeCarried].ItemType;
                    decimal itemCostC = getAllCarried2[selChangeCarried].Cost;
                    string itemDescC = getAllCarried2[selChangeCarried].Descr ?? "";
                    double itemWeightC = getAllCarried2[selChangeCarried].Weight;

                    //Changing value code 
                    //Offer to change name
                    Console.WriteLine($"\nChange name: {getAllCarried2[selChangeCarried].Name}?");
                    change = Console.ReadLine() ?? "";
                    if(change != ""){itemNameC = change;} change = ""; //Change name  getAllCarried2[selChangeCarried].Name
                    
                    //Offer to change type
                    Console.WriteLine($"\nChange type: {getAllCarried2[selChangeCarried].ItemType}? [0 = clay, 1 = tools, 2 = equip]:");
                    change = Console.ReadLine() ?? "";
                    bool res; int a;
                    res = Int32.TryParse(change, out a);
                    if(res){
                        changeInt = Int32.Parse(change);
                        if(changeInt != 0){itemTypeC = changeInt;} //Change type getAllCarried2[selChangeCarried].ItemType 
                    } changeInt = 0; change = ""; 
                    
                    //Offer to change cost
                    Console.WriteLine($"\nChange cost: {getAllCarried2[selChangeCarried].Cost}?");
                    change = Console.ReadLine() ?? "";
                    res = Int32.TryParse(change, out a);
                    if(res){
                        changeDec = Convert.ToDecimal(Int32.Parse(change));
                        if(changeDec != 0){itemCostC = changeDec;} //Change type  getAllCarried2[selChangeCarried].Cost
                    } changeDec = 0; change = ""; 
                    
                    //Offer to change weight
                    Console.WriteLine($"\nChange weight: {getAllCarried2[selChangeCarried].Weight}?");
                    change = Console.ReadLine() ?? "";
                    res = Int32.TryParse(change, out a);
                    if(res){
                        changeDoub = Convert.ToDouble(Int32.Parse(change));
                        if(changeDoub != 0){itemWeightC = changeDoub;} //Change type  getAllCarried2[selChangeCarried].Cost
                    } changeDoub = 0; change = ""; 
                    
                    //Offer to change description
                    Console.WriteLine($"\nChange description: {getAllCarried2[selChangeCarried].Descr}?");
                    change = Console.ReadLine() ?? "";
                    if(change != ""){itemDescC = change;} change = ""; //Change description  getAllCarried2[selChangeCarried].Desc

                    //Console.WriteLine($"itemCostC: {itemCostC}");
                    ProdDetails changeCarry = new ProdDetails {
                        APN = getAllCarried2[selChangeCarried].APN,
                        Name = itemNameC,
                        ItemType = itemTypeC,
                        Descr = itemDescC,
                        Cost = itemCostC,
                        Weight = itemWeightC
                    };
                    // string jsonStringC = JsonSerializer.Serialize(changeCarry);

                    _bl.ChangeCarried(changeCarry);
                    //_bl.ChangeCarried(selChangeCarried,itemNameC,itemTypeC,itemDescC,itemCostC,itemWeightC);
                    //FileRepo.SaveCarried();
                break;

                //Return to management menu:
                case "x":
                    exit = true;
                break;

                //Wrong input
                default:
                    Console.WriteLine("Wrong input, try again.");
                break;
            }


        }//End While exit loop



    }//End class


}
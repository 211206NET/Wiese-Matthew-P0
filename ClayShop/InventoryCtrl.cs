

namespace UI;

//This class is for adding/removing items from carried list and stocking stores based on items on carried list
public class InventoryCtrl
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
            Console.WriteLine("[0] Add item to list of carried items:");//Done
            Console.WriteLine("[1] Remove item from list of carried items:");
            Console.WriteLine("[2] Make changes to item on carried items list:");//Done
            Console.WriteLine("[x] Return to management menu:");//Done

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

                    //Add user data to new carried items list
                    ProdDetails itemNew = new ProdDetails
                    {
                            APN = 0,
                            Name = itemName ?? "",
                            ItemType = itemType,
                            Desc = itemDesc ?? "",
                            Cost = itemCost,
                            Weight = itemWeight
                    };

                    //_bl.AddCarried(itemNew);
                    _bl.AddCarried(0,itemName ?? "",itemType,itemDesc ?? "",itemCost,itemWeight);

                    Console.WriteLine($"[{itemName}] successfully created and added to the list of carried items for this franchise.\n");
                break;
                
                //Remove item from list of carried items:
                case "1":
                break;

                //Make changes to item on carried items list:
                case "2":
                    List<ProdDetails> getAllCarried2 = _bl.GetAllCarried(); //Needs to be in each case ir won't update in while loop

                    string itemNameC = "";//How do these empty values not overwrite data?
                    int itemTypeC = 0;
                    decimal itemCostC = 0;
                    string itemDescC = "";
                    double itemWeightC = 0;

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
                        $"Item Description: {getAllCarried2[i].Desc}\n");
                    }

                    //Ask manager to choose entry to modify
                    string change = ""; int changeInt = 0; decimal changeDec = 0; double changeDoub = 0; //Vars to record manager changes
                    Console.WriteLine("\nSelect which number you want to modify.");
                    int selChangeCarried = Int32.Parse(Console.ReadLine() ?? "");
                    //Show manager what item was selected
                    Console.WriteLine($"\nYou chose: {getAllCarried2[selChangeCarried].Name} "+
                    "\nEnter a new value for each field or leave blank to keep the same.\n");
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
                    Console.WriteLine($"\nChange description: {getAllCarried2[selChangeCarried].Desc}?");
                    change = Console.ReadLine() ?? "";
                    if(change != ""){itemDescC = change;} change = ""; //Change description  getAllCarried2[selChangeCarried].Desc

                    
                    ProdDetails changeCarry = new ProdDetails {
                        APN = selChangeCarried,
                        Name = itemNameC,
                        ItemType = itemTypeC,
                        Desc = itemDescC,
                        Cost = itemCostC,
                        Weight = itemWeightC
                    };
                    // string jsonStringC = JsonSerializer.Serialize(changeCarry);

                    _bl.ChangeCarried(selChangeCarried,itemNameC,itemTypeC,itemDescC,itemCostC,itemWeightC);
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
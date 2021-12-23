using DL;

namespace UI;

public class Management
{

    private IBL _bl;
    public Management(IBL bl)
    {
        _bl = bl;
    }


    public int chosenStore = 0;//StoreID
    public int chosenStoreIndex = 0;//Index of store in store list
    public void Start()
    {
        List<Store> allStores = _bl.GetAllStores();
        bool exit = false;
        int idStamp = allStores.Count+1;
        Console.WriteLine("Manage fired");
        //Main Loop
        while(!exit)
        {
            //Management Menu
            Console.WriteLine("Select an action:");
            Console.WriteLine("[0] Add store:"); //Done
            Console.WriteLine("[1] Select current store:"); //Done
            Console.WriteLine("[2] Manage company carried items:"); //Done
            Console.WriteLine($"[3] Adjust/Add store info [Current store number: {chosenStore}]:"); //Done
            Console.WriteLine($"[4] Manage store inventory [Current store number: {chosenStore}]:"); //Done
            Console.WriteLine($"[5] Delete store [Current store number: {chosenStore}]:"); //Done
            Console.WriteLine("[x] Return to main menu:"); //Done
            string choose = Console.ReadLine() ?? "";
            switch(choose)
            {
                //Add Store
                case "0":
                    //Get user input
                    Console.WriteLine("Enter a store name:");
                    string userStoreName = Console.ReadLine() ?? "";
                    Console.WriteLine("Enter the city of the store:");
                    string userCity = Console.ReadLine() ?? "";
                    Console.WriteLine("Enter the state of the store:");
                    string userState = Console.ReadLine() ?? "";
                    //Add user data to new store generation
                    Store storeNew = new Store
                    {
                        StoreID = idStamp,
                        StoreName = userStoreName,
                        City = userCity,
                        State = userState
                    };
                    //allStores.AddStore(storeNew);
                    _bl.AddStore(storeNew);
                    //allStores.Add(storeNew); //Plug new store into store list
                    Console.WriteLine($"[{idStamp}] Store: {userStoreName} successfully created!\n");
                    //chosenStore = idStamp; //Set current store to store just made
                    idStamp++; //Add to idStamp for next store
                break;

                //Select Current Store (Manager can change stores from either menu and it will remember from main to manage):
                case "1":
                    List<Store> allStoresQ = _bl.GetAllStores();
                    Console.WriteLine("Choose which store you want:");
                    //Cycle through all stores show a list of them
                    for(int i = 0; i < allStoresQ.Count; i++)  
                    {
                        //Console.WriteLine($"allStores.Count: {allStores.Count}");
                        Console.WriteLine($"[{i}], Store ID: {allStoresQ[i].StoreID}, "+
                        $"Store Name: {allStoresQ[i].StoreName}, "+
                        $"City: {allStoresQ[i].City}, "+
                        $"State: {allStoresQ[i].State}");
                        //allStores[i].SetUp(); //Tell store to generate inventory PLACE HOLDER
                    }

                    int select = Int32.Parse(Console.ReadLine() ?? ""); 
                    
                    // string select = Console.ReadLine(); 
                    // switch(select)
                    // {
                    //     case Char.IsNumber:
                    //     break;
                    // }
                    chosenStoreIndex = select;
                    chosenStore = allStoresQ[chosenStoreIndex].StoreID;   //User inputs a choice
                    Console.WriteLine($"allStoresQ[chosenStoreIndex].StoreID: {allStoresQ[chosenStoreIndex].StoreID}, chosenStoreIndex: {chosenStoreIndex}");

                break;

                //Manage Company Carried Items:
                case "2":
                    Console.WriteLine("Change carried items.");
                    
                    //Here, I instantiated an implementation of IRepo (FileRepo)
                    IRepo repo = new FileRepo();
                    //next, I instantiated CSBL (an implementation of IBL) and then injected IRepo implementation for IBL/CSBL
                    IBL bl = new CSBL(repo);
                    //Finally, I instantiate carrMenu that needs an instance that implements Business Logic class
                    InventoryCtrl carrMenu = new InventoryCtrl(bl);
                    //Reset local settings for when returning to this menu
                    carrMenu.Start();
                break;

                //Adjust/Add Store Info:
                case "3":
                    List<Store> allStoresA = _bl.GetAllStores();
                    Console.WriteLine("Changing Store Address etc.");

                    string storeName = allStoresA[chosenStoreIndex].StoreName ?? "";
                    string city = allStoresA[chosenStoreIndex].City ?? "";
                    string state = allStoresA[chosenStoreIndex].State ?? "";
                    //chosenStore
                    //Ask manager to choose entry to modify
                    string change = ""; //Var to record manager changes
                    //Show manager what store was selected
                    Console.WriteLine($"\nYou chose: {allStoresA[chosenStoreIndex].StoreName} "+
                    "\nEnter a new value for each field or leave blank to keep the same.\n");
                    //Offer to change name
                    Console.WriteLine($"\nChange name: {allStoresA[chosenStoreIndex].StoreName}?");
                    change = Console.ReadLine() ?? "";
                    if(change != ""){storeName = change;} change = ""; //Change name of store

                    //Offer to change city
                    Console.WriteLine($"\nChange city: {allStoresA[chosenStoreIndex].City}?");
                    change = Console.ReadLine() ?? "";
                    if(change != ""){city = change;} change = ""; //Change city

                    //Offer to change state
                    Console.WriteLine($"\nChange state: {allStoresA[chosenStoreIndex].State}?");
                    change = Console.ReadLine() ?? "";
                    if(change != ""){state = change;} change = ""; //Change state 

                    _bl.ChangeStoreInfo(chosenStoreIndex,storeName,city,state);
                    
                break;

                //Manage Store Inventory
                case "4":
                    //Get list of stores to find current store
                    List<Store> allStoresI = _bl.GetAllStores(); 
                    List<ProdDetails> allInventory = _bl.GetAllInventory();
                    //_bl.GetAllInventory();

                    //First show current inventory
                    Console.WriteLine($"Inventory for {allStoresI[chosenStoreIndex].StoreName}, Inventory count: {allInventory.Count}\n");
                    //if(allStoresI[chosenStore].localInv.Count > 0){
                    int intFor = 0;
                    if(allInventory.Count > 0){
                    //foreach(ProdDetails inv in allStoresI[chosenStore].localInv)
                    foreach(ProdDetails inv in allInventory) //To find inventory of the store we want only
                    {
                        if(inv.StoreAt == allStoresI[chosenStoreIndex].StoreID){
                        Console.WriteLine($"[{intFor}], APN: [{inv.APN}] {inv.Name}, Cost: {inv.Cost}, Weight: {inv.Weight}\n"+
                        $"\tDescription: {inv.Desc}");  intFor++;}
                    }}
                    Console.WriteLine("\nEnter a number to select item to change quantity of,\nor enter 'n' to add new product from carried list.");
                    string choice = Console.ReadLine() ?? "";
                    bool res; bool res2; bool res3; int a; int choiceInt = 0;  int choiceInt2 = 0;
                    res = Int32.TryParse(choice, out a);
                    if(res)
                    {
                        //A number was selected, choose an item to change qty
                        choiceInt = Int32.Parse(choice); //Convert string to int
                        // Console.WriteLine($"You selected: {allStoresI[chosenStoreIndex].localInv[choiceInt].Name}"+
                        // $", Quantity: {allStoresI[chosenStoreIndex].localInv[choiceInt].OnHand}\nEnter new value:");
                        Console.WriteLine($"You selected: {allInventory[choiceInt].Name}"+
                        $", Quantity: {allInventory[choiceInt].OnHand}\nEnter new value:");
                        choice = Console.ReadLine() ?? "";
                        res2 = Int32.TryParse(choice, out a);
                        if(res2)
                        {
                            allInventory[choiceInt].OnHand = Int32.Parse(choice); //Set new Qty

                            _bl.ChangeInventory(allStoresI[chosenStoreIndex].StoreID, choiceInt, Int32.Parse(choice)); //Call method to adjust Qty of item already on hand
                        }
                        else{Console.WriteLine("Not a numeric value!");}
                    }
                    else
                    {
                        //A string was selected, add new item maybe
                        if(choice == "n")
                        {
                            //Add new item to stock
                            List<ProdDetails> getAllCarried2 = _bl.GetAllCarried(); //Needs to be in each case ir won't update in while loop
                            //Add new item
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
                                $"\tItem Description: {getAllCarried2[i].Desc}");
                            }
                            Console.WriteLine("Choose which item to add from the above list.");
                            choice = Console.ReadLine() ?? "";
                            res2 = Int32.TryParse(choice, out a);
                            if(res2)
                            {   
                                choiceInt = Int32.Parse(choice); //Manager has chosen what item to add
                                bool abort = false; //Stop adding item if already added

                                //First check that this item isn't already in stock
                                //foreach(ProdDetails inv in allStoresI[chosenStore].localInv)
                                foreach(ProdDetails inv in allInventory)
                                {   
                                   Console.WriteLine($"inv.StoreAt: {inv.StoreAt}, chosenStore: {chosenStore},"+
                                   $"inv.APN: {inv.APN}, Carried APN: {getAllCarried2[choiceInt].APN}");
                                    if(inv.StoreAt == chosenStore){
                                    if(inv.APN == getAllCarried2[choiceInt].APN){abort = true;}}//This item is already in stock, abort
                                }

                                //Continue with adding item
                                if(!abort){
                                Console.WriteLine($"You chose {getAllCarried2[choiceInt].Name}, Now enter quantity for the item:");

                                choice = Console.ReadLine() ?? "";
                                res3 = Int32.TryParse(choice, out a);
                                if(res3)
                                {
                                    choiceInt2 = Int32.Parse(choice);
                                    ProdDetails addStock = new ProdDetails {
                                        StoreAt = allStoresI[chosenStoreIndex].StoreID, 
                                        OnHand = choiceInt2, 
                                        APN = getAllCarried2[choiceInt].APN,
                                        Name = getAllCarried2[choiceInt].Name,
                                        ItemType = getAllCarried2[choiceInt].ItemType,
                                        Desc = getAllCarried2[choiceInt].Desc,
                                        Cost = getAllCarried2[choiceInt].Cost,
                                        Weight = getAllCarried2[choiceInt].Weight
                                    };

                                    //Adding a new entry to list allInventory
                                    //allStoresI[chosenStore].localInv.Add(addStock);
                                    allInventory.Add(addStock);

                                    //Now to Save it
                                    _bl.AddInventory(chosenStoreIndex, addStock);
                                    abort = false; //reset
                                }}
                            }
                        }
                    }

                break;

                //Delete Store
                case "5":
                    List<Store> allStoresB = _bl.GetAllStores();
                    Console.WriteLine($"Are you sure you want to remove the store: {allStoresB[chosenStoreIndex].StoreName}? [y,n]");
                    string decide = Console.ReadLine() ?? "";
                    if(decide == "y")
                    {
                        Console.WriteLine($"The store: {allStoresB[chosenStoreIndex].StoreName} has been removed."+
                        "\nMake sure to allocate inventory where needed.\n");
                        _bl.RemoveStore(chosenStoreIndex);
                    }
                break;

                case "x":
                //Return to Main Menu:
                    exit = true;
                break;

                default:
                    Console.WriteLine("Input error, try again.");
                break;
            }
        }
    }
}
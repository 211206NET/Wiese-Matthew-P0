using DL;

namespace UI;

public class Management : IMenu
{

    private IBL _bl;
    public Management(IBL bl)
    {
        _bl = bl;
    }

    int targetInv = 0; //Index of inventory
    public int chosenStore = 0;//StoreID
    public int chosenStoreIndex = 0;//Index of store in store list
    public void Start()
    {
        List<Inventory> allInventory = _bl.GetAllInventory();
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
                    Console.WriteLine("Enter the salestax for the state the store is in:");
                    decimal userST = Convert.ToDecimal(Int32.Parse(Console.ReadLine() ?? ""));
                    //Add user data to new store generation //DISABLED because of row error

                    Store storeNew = new Store
                    {
                        StoreID = idStamp,
                        StoreName = userStoreName,
                        City = userCity,
                        State = userState,
                        SalesTax = userST
                    };

                    _bl.AddStore(storeNew);
                    allStores = _bl.GetAllStores();
                    // //Add Inventory Obj for new store generation   //Obsolete, inventory is now by item not store
                    // Inventory invNew = new Inventory
                    // {
                    //     Id = idStamp,
                    //     Store = idStamp
                    // };
                    // //allStores.AddStore(storeNew);
                    // _bl.AddInventory(invNew);
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
                    
                    // //Here, I instantiated an implementation of IRepo (FileRepo)
                    // IRepo repo = new FileRepo();
                    // //next, I instantiated CSBL (an implementation of IBL) and then injected IRepo implementation for IBL/CSBL
                    // IBL bl = new CSBL(repo);
                    // //Finally, I instantiate carrMenu that needs an instance that implements Business Logic class
                    // InventoryCtrl carrMenu = new InventoryCtrl(bl);
                    // //Reset local settings for when returning to this menu
                    // carrMenu.Start();

                    MenuFactory.GetMenu("inventory").Start();
                break;

                //Adjust/Add Store Info:
                case "3":
                    allStores = _bl.GetAllStores();
                    Console.WriteLine("Changing Store Address etc.");

                    string storeName = allStores[chosenStoreIndex].StoreName ?? "";
                    string city = allStores[chosenStoreIndex].City ?? "";
                    string state = allStores[chosenStoreIndex].State ?? "";
                    decimal salesTax = allStores[chosenStoreIndex].SalesTax;
                    //chosenStore
                    //Ask manager to choose entry to modify
                    string change = ""; //Var to record manager changes
                    //Show manager what store was selected
                    Console.WriteLine($"\nYou chose: {allStores[chosenStoreIndex].StoreName} "+
                    "\nEnter a new value for each field or leave blank to keep the same.\n");
                    //Offer to change name
                    Console.WriteLine($"\nChange name: {allStores[chosenStoreIndex].StoreName}?");
                    change = Console.ReadLine() ?? "";
                    if(change != ""){storeName = change;} change = ""; //Change name of store

                    //Offer to change city
                    Console.WriteLine($"\nChange city: {allStores[chosenStoreIndex].City}?");
                    change = Console.ReadLine() ?? "";
                    if(change != ""){city = change;} change = ""; //Change city

                    //Offer to change state
                    Console.WriteLine($"\nChange state: {allStores[chosenStoreIndex].State}?");
                    change = Console.ReadLine() ?? "";
                    if(change != ""){state = change;} change = ""; //Change state 

                    //Offer to change sales tax
                    Console.WriteLine($"\nChange sales tax: {allStores[chosenStoreIndex].SalesTax}?");
                    change = Console.ReadLine() ?? "";
                    if(change != ""){salesTax = Convert.ToDecimal(Int32.Parse(change));} change = ""; //Change sales tax 

                //Disabled due to row error
                    // Store storInf = new Store {
                    //     //StoreAt = allStores[chosenStoreIndex].StoreID, 
                    //     StoreID = chosenStoreIndex, 
                    //     StoreName = storeName,
                    //     City = city,
                    //     State = state,
                    //     SalesTax = salesTax
                    // };

                    // _bl.ChangeStoreInfo(chosenStoreIndex, storInf);
                    // _bl.ChangeStoreInfo(chosenStoreIndex,storeName,city,state);
                    
                break;

                //Manage Store Inventory
                case "4":
                    //Get list of stores to find current store
                    allStores = _bl.GetAllStores(); 
                    allInventory = _bl.GetAllInventory();
                    //_bl.GetAllInventory();

                    //First show current inventory
                    Console.WriteLine($"Inventory for {allStores[chosenStoreIndex].StoreName}, Inventory count: {allInventory.Count}\n");
                    //if(allStores[chosenStore].localInv.Count > 0){
     
                    Console.WriteLine($"allInventory.Count: {allInventory.Count}");
                    if(allInventory.Count > 0){

                    //Return what index of inventory we need to access
                    for(int i = 0; i < allInventory.Count; i++)
                    {
                        if(allInventory[i].Store == allStores[chosenStoreIndex].StoreID){targetInv = i;}
                    }

                    //foreach(ProdDetails inv in allInventory) //To find inventory of the store we want only
                    for(int i = 0; i < allInventory[targetInv].Items.Count; i++)
                    {
                        Console.WriteLine($"APN: [{allInventory[targetInv].Items[i].APN}] {allInventory[targetInv].Items[i].Name},"+
                        $" Cost: {allInventory[targetInv].Items[i].Cost}, Weight: {allInventory[targetInv].Items[i].Weight}\n"+
                        $"\tDescription: {allInventory[targetInv].Items[i].Descr}"); 
                    }}
                    Console.WriteLine("\nEnter an APN to select item to change quantity of,\nor enter 'n' to add new product from carried list.");
                    string choice = Console.ReadLine() ?? "";
                    bool res; bool res2; bool res3; int a; int choiceInt = 0;  int choiceInt2 = 0;
                    res = Int32.TryParse(choice, out a);

                    if(res)
                    {
                        //A number was selected, choose an item to change qty
                        choiceInt = Int32.Parse(choice); //Convert string to int
                    
                        //First get the actual index of the item
                        int getIndex = 0;
                        //foreach(ProdDetails inv in allInventory)
                        for(int i = 0; i < allInventory[targetInv].Items.Count; i++) //-1 needed on Count?
                        {   
                            // Console.WriteLine($"i: {i}, Inventory.StoreAt: {allInventory[i].StoreAt}, "+
                            // $"Store.StoreID: {allStores[chosenStoreIndex].StoreID}, Inventory.APN: {allInventory[i].APN}"+
                            // $"Chosen APN: {choiceInt}");
                            //     Inventory.StoreAt             Store.StoreID                          Inventory.APN           
                            if(allInventory[targetInv].Store == allStores[chosenStoreIndex].StoreID &&  //allInventory[targetInv].Items[i].StoreAt
                            allInventory[targetInv].Items[i].APN == choiceInt)
                            {
                                getIndex = i;
                                Console.WriteLine($"i: {i}, allInventory[i].APN: {allInventory[targetInv].Items[i].APN}");
                            }
                        }

                        // Console.WriteLine($"You selected: {allStores[chosenStoreIndex].localInv[choiceInt].Name}"+
                        // $", Quantity: {allStores[chosenStoreIndex].localInv[choiceInt].OnHand}\nEnter new value:");
                        
                        Console.WriteLine($"You selected: {allInventory[targetInv].Items[getIndex].Name}"+
                        $", Quantity: {allInventory[targetInv].Items[getIndex].OnHand}\nEnter new value:");
                        choice = Console.ReadLine() ?? "";
                        res2 = Int32.TryParse(choice, out a);
                        if(res2)
                        {
                            allInventory[targetInv].Items[getIndex].OnHand = Int32.Parse(choice); //Set new Qty
                            if(Int32.Parse(choice) > 0){
                            _bl.ChangeInventory(allStores[chosenStoreIndex].StoreID, getIndex, Int32.Parse(choice)); //Call method to adjust Qty of item already on hand
                            }
                            else{_bl.RemoveItem(targetInv,getIndex);}
                        }
                        else{Console.WriteLine("Not a numeric value!");}
                    }
                    else//Change qty above, new item below
                    {
                        //A string was selected, add new item maybe
                        if(choice == "n")
                        {
                            //Add new item to stock
                            List<ProdDetails> getAllCarried = _bl.GetAllCarried();
                            //Add new item
                            string itemTypeStr; itemTypeStr = "";
                            //Show list of all carried items, manager can select which one to modify
                            for(int i = 0; i < getAllCarried.Count; i++)  
                            {
                                if(getAllCarried[i].ItemType == 0){itemTypeStr = "Clay";}
                                if(getAllCarried[i].ItemType == 1){itemTypeStr = "Tool";}
                                if(getAllCarried[i].ItemType == 2){itemTypeStr = "Equipment";}
                                Console.WriteLine($"\n[{i}], APN: {getAllCarried[i].APN}, "+
                                $"Item Name: {getAllCarried[i].Name}, "+
                                $"Item Type: {itemTypeStr}, "+
                                $"Item Cost: {getAllCarried[i].Cost}, "+
                                $"Item Weight: {getAllCarried[i].Weight}, \n"+
                                $"\tItem Description: {getAllCarried[i].Descr}");
                            }
                            Console.WriteLine("Choose which item to add from the above list.");
                            choice = Console.ReadLine() ?? "";
                            res2 = Int32.TryParse(choice, out a);
                            if(res2)
                            {   
                                choiceInt = Int32.Parse(choice); //Manager has chosen what item to add
                                bool abort = false; //Stop adding item if already added
                                
                                // Console.WriteLine($"targetInv: {targetInv}, allInventory.Count: {allInventory.Count}");
                                // Console.WriteLine($"Items.Count {allInventory[targetInv].Items.Count}");
                                //First check that this item isn't already in stock
                                //foreach(ProdDetails inv in allStores[chosenStore].localInv)
                                foreach(ProdDetails inv in allInventory[targetInv].Items)
                                {   
                                    // Console.WriteLine($"(Inside Foreach) targetInv: {targetInv}, chosenStore: {chosenStore}");
                                    // Console.WriteLine($"allinv.Store: {allInventory[targetInv].Store}");
                                    // Console.WriteLine($"chosenStore: {chosenStore},");
                                    // Console.WriteLine($"inv.APN: {inv.APN}, Carried APN: {getAllCarried[choiceInt].APN}");
                                    if(allInventory[targetInv].Store == chosenStore){            //inv.StoreAt
                                    if(inv.APN == getAllCarried[choiceInt].APN){abort = true;}}//This item is already in stock, abort
                                }

                                // Console.WriteLine($"abort: {abort}");
                                
                                //Continue with adding item
                                if(!abort){
                                Console.WriteLine($"You chose {getAllCarried[choiceInt].Name}, Now enter quantity for the item:");

                                choice = Console.ReadLine() ?? "";
                                res3 = Int32.TryParse(choice, out a);
                                if(res3)
                                {
                                    choiceInt2 = Int32.Parse(choice);//Qty
                                    ProdDetails addStock = new ProdDetails {
                                        //StoreAt = allStores[chosenStoreIndex].StoreID, 
                                        OnHand = choiceInt2, 
                                        APN = getAllCarried[choiceInt].APN,
                                        Name = getAllCarried[choiceInt].Name,
                                        ItemType = getAllCarried[choiceInt].ItemType,
                                        Descr = getAllCarried[choiceInt].Descr,
                                        Cost = getAllCarried[choiceInt].Cost,
                                        Weight = getAllCarried[choiceInt].Weight
                                    };

                                    //allInventory[targetInv].Items.Add(addStock);  //DO I NEED THIS EVEN?
                                    //Now to Save it
                                    _bl.AddItem(targetInv, addStock);
                                    abort = false; //reset
                                }}
                            }
                        }
                    }

                break;

                //Delete Store
                case "5":
                    List<Store> allStoresB = _bl.GetAllStores();
                    allInventory = _bl.GetAllInventory();
                    int useInv = -1;
                    Console.WriteLine($"Are you sure you want to remove the store: {allStoresB[chosenStoreIndex].StoreName}? [y,n]");
                    string decide = Console.ReadLine() ?? "";
                    //Get Inventory Index
                    for(int i = 0; i < allInventory.Count; i++)
                    {
                        if(allInventory[i].Store == allStoresB[chosenStoreIndex].StoreID)
                        {useInv = i; break;}
                    }

                    if(decide == "y")
                    {
                        Console.WriteLine($"The store: {allStoresB[chosenStoreIndex].StoreName} has been removed."+
                        "\nMake sure to allocate inventory where needed.\n");
                        _bl.RemoveStore(chosenStoreIndex);
                        if(allInventory.Count > -1){
                        _bl.RemoveInventory(useInv);//Deletes the store inventory
                        }
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
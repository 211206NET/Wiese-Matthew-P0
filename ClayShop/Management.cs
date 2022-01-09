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
        List<ProdDetails> allCarried = _bl.GetAllCarried();
        bool exit = false;
        int idStamp = allStores.Count+1;
        Console.WriteLine("Manage fired");
        //Main Loop
        while(!exit)
        {
            //Management Menu
            Console.WriteLine("Select an action:");
            Console.WriteLine("[0] Add store:"); //
            Console.WriteLine("[1] Select current store:"); //
            Console.WriteLine("[2] Manage company carried items:"); //
            Console.WriteLine($"[3] Adjust/Add store info [Current store number: {chosenStore}]:"); //
            Console.WriteLine($"[4] Manage store inventory [Current store number: {chosenStore}]:"); //
            Console.WriteLine($"[5] Delete store [Current store number: {chosenStore}]:"); //
            Console.WriteLine($"[6] See store order history for: {chosenStore}]:"); //
            Console.WriteLine("[x] Return to main menu:"); //
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
                    //decimal userST = Convert.ToDecimal(Int32.Parse(Console.ReadLine() ?? ""));
                    decimal userST = Decimal.Parse(Console.ReadLine() ?? "");
                    //Add user data to new store generation //DISABLED because of row error
                    //bool res = Decimal.TryParse(lclVersion, out localVersion);

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
                    if(change != ""){salesTax = Decimal.Parse(change);} change = ""; //Change sales tax 
                    //if(change != ""){salesTax = Convert.ToDecimal(Int32.Parse(change));} change = ""; //Change sales tax 

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
                    allCarried = _bl.GetAllCarried();
                    //_bl.GetAllInventory();

                    //First show current inventory
                    Console.WriteLine($"Inventory for {allStores[chosenStoreIndex].StoreName}");
                    //if(allStores[chosenStore].localInv.Count > 0){
     
                    Console.WriteLine($"allInventory.Count: {allInventory.Count}");
                    if(allInventory.Count > 0){

                    //Return what index of inventory we need to access
                    //List<ProdDetails> thisStoreInv = new List<ProdDetails>();
                    // int[] whatInv = new int[allInventory.Count]; int posInt = 0; //Store item number of inventory that belongs to this store
                    // for(int i = 0; i < allInventory.Count; i++)
                    // {
                    //     if(allInventory[i].Store == allStores[chosenStoreIndex].StoreID){posInt += 1; whatInv[posInt] = allInventory[i].Item;}
                    // }

                    //List<ProdDetails> thisStoreInv = new List<ProdDetails>();  
                    // int[] thisStoreInv = new int[allCarried.Count];
                    // for(int i = 0; i < allInventory.Count; i++)
                    // {
                    //     if(allInventory[i].Store == allStores[chosenStoreIndex].StoreID){thisStoreInv[i] = ???allCarried[i].APN;}
                    // }

                    //Show store inventory
                    //Loop through inventory checking store assignment, then nest loop to get details from carried
                    for(int i = 0; i < allInventory.Count; i++)
                    {        
                        if(allInventory[i].Store == allStores[chosenStoreIndex].StoreID)
                        {
                            //Console.WriteLine($"APN: [{allCarried[i].APN}]");
                            for(int j = 0; j < allCarried.Count; j++){        //allCarried[i].StoreAt will never make sense
                            if(allInventory[i].Item == allCarried[j].APN){
                            Console.WriteLine($"APN: [{allCarried[j].APN}] {allCarried[j].Name},"+
                            $" Cost: {allCarried[j].Cost}, Weight: {allCarried[j].Weight}\n"+
                            $"\tDescription: {allCarried[j].Descr}"); }
                            }
                        }
                    }}

                    // for(int i = 0; i < allInventory[targetInv].Items.Count; i++)
                    // {
                    //     Console.WriteLine($"APN: [{allInventory[targetInv].Items[i].APN}] {allInventory[targetInv].Items[i].Name},"+
                    //     $" Cost: {allInventory[targetInv].Items[i].Cost}, Weight: {allInventory[targetInv].Items[i].Weight}\n"+
                    //     $"\tDescription: {allInventory[targetInv].Items[i].Descr}"); 
                    // }}
                    Console.WriteLine("\nEnter an APN to select item to change quantity of,\nor enter 'n' to add new product from carried list.");
                    string choice = Console.ReadLine() ?? "";
                    bool res; bool res2; bool res3; int a; int choiceInt = 0;  int choiceInt2 = 0;
                    res = Int32.TryParse(choice, out a);

                    if(res)
                    {
                        //A SINGLE number was selected, choose an item to change qty
                        choiceInt = Int32.Parse(choice); //Convert string to int
                        
                        

                        //STORE INVENTORY
                        //First get the actual index of the item in Inventory for store inventory details
                        int getIndexInv = 0; 
                        for(int i = 0; i < allInventory.Count; i++) 
                        {   
                            // Console.WriteLine($"allInventory[i].Store: {allInventory[i].Store}, allStores[chosenStoreIndex].StoreID: "+
                            //     $"{allStores[chosenStoreIndex].StoreID}, allInventory[i].Item: {allInventory[i].Item}, choiceInt: {choiceInt}"+
                            //     $", chosenStoreIndex: {chosenStoreIndex}, allStores.Count: {allStores.Count}");
                            if(allInventory[i].Store == allStores[chosenStoreIndex].StoreID &&  //allInventory[targetInv].Items[i].StoreAt
                            allInventory[i].Item == choiceInt)
                            {
                                getIndexInv = i;
                            }
                        }
                        
                        //CARRIED
                        //Second get the actual index of the item in Carried for product details
                        int getIndex = 0;
                        if(getIndexInv > 0){
                        for(int i = 0; i < allCarried.Count; i++) 
                        {   
                            if(allCarried[i].APN == choiceInt)
                            {
                                getIndex = i;
                                Console.WriteLine($"i: {i}, allCarried[i].APN: {allCarried[i].APN}");
                            }
                        }
                        }

                        //Item
                        Console.WriteLine($"You selected: {allCarried[getIndex].Name}, getIndexInv: {getIndexInv}, "+
                        $", Quantity: {allInventory[getIndexInv].Qty}\nEnter amount to adjust by:"); //allInventory[targetInv].Items[getIndex].OnHand
                        choice = Console.ReadLine() ?? "";
                        res2 = Int32.TryParse(choice, out a);
                        if(res2)
                        {

                            int adjQty = Int32.Parse(choice); 

                            Console.WriteLine($"adjQty: {adjQty}");
                            //Get qty after adjustment
                            int determineQty = allInventory[getIndexInv].Qty+adjQty;

                            if(determineQty > -1){
                                if(determineQty > 0){
                                _bl.ChangeInventory(allInventory[getIndexInv].Id, determineQty);
                                }
                                else{_bl.RemoveInventory(allInventory[getIndexInv].Id);}
                            }
                            else
                            {Console.WriteLine($"Sorry, but there is insufficient inventory for you change request,\nresult would be less than zero");}
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
                            int newID = 0;
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
                                newID = allInventory.Count+1;
                                foreach(Inventory inv in allInventory)
                                {   
                                    //Check for duplicate Ids
                                    if(inv.Id == newID){newID++;}//Same id exists, keep interating id until it has no match

                                    // Console.WriteLine($"(Inside Foreach) targetInv: {targetInv}, chosenStore: {chosenStore}");
                                    // Console.WriteLine($"allinv.Store: {allInventory[targetInv].Store}");
                                    // Console.WriteLine($"chosenStore: {chosenStore},");
                                    // Console.WriteLine($"inv.APN: {inv.APN}, Carried APN: {getAllCarried[choiceInt].APN}");
                                    if(allInventory[targetInv].Store == chosenStore){            //inv.StoreAt
                                    if(inv.Item == getAllCarried[choiceInt].APN){abort = true;}}//This item is already in stock, abort
                                }

                                // Console.WriteLine($"abort: {abort}");
                                
                                //Continue with adding item
                                if(!abort){
                                Console.WriteLine($"You chose {getAllCarried[choiceInt].Name}, Now enter quantity for the item:");
                                choice = Console.ReadLine() ?? "";
                                res3 = Int32.TryParse(choice, out a);

                                
                                Console.WriteLine($"newID {allInventory.Count}, totally 10");
                                if(res3)
                                {
                                    choiceInt2 = Int32.Parse(choice);//Qty
                                    Inventory addStock = new Inventory {
                                        Id = newID, 
                                        Store = allStores[chosenStoreIndex].StoreID, 
                                        Item = getAllCarried[choiceInt].APN,
                                        Qty = choiceInt2
                                    };

                                    //allInventory[targetInv].Items.Add(addStock);  //DO I NEED THIS EVEN?
                                    //Now to Save it
                                    _bl.AddInventory(addStock);//targetInv, 
                                    abort = false; //reset
                                }}else{Console.WriteLine("This store already has this item, you can go back and change the quantity.");}
                            }
                        }
                    }

                break;

                //Delete Store
                case "5":
                    List<Store> allStoresB = _bl.GetAllStores();
                    allInventory = _bl.GetAllInventory();
                    int getInvID = -1;
                    Console.WriteLine($"Are you sure you want to remove the store: {allStoresB[chosenStoreIndex].StoreName}? [y,n]");
                    string decide = Console.ReadLine() ?? "";
                    //Get Inventory Index
                    for(int i = 0; i < allInventory.Count; i++)
                    {
                        if(allInventory[i].Store == allStoresB[chosenStoreIndex].StoreID)
                        {getInvID = allInventory[i].Id; break;}
                    }

                    if(decide == "y")
                    {
                        Console.WriteLine($"The store: {allStoresB[chosenStoreIndex].StoreName} has been removed."+
                        "\nMake sure to allocate inventory where needed.\n");
                        //Delete down the PK/FK dependency from most dependant to least
                        //Line
                        _bl.RemoveLineItem(getInvID);
                        //Orders
                        _bl.DeleteOrders(chosenStoreIndex);
                        //Inventory
                        if(allInventory.Count > -1){
                        _bl.RemoveInventory(chosenStoreIndex);//Deletes the store inventory, for the purpose of this project
                        }
                        //Store
                        _bl.RemoveStore(chosenStoreIndex); //Store is deleted last as it has PK/FK connections
                    }
                break;

                //See store order history
                case "6":
                    allStores = _bl.GetAllStores();
                    ViewOrders(allStores, allStores[chosenStoreIndex].StoreID);
                break;

                case "x":
                //Return to Main Menu:
                    exit = true;
                break;

                default:
                    Console.WriteLine("Input error, try again.");
                break;
            }//Switch
        }//Loop
    }//Start

    private void ViewOrders(List<Store> allTheStores, int sID)
    {
        int storeIndex = -1;
        List<Orders> allOrders = _bl.GetAllOrders();
        //Get Order Info
        Console.WriteLine("\n//---------------* List of Orders for current store: *---------------\\\\");
        foreach(Orders ordo in allOrders)//Loop through all orders
        {
            //Get Store Info
            for(int i = 0; i < allTheStores.Count; i++)
            {
                if(allTheStores[i].StoreID == ordo.StoreId && allTheStores[i].StoreID == sID){storeIndex = i;}
                //Console.WriteLine($"allTheStores[i].StoreID : {allTheStores[i].StoreID }, ordo.StoreId: {ordo.StoreId}");
            }

            //Console.WriteLine($"sID: {sID}, storeIndex: {storeIndex}");
            //Console.ReadLine();

            if(ordo.StoreId == sID && storeIndex > -1)//Find orders filtered by ones for current customer
            {
                Console.WriteLine($"Id: [{ordo.OrderId}], Store Id: [{sID}], Store Name: {allTheStores[storeIndex].StoreName}, "+
                $"Order Date: {ordo.OrderDate}, Total Items: {ordo.TotalQty}, Total Cost: {ordo.TotalCost}");
            }
            Console.WriteLine("\n\n");
        }
    }

}//Class
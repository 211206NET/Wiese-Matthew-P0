using DL;

namespace UI;

public class Management
{

    private IBL _bl;
    public Management(IBL bl)
    {
        _bl = bl;
    }


    public int chosenStore = 0;

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
            Console.WriteLine("[0] Add store:");
            Console.WriteLine("[1] Select current store:");
            Console.WriteLine("[2] Change inventory:");
            Console.WriteLine("[3] Adjust/Add store info:");
            Console.WriteLine("[x] Return to main menu:");
            string choose = Console.ReadLine();
            switch(choose)
            {
                //Add Store
                case "0":
                    //Get user input
                    Console.WriteLine("Enter a store name:");
                    string userStoreName = Console.ReadLine();
                    Console.WriteLine("Enter the city of the store:");
                    string userCity = Console.ReadLine();
                    Console.WriteLine("Enter the state of the store:");
                    string userState = Console.ReadLine();
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
                    Console.WriteLine("Choose which store you want:");
                    //Cycle through all stores show a list of them
                    for(int i = 0; i < allStores.Count; i++)  
                    {
                        //Console.WriteLine($"allStores.Count: {allStores.Count}");
                        Console.WriteLine($"[{i}], Store ID: {allStores[i].StoreID}, "+
                        $"Store Name: {allStores[i].StoreName}, "+
                        $"City: {allStores[i].City}, "+
                        $"State: {allStores[i].State}\n");
                        allStores[i].SetUp(); //Tell store to generate inventory PLACE HOLDER
                    }

                    int select = Int32.Parse(Console.ReadLine()); 
                    // string select = Console.ReadLine(); 
                    // switch(select)
                    // {
                    //     case Char.IsNumber:
                    //     break;
                    // }
                    chosenStore = select;   //User inputs a choice
                    
                break;

                //Change Inventory:
                case "2":

                    Console.WriteLine("Adding and changing product qty here");
                    //Add a new item 
                    //Yeah um... no
                    //List<Clay> addClay = _bl.AddClay(1,dsds);
                    // List<Store> addClay = _bl.AddClay(id,sendClay);
                    
                    // Clay storeClay = new Clay
                    // {
                    //     StoreID = chosenStore,
                    //     Name = "Hard Clay"
                    // };
                    // addClay.Add(storeClay);
                    
                break;

                //Adjust/Add Store Info:
                case "3":
                    
                    Console.WriteLine("Changing Store Address etc. Or deleting store");
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
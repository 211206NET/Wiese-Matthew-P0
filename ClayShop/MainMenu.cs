using DL;

namespace UI;
public class MainMenu {

private IBL _bl;
public MainMenu(IBL bl)
{
    _bl = bl;
}


public void Start() 
{

List<Store> allStores = _bl.GetAllStores();

//List<Store> allStores = new List<Store>();
bool doOnce = false;
int idStamp = 1;
//PLACE HOLDER
//Set up inventory for now (Place-holder until data=base)
//Because data is deleted each time this is closed, for now I
//have the data for stores and inventory hard coded at the start 
//of the scripts for testing
if(doOnce == false){
Store store1 = new Store
{
    StoreID = idStamp,
    StoreName = "Randal's Clay Shop",
    City = "Tacoma",
    State = "WA"
};
Store store2 = new Store
{
    StoreID = idStamp+1,
    StoreName = "Mary's Modeling Clay Shop",
    City = "Tacoma",
    State = "WA"
};
Store store3 = new Store
{
    StoreID = idStamp+2,
    StoreName = "The Clay Zone",
    City = "Tacoma",
    State = "WA"
};

allStores.Add(store1);
allStores.Add(store2);
allStores.Add(store3);
idStamp = 4; //The next store to be made will have this ID
doOnce = true;
}//end PLACE HOLDER to get initial stores

//Clay Shop! Matthew Wiese: P0
Console.WriteLine("Welcome to the plasticine clay shop.\n" +
"Here we have the best clay for claymation.");

bool exit = false;
int pos = 0; //Position, where the user is currently in the application
int chosenStore = 0; //Which store the user currently has chosen


//Main Loop
while(!exit)
{
    switch(pos)
    {
        //Login
        case 0:
            Console.WriteLine("Login Here: (Place holder: Enter any value to continue)");
            Console.ReadLine(); //PLACE HOLDER
            pos = 1;
        break;

        //Select Store
        case 1:
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
            Console.WriteLine("[ADMIN] Add a store: enter 'a'");//Go to case 6 main switch
            string select = Console.ReadLine();
            if(select == "a" || select == "A") //User wants to add a store
            {
                pos = 6;
                break;
            }
            else
            {
                chosenStore = Int32.Parse(select);   //User inputs a choice
                pos = 2;
            }
        break;

        //Store Main Menu
        case 2:
            Console.WriteLine($"{allStores[chosenStore].StoreName}\nWhat would you like to do?");
            Console.WriteLine("1.) View Clays");//1.)
            Console.WriteLine("2.) View Professional Clay Tools");//2.) 
            Console.WriteLine("3.) View Claymation Studio Accessories");//3.) 
            Console.WriteLine("4.) Return to Store Selection");//4.)  
            Console.WriteLine("5.) Add new product to inventory");//5.)  
            Console.WriteLine("6.) Change quantity of an inventory [UNDER CONSTRUCTION]");//6.)  
            Console.WriteLine("x.) Exit");//x.) 

            string input = Console.ReadLine();
            //Homunculus Switch
            switch(input)
            {
                case "1":   
                    pos = 3;//View Clays
                break;
                case "2":
                    pos = 4;//View Tools
                break;
                case "3":
                    pos = 5;//View Studio Equipment
                break;
                case "4":
                    pos = 1;//Return to Store Selection
                break;
                case "5":
                    pos = 7;//Add New Item
                break;
                case "6":
                    pos = 1;//Change Qty [UNDER CONSTRUCTION] PLACE HOLDER
                break;
                case "x":
                    exit = true;//Exit App
                break;
                default:
                    Console.WriteLine("Please enter 1,2,3,4 or x");
                break;
            }//End Switch pos == 2 
        break;

        //Display Clay Inventory for Selected Store
        case 3:
            Console.WriteLine("Clay Inventory");
            foreach(Clay inv in allStores[chosenStore].locClay)
            {
                Console.WriteLine($"[{inv.APN}] Clay Product: {inv.Name}");
            }

            Console.WriteLine("Select product for more details or 'x' to return to menu:\n");
            //int chooseAPN = Int32.Parse(Console.ReadLine()); 
            string chooseAPN = Console.ReadLine(); 
            if(chooseAPN == "x")
            {
                pos = 2; //Return to main Menu
            }
            else
            {
                int intAPN = Int32.Parse(chooseAPN); //Select APN
                foreach(Clay inv2 in allStores[chosenStore].locClay)
                {
                    if(inv2.APN == intAPN)
                    {
                        inv2.ShowDesc();
                    }
                }
            }
            Console.WriteLine("\nEnter any value to return to main menu");
            Console.ReadLine(); //For now take user input to continue
            pos = 2;
        break;

        //Display Tool Inventory for Selected Store
        case 4:
            Console.WriteLine("Tool Inventory");
            foreach(Tools tInv in allStores[chosenStore].locTools)
            {
                Console.WriteLine($"Tool Product: {tInv.Name}");
            }
            Console.WriteLine("Enter anything to continue:");
            Console.ReadLine(); //For now take user input to continue
            pos = 2; //Return to main Menu
        break;

        //Display Studio Equipment Inventory for Selected Store
        case 5:
            Console.WriteLine("Studio Equipment Inventory");
            foreach(Equip eInv in allStores[chosenStore].locEquip)
            {
                Console.WriteLine($"Equipment Product: {eInv.Name}");
            }
            Console.WriteLine("Enter anything to continue:");
            Console.ReadLine(); //For now take user input to continue
            pos = 2; //Return to main Menu
        break;

        //Add a New Store
        case 6:
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
            pos = 1; //Return to store selection
        break;

        case 7:
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

        //pos var is set wrong
        default:
            Console.WriteLine("ERROR: pos is set to invalid value somehow");//Should never occur
            pos = 2; //Return to main Menu
        break;

    }//End Master Case Switch



}//End Main While Loop

}//End Start

}//End MainMenu Class




// - Workflow of Viewing StoreFront Inventory
// 1. I want to hide this functionality unless they're logged in
//     - Check if they're logged in or not
//     - Have currentCustomer set to a default, that will be null before 
        //they log in (So once they 'login', the current customer is not 
        //null, and has current customer info)
//     - And then once I know that it's not null, then I'll display this menu

// 2. Prompt the user to log in (if they haven't)
//     - I need to go back to the log in menu (of a sort)
//     - And then give them log in screen again

// 3. Prompt them to select which storefront to browse
//     - First, I need to grab all storefronts
//     - Iterate through the stores and display them
//     - Once they select the storefront, save it somewhere so I can query the inventory 
        //of the store

// 4. Once they select a storefront, query the inventory of the storefront
//     a. Find all inventory that belongs to the storefront
//     b. Then iterate through that list and display the inventory accordingly

// 5. Display the storefront inventory with (its quantity, maybe?)

// List<Store> allStores = new List<Store>();

// //PLACE HOLDER
// //Set up inventory for now (Place-holder until data=base)
// //Because data is deleted each time this is closed, for now I
// //have the data for stores and inventory hard coded at the start 
// //of the scripts for testing
// Store store1 = new Store
// {
//     StoreID = 1,
//     StoreName = "Randal's Clay Shop",
//     City = "Tacoma",
//     State = "WA"
// };
// Store store2 = new Store
// {
//     StoreID = 2,
//     StoreName = "Mary's Modeling Clay Shop",
//     City = "Tacoma",
//     State = "WA"
// };
// Store store3 = new Store
// {
//     StoreID = 3,
//     StoreName = "The Clay Zone",
//     City = "Tacoma",
//     State = "WA"
// };

// allStores.Add(store1);
// allStores.Add(store2);
// allStores.Add(store3);

//END PLACE HOLDER
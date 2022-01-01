using DL;

namespace UI;
public class MainMenu {

private IBL _bl;
public MainMenu(IBL bl)
{
    _bl = bl;
}

public int chosenStore = 0; //Which store the user currently has chosen

public void Start() 
{

List<Store> allStores = _bl.GetAllStores();

List<Inventory> allInventory = _bl.GetAllInventory();

List<Customers> allCustomers = _bl.GetAllCustomers();

//Clay Shop! Matthew Wiese: P0
Console.WriteLine("Welcome to the plasticine clay shop.\n" +
"Here we have the best clay for claymation.");

bool exit = false;
bool canLog = false; //For making new customer
bool canMake = false; //For making new customer
bool manager = false; //If Manager is logged in
int pos = 0; //Position, where the user is currently in the application
int whatItem = 0; //0 = Clay, 1 = Tools, 2 == Equipment (For showing item inventory by section)

DateOnly dateOnlyVar = DateOnly.FromDateTime(DateTime.Now); 

// string[] testA = new string[8];
// testA[1] = "abCDef";
// Console.WriteLine($"Easy Way: {testA[1][0]}");

// //int index = testA[1].IndexOf("C");
// char[] chars = testA[1].ToCharArray();
// char first = chars[0];
// Console.WriteLine($"result of tets string: {first}");

// List<string> Strlist = new List<string>();  
// Strlist.Add("ZbCDef");
// Console.WriteLine($"Easy List Way: {Strlist[0][0]}");

//Console.WriteLine($"{(32 / 8 * 2)}");

//Main Loop
while(!exit)
{
    switch(pos)
    {
        //Login
        case 0:
        //public DateOnly dateOnlyVar = DateOnly.Today; 
        Console.WriteLine($"dateOnlyVar: {dateOnlyVar}");
        Console.WriteLine("1.) Login Here");
        Console.WriteLine("2.) No account? Create an account.");//1.)
        string choose = Console.ReadLine() ?? ""; //PLACE HOLDER

        switch(choose)
        {
        case "1":
        while(!canLog)
        {
            //Login Functionality here
            Console.WriteLine("Enter user name here");
            string userNL = Console.ReadLine() ?? "";
            Console.WriteLine("Enter password here");
            string pwL = Console.ReadLine() ?? "";

            foreach(Customers custL in allCustomers)
            {
                if(custL.UserName == userNL && custL.Pass == pwL)
                {       
                    canLog = true;
                    //Check if manager login
                    if(userNL.IndexOf("MNG",2)>0){manager = true;}else{manager = false;}
                    if(manager == true){Console.WriteLine($"\nWelcome back manager {userNL}!\n");}else
                    {Console.WriteLine($"\nWelcome back {userNL}!\n");}
                }
            }
            
            if(!canLog){Console.WriteLine("\nUser name and/or password incorrect, please try again\n");}
        }


            pos = 1;
        break;

        case "2":

        while(!canMake)
        {
            //Make new customer
            Console.WriteLine("Enter user name here");
            string userN = Console.ReadLine() ?? "";

            Console.WriteLine("Enter new password here");
            string pw1 = Console.ReadLine() ?? "";
            Console.WriteLine("Re-enter new password here");
            string pw2 = Console.ReadLine() ?? "";

            //Note: to make a manager login, the user name must contain the string "MNG"
            //The system for restricting a manager account to be created is not built yet

            if(pw1 == pw2)
            {
            canMake = true;
            //Check if this customer already exists
            foreach(Customers custo in allCustomers)
            {
                if(custo.UserName == userN)
                {
                //An account with this name already exists
                canMake = false;
                Console.WriteLine("Sorry, but an account with this name already exists.");
                }else{canMake = true;}    
            }
            }else{canMake = false;}

            if(canMake == true)
            {
                // _bl.AddCustomer(newCust);
                _bl.AddCustomer(0,userN,pw1);
            }

            //Check if manager login
            if(userN.IndexOf("MNG",2)>0){manager = true;}else{manager = false;}
        }
        break;

        default:
            Console.WriteLine("Input a correct choice.");
        break;
        }

        if(canMake == true || canLog == true){pos = 1;}else{pos = 0;}
        break;

        //Select Store
        case 1:
            //Update Lists
            allStores = _bl.GetAllStores();
            allInventory = _bl.GetAllInventory();
            allCustomers = _bl.GetAllCustomers();
            Console.WriteLine("Choose which store you want:");
            //Cycle through all stores show a list of them
            for(int i = 0; i < allStores.Count; i++)  
            {
                //Console.WriteLine($"allStores.Count: {allStores.Count}");
                Console.WriteLine($"[{i}], Store ID: {allStores[i].StoreID}, "+
                $"Store Name: {allStores[i].StoreName}, "+
                $"City: {allStores[i].City}, "+
                $"State: {allStores[i].State}");
                //allStores[i].SetUp(); //Tell store to generate inventory PLACE HOLDER
            }

            //Console.WriteLine("[ADMIN] Add a store: enter 'a'");//Go to case 6 main switch
            if(manager == true){Console.WriteLine("[a], <ADMIN> Management menu");//Go to case 6 main switch
                string select = Console.ReadLine() ?? "";
                if(select == "a" || select == "A") //User wants to add a store
                {
                    pos = 6;
                    break;
                }
                else{chosenStore = Int32.Parse(select);}
            }
            else
            {
                //Normal customer login
                chosenStore = Int32.Parse(Console.ReadLine() ?? "");   //User inputs a choice
            }
            pos = 2;
        break;

        //Store Customer Main Menu 
        case 2:
            Console.WriteLine($"\nWelcome to {allStores[chosenStore].StoreName}\nWhat would you like to do?");
            Console.WriteLine("1.) Shop Clays");//1.)
            Console.WriteLine("2.) Shop Professional Clay Tools");//2.) 
            Console.WriteLine("3.) Shop Claymation Studio Accessories");//3.) 
            Console.WriteLine("4.) Return to Store Selection [ Warning: This will clear items in your cart! ]");//4.)   
            Console.WriteLine("5.) Checkout");//5.)  
            Console.WriteLine("x.) Exit");//x.) 

            string input = Console.ReadLine() ?? "";
            //Homunculus Switch
            switch(input)
            {
                case "1":   
                    pos = 3;//View Clays
                    whatItem = 0;
                break;
                case "2":
                    pos = 3;//View Tools
                    whatItem = 1;
                break;
                case "3":
                    pos = 3;//View Studio Equipment
                    whatItem = 2;
                break;
                case "4":
                    pos = 1;//Return to Store Selection
                break;
                case "5":
                    pos = 7;//Checkout
                break;
                case "x":
                    exit = true;//Exit App
                break;
                default:
                    Console.WriteLine("Please enter 1,2,3,4,5 or x");
                break;
            }//End Switch pos == 2 
        break;

        //Display Inventory for Selected Store
        case 3:
            int remAPN = 0; string remName = ""; decimal remCost = 0; 
            List<LineItems> lineItemsList = _bl.GetAllLineItem();//Update shopping list
            if(whatItem == 0){Console.WriteLine("Clay Inventory");}
            if(whatItem == 1){Console.WriteLine("Tools Inventory");}
            if(whatItem == 2){Console.WriteLine("Equipment Inventory");}

            int targetInv = 0;
            for(int i = 0; i < allInventory.Count; i++)
            {
                if(allInventory[i].Store == allStores[chosenStore].StoreID){targetInv = i;}
            }

            foreach(ProdDetails pDet in allInventory[targetInv].Items)
            {
                if(pDet.ItemType == whatItem && pDet.StoreAt == allStores[chosenStore].StoreID)
                {Console.WriteLine($"[{pDet.APN}] Clay Product: {pDet.Name}");}
            }

            Console.WriteLine("Select product for more details or 'x' to return to menu:\n");
            //int chooseAPN = Int32.Parse(Console.ReadLine()); 
            string chooseAPN = Console.ReadLine() ?? ""; 
            if(chooseAPN == "x")
            {
                pos = 2; //Return to main Menu
            }
            else
            {
                int intAPN = Int32.Parse(chooseAPN); //Select APN
                foreach(ProdDetails prodD in allInventory[targetInv].Items)
                {
                    if(prodD.ItemType == whatItem){ 
                    if(prodD.APN == intAPN)
                    {
                        //inv2.ShowDesc(); //This should come from BL...?
                        Console.WriteLine($"Cost: {prodD.Cost}, APN: [{prodD.APN}],"+
                        $" Clay Product: {prodD.Name}, Weight: {prodD.Weight}"+
                        $"\nDescription: {prodD.Desc}, Quantity left: {prodD.OnHand}"); 
                        remAPN = prodD.APN;  remName = prodD.Name ?? "";  
                        remCost = prodD.Cost; 
                    }} 
                }//End Loop

                //Add to cart option here:
                Console.WriteLine("\nDo you want to buy this item? y/n");
                string buy = Console.ReadLine() ?? "";
                if(buy == "y")
                {
                    Console.WriteLine("\nHow many do you want to buy?");
                    int qtyToBuy = Int32.Parse(Console.ReadLine() ?? ""); //need check for parse!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                    decimal sendTax = Convert.ToDecimal(chosenStore*0.2); //[PLACE HOLDER]

                    
                    //Now to Save it
                    _bl.AddLineItem(remAPN, remName ?? "", qtyToBuy, remCost, sendTax);         
                    pos = 2;
                    break;
                }
                else
                {
                    pos = 2;
                    break;
                }
            }

            Console.WriteLine("\nEnter any value to return to main menu");
            Console.ReadLine(); //For now take user input to continue
            pos = 2;
        break;

        // //Display Tool Inventory for Selected Store
        // case 4:
        //     Console.WriteLine("Tool Inventory");
        //     //Return what index of inventory we need to access
        //     int targetInvT = 0;
        //     for(int i = 0; i < allInventory.Count; i++)
        //     {
        //         if(allInventory[i].Store == allStores[chosenStore].StoreID){targetInv = i;}
        //     }

        //     foreach(ProdDetails pDetT in allInventory[targetInvT].Items)
        //     {
        //         if(pDetT.ItemType == 1 && pDetT.StoreAt == allStores[chosenStore].StoreID)
        //         {Console.WriteLine($"[{pDetT.APN}] Clay Product: {pDetT.Name}");}
        //     }

        //     Console.WriteLine("Select product for more details or 'x' to return to menu:\n");
        //     string chooseAPNT = Console.ReadLine() ?? ""; 
        //     if(chooseAPNT == "x")
        //     {
        //         pos = 2; //Return to main Menu
        //     }
        //     else
        //     {
        //         int intAPN = Int32.Parse(chooseAPNT); //Select APN

        //         foreach(ProdDetails prodDT in allInventory[targetInvT].Items)
        //         {
        //             if(prodDT.ItemType == 1){ 
        //             if(prodDT.APN == intAPN)
        //             {
        //                 //inv2.ShowDesc(); //This should come from BL...?
        //                 Console.WriteLine($"Cost: {prodDT.Cost}, APN: [{prodDT.APN}],"+
        //                 $" Clay Product: {prodDT.Name}, Weight: {prodDT.Weight}"+
        //                 $"\nDescription: {prodDT.Desc}, Quantity left: {prodDT.OnHand}"); 
        //                 remAPN = prodDT.APN;  remName = prodDT.Name ?? "";  
        //                 remCost = prodDT.Cost; 
        //             }}
        //         }//End For 
                
        //         //Add to cart option here:

        //     }
        //     Console.WriteLine("\nEnter any value to return to main menu");
        //     Console.ReadLine(); //For now take user input to continue

        //     pos = 2; //Return to main Menu
        // break;

        // //Display Studio Equipment Inventory for Selected Store
        // case 5:
        //     Console.WriteLine("Studio Equipment Inventory");
        //     //Return what index of inventory we need to access
        //     int targetInvE = 0;
        //     for(int i = 0; i < allInventory.Count; i++)
        //     {
        //         if(allInventory[i].Store == allStores[chosenStore].StoreID){targetInvE = i;}
        //     }

        //     foreach(ProdDetails pDetE in allInventory[targetInvE].Items)
        //     {
        //         if(pDetE.ItemType == 2 && pDetE.StoreAt == allStores[chosenStore].StoreID)
        //         {Console.WriteLine($"[{pDetE.APN}] Clay Product: {pDetE.Name}");}
        //     }

        //     Console.WriteLine("Select product for more details or 'x' to return to menu:\n");
        //     string chooseAPNE = Console.ReadLine() ?? ""; 
        //     if(chooseAPNE == "x")
        //     {
        //         pos = 2; //Return to main Menu
        //     }
        //     else
        //     {
        //         int intAPN = Int32.Parse(chooseAPNE); //Select APN

        //         foreach(ProdDetails prodDE in allInventory[targetInvE].Items)
        //         {
        //             if(prodDE.ItemType == 2){ 
        //             if(prodDE.APN == intAPN)
        //             {
        //                 //inv2.ShowDesc(); //This should come from BL...?
        //                 Console.WriteLine($"Cost: {prodDE.Cost}, APN: [{prodDE.APN}],"+
        //                 $" Clay Product: {prodDE.Name}, Weight: {prodDE.Weight}"+
        //                 $"\nDescription: {prodDE.Desc}, Quantity left: {prodDE.OnHand}"); 
        //                 remAPN = prodDE.APN;  remName = prodDE.Name ?? "";  
        //                 remCost = prodDE.Cost; 
        //             }}
        //         }//End For 

        //         //Add to cart option here:

        //     }
        //     Console.WriteLine("\nEnter any value to return to main menu");
        //     Console.ReadLine(); //For now take user input to continue

        //     pos = 2; //Return to main Menu
        // break;

        //Management Menu
        case 6:
        
            Console.WriteLine($"Pos: {pos}");
            //Here, I instantiated an implementation of IRepo (FileRepo)
            IRepo repo = new FileRepo();
            //next, I instantiated CSBL (an implementation of IBL) and then injected IRepo implementation for IBL/CSBL
            IBL bl = new CSBL(repo);
            //Finally, I instantiate MngMenu that needs an instance that implements Business Logic class
            Management mngMenu = new Management(bl);
            mngMenu.chosenStore = this.chosenStore;
            //Reset local settings for when returning to this menu
            chosenStore = 0;
            pos = 1;
            mngMenu.Start();

        break;

        //Checkout/Shopping Cart
        case 7:
        
            Console.WriteLine($"Pos: {pos}");
            //Here, I instantiated an implementation of IRepo (FileRepo)
            IRepo repoCart = new FileRepo();
            //next, I instantiated CSBL (an implementation of IBL) and then injected IRepo implementation for IBL/CSBL
            IBL blCart = new CSBL(repoCart);
            //Finally, I instantiate repoCart that needs an instance that implements Business Logic class
            Cart cartMenu = new Cart(blCart);
            cartMenu.chosenStore = this.chosenStore;
            //Reset local settings for when returning to this menu
            //chosenStore = 0;
            pos = 2;
            cartMenu.Start();

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
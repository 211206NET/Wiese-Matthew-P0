
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
            Console.WriteLine("[0] Add item to list of carried items:");
            Console.WriteLine("[1] Remove item from list of carried items:");
            Console.WriteLine("[2] Add inventory to a store:");
            Console.WriteLine("[3] Remove inventory from a store:");
            Console.WriteLine("[4] Make changes to item on carried items list:");
            Console.WriteLine("[x] Return to management menu:");

            string choose = Console.ReadLine();
            switch(choose)
            {
                //Add item to list of carried items:
                case "0":
                    //Get user input
                    Console.WriteLine("Enter an item name:");
                    string itemName = Console.ReadLine();
                    Console.WriteLine("Enter what type of item it is [0 = clay, 1 = tools, 2 = equip]:");
                    int itemType = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Enter a description of the item:");
                    string itemDesc = Console.ReadLine();
                    Console.WriteLine("Enter the cost of the item");
                    Decimal itemCost = Convert.ToDecimal(Int32.Parse(Console.ReadLine()));
                    Console.WriteLine("Enter the weight of the item");
                    Double itemWeight = Convert.ToDouble(Int32.Parse(Console.ReadLine()));

                    //Add user data to new carried items list
                    ProdDetails itemNew = new ProdDetails
                    {
                            APN = 0,
                            Name = itemName,
                            ItemType = itemType,
                            Desc = itemDesc,
                            Cost = itemCost,
                            Weight = itemWeight
                    };

                    //_bl.AddCarried(itemNew);
                    _bl.AddCarried(0,itemName,itemType,itemDesc,itemCost,itemWeight);

                    Console.WriteLine($"[{itemName}] successfully created and added to the list of carried items for this franchise.\n");
                break;
                
                //Remove item from list of carried items:
                case "1":
                break;

                //Add inventory to a store:
                case "2":
                break;

                //Remove inventory from a store:
                case "3":
                break;

                //Make changes to item on carried items list:
                case "4":
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
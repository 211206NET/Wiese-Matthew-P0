using System.Text.RegularExpressions;

namespace Models;

public class Store
{
    public List<ProdDetails> localInv = new List<ProdDetails>(); //Will store all inventory
    
    public string? StoreName { get; set; }
    public int StoreID { get; set; }//Each different store has an ID 
    public string? City { get; set; }
    public string? State { get; set; }

    // private IBL _bl;
    // public Store(IBL bl)
    // {
    //     _bl = bl;
    // }

    //...Nah
    // private string _name;
    // public Store(string name)
    // {
    //     this._name = name;
    // }
    // public string StoreName 
    // { 
    //     get => _name;
    //     set
    //     {
    //         Regex pattern = new Regex("^[a-zA-Z0-9=!?']+$");
    //         if(string.IsNullOrWhiteSpace(value))
    //         {
    //             //Checking if null
    //             throw new Exception("Name can't be empty");//Should not use this, instead use custom exception
    //         }
    //         else if(!pattern.IsMatch(value))//even if not null, still check for name validity with RegEx
    //         {
    //             throw new Exception("Name has invalid character");
    //         }
                //else
                //this._name = value;
            
    //         //([A-Z]\w+/g)   /^[a-zA-Z0-9=!?']+$/g
    //     } 
    // }
    //public bool Inited { get; set; } //If this Class ran at least once yet, IDK might be useful
    //public int CPN = 1; //{ get; set; } //Clay Product Number //unused

    //public Store(){}

    //Set details of store location
    // public Store() //int storeID, string storeName, string city, string state
    // {
    //      this.StoreID = storeID;
    //      this.StoreName = storeName;
    //      this.City = city;
    //      this.State = state;
    // }

    //New master item list

    // public void RefreshInv()
    // {
    //     //localInv = FileRepo.GetAllInventory();
    // }

    //Old lists
    // public List<Clay> locClay = new List<Clay>(); //Local Clay Inventory
    // public List<Tools> locTools = new List<Tools>(); //Local Clay Inventory
    // public List<Equip> locEquip = new List<Equip>(); //Local Clay Inventory
    
    // public Store()
    // {
    //     this.locClay = new List<Clay>();
    // }

    // public List<Clay> locClay { get; }

//     //Set up inventory for now PLACE HOLDER
//     Clay addClay = new Clay
//     {
//         Name = "Green Clay",
//         APN = 0
//     };
//     Clay addClay2 = new Clay
//     {
//         Name = "Red Clay",
//         APN = 1
//     };
//     Clay addClay3 = new Clay
//     {
//         Name = "Blue Clay",
//         APN = 2
//     };
//     Tools addTools = new Tools
//     {
//         Name = "Wooden Japanese clay knife"
//     };
//     Tools addTools2 = new Tools
//     {
//         Name = "Stainless steel needle tool"
//     };
//     Tools addTools3 = new Tools
//     {
//         Name = "Marble rolling pin"
//     };
//     Equip addEquip = new Equip
//     {
//         Name = "Studio Quality Drafting Table LED Desk Lamp with Clamp"
//     };
//     Equip addEquip2 = new Equip
//     {
//         Name = "Turntable 14\""
//     };
//     Equip addEquip3 = new Equip
//     {
//         Name = "Desktop GreenScreen 30\""
//     };
    
//    public void SetUp()//PLACE HOLDER inventory filler
//    {
//         if(Inited == false)
//         {
//             if(StoreID != 2){locClay.Add(addClay);}
//             if(StoreID != 3){locClay.Add(addClay2);}
//             locClay.Add(addClay3);
//             if(StoreID != 2){locTools.Add(addTools);}
//             if(StoreID != 1){locTools.Add(addTools2);}
//             locTools.Add(addTools3);
//             if(StoreID != 3){locEquip.Add(addEquip);}
//             if(StoreID != 1){locEquip.Add(addEquip2);}
//             locEquip.Add(addEquip3);
//             //Console.WriteLine($"locClay.Count: {locClay.Count}");
//             if(locClay.Count > 0){locClay[0].ClayDesc();} //Set item descriptions
//             if(locClay.Count > 1){locClay[1].ClayDesc();}
//             if(locClay.Count > 2){locClay[2].ClayDesc();}
//             Inited = true;
//         }
//    }

//     public void ShowClay()//Not currently used
//     {
        
//         foreach(Clay clayTypes in locClay)//Just made up clayTypes right here
//         {
//             //Console.WriteLine($"Clay: {clayTypes.Name}");
//             clayTypes.ClayDesc();
//             Console.WriteLine("=======END========");
//             // foreach(Review review in resto.Reviews)
//             // {
//             //     Console.WriteLine($"Rating: {review.Rating} \t Note: {review.Note}");
//             // }
//         }
//     }

    




}
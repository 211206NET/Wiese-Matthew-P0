namespace Models;

public class ProdDetails
{
    //Product details database
    //This object stores detailed information on each product
    //This stores information for all types of products
    //Product Objects will reference this object to retrive detailed information
    public ProdDetails(){}

    public ProdDetails(int onHand, int itemType, int apn, string name, double weight, decimal cost, string desc) //int storeAt, 
    {
        //this.StoreAt = storeAt;
        this.OnHand = onHand;
        this.ItemType = itemType;
        this.APN = apn;
        this.Name = name;
        this.Weight = weight;
        this.Cost = cost;
        this.Desc = desc;
    }

    // public void ShowDesc()
    // {
    //     //Console.WriteLine($"APN: {APN}");
    //     //if(Details.Count >= 0)//Safeguard to prevent out of range array
    //     //{
    //         Console.WriteLine($"Name: {Name}, Cost: {Cost}, "+
    //         $"Weight: {Weight}, Description: {Desc}");
    //     //}
    // }

    public int APN { get; set; }//Assigned Product Number  [PK]
    //public int StoreAt { get; set; }//What store this item has been stocked at  [FK]  [Will use Inventory between this and store]
    public string? Name { get; set; }//Mirrored from product object
    public int OnHand { get; set; }//Number of this item the store has currently
    public int ItemType { get; set; }//0 = clay, 1 = tools, 2 = equip
    public double Weight { get; set; }//How many pounds one unit of this product weigh
    public decimal Cost { get; set; }//Amount the store sells for
    public string? Desc { get; set; }//Description of product
    

}
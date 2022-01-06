using System.Data;

namespace Models;

public class ProdDetails
{
    //Product details database
    //This object stores detailed information on each product
    //This stores information for all types of products
    //Product Objects will reference this object to retrive detailed information
    public ProdDetails(){}

    public ProdDetails(DataRow row)
    {
        this.APN = (int) row["Id"];
        this.Name = row["Name"].ToString() ?? "";
        this.OnHand = (int) row["Id"];
        this.ItemType = (int) row["Id"];
        this.Weight = (double) row["Cost"];
        this.Cost = (decimal) row["Cost"];
        this.Descr = row["Name"].ToString() ?? "";
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
    public string? Descr { get; set; }//Description of product
    
    public void ToDataRow(ref DataRow row)
    {
        row["APN"] = this.APN;
        row["Name"] = this.Name;
        row["OnHand"] = this.OnHand;
        row["ItemType"] = this.ItemType;
        row["Weight"] = this.Weight;
        row["Cost"] = this.Cost;
        row["Descr"] = this.Descr;
    }

    

}
using System.Data;

namespace Models;

public class ProdDetails
{
    //Product details database
    //This object stores detailed information on each product
    //This stores information for all types of products
    //Product Objects will reference this object to retrive detailed information
    public ProdDetails(){}

    /// <summary>
    /// Convert product details table data to data row
    /// </summary>
    /// <param name="row"></param>
    public ProdDetails(DataRow row)
    {
        this.APN = (int) row["APN"];
        //this.StoreAt = (int) row["StoreAt"];
        this.Name = row["Name"].ToString() ?? "";
        //this.OnHand = (int) row["OnHand"];
        this.ItemType = (int) row["ItemType"];
        this.Weight = (double) row["Weight"];
        this.Cost = (decimal) row["Cost"];
        this.Descr = row["Descript"].ToString() ?? "";
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
    //public int OnHand { get; set; }//Number for how many to add when adding new inventory, no use outside of that
    public int ItemType { get; set; }//0 = clay, 1 = tools, 2 = equip
    public double Weight { get; set; }//How many pounds one unit of this product weigh
    public decimal Cost { get; set; }//Amount the store sells for
    public string? Descr { get; set; }//Description of product
    
    /// <summary>
    /// Fill in columns in database row with this instance
    /// </summary>
    /// <param name="row"></param>
    public void ToDataRow(ref DataRow row)
    {
        row["APN"] = this.APN;
        //row["StoreAt"] = this.StoreAt;
        row["Name"] = this.Name;
        //row["OnHand"] = this.OnHand;
        row["ItemType"] = this.ItemType;
        row["Weight"] = this.Weight;
        row["Cost"] = this.Cost;
        row["Descript"] = this.Descr;
    }

    

}
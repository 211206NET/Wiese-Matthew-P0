using System.Text.RegularExpressions;
using System.Data;
//using CustomExceptions;
namespace Models;

public class Inventory
{
    //This will serve as a prefab to be added to the Inventory Table
    public Inventory()
    {
        //this.Items = new List<ProdDetails>();
    }

    /// <summary>
    /// Convert inventory table data to data row
    /// </summary>
    /// <param name="row"></param>
    public Inventory(DataRow row)
    {
        this.Id = (int) row["Id"];
        this.Store = (int) row["Store"]; 
        this.Item = (int) row["Item"]; 
        this.Qty = (int) row["Qty"]; //row["Store"].ToString() ?? "";
    }

    public int Id { get; set; } // 1 for 1 relationship with Store ID: redundant [PK]
    public int Store { get; set; } //What store this Inventory is for [FK] 
    public int Item { get; set; } //What APN item this is [FK] 
    public int Qty { get; set; } //How many of this item is stored here

    //public List<ProdDetails> Items { get; set; }//Details from object ProdDetails stored in here [FK]
    
    /// <summary>
    /// Fill in columns in database row with this instance
    /// </summary>
    /// <param name="row"></param>
    public void ToDataRow(ref DataRow row)
    {
        row["StoreId"] = this.Id;
        row["StoreName"] = this.Store;
        row["Item"] = this.Item;
        row["Qty"] = this.Qty;
    }
    // public void ToDataRowChange(ref DataRow row)
    // {
    //     row["StoreId"] = this.Id;
    //     row["Item"] = this.Item;
    //     row["Qty"] = this.Qty;
    // }
}
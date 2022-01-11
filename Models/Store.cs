using System.Text.RegularExpressions;
using System.Data;

namespace Models;

public class Store
{
    //public List<ProdDetails> localInv = new List<ProdDetails>(); //Will store all inventory
    
    public Store(){}

    /// <summary>
    /// Convert store table data to data row
    /// </summary>
    /// <param name="row"></param>
    public Store(DataRow row)
    {
        this.StoreID = (int) row["StoreId"];
        this.StoreName = row["StoreName"].ToString() ?? "";
        this.City = row["City"].ToString() ?? "";
        this.State = row["State"].ToString() ?? "";
        this.SalesTax = (decimal) row["SalesTax"];
    }
    
    public int StoreID { get; set; }//Each different store has an ID [PK]
    public string? StoreName { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public decimal SalesTax { get; set; }//Percent tax rate  [NOT IMPLEMENTED YET]
    
    /// <summary>
    /// Fill in columns in database row with this instance
    /// </summary>
    /// <param name="row"></param>
    public void ToDataRow(ref DataRow row)
    {
        row["StoreId"] = this.StoreID;
        row["StoreName"] = this.StoreName;
        row["City"] = this.City;
        row["State"] = this.State;
        row["SalesTax"] = this.SalesTax;
    }

}
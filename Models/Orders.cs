using System.Data;

namespace Models;

public class Orders
{
    //Each order will make an Orders object, these are formed after the cutomer confirms their purchase
    //public Orders(){}

    public Orders()
    {
        //this.OrderItems = new List<LineItems>();
    }

    /// <summary>
    /// Convert order table data to data row
    /// </summary>
    /// <param name="row"></param>
    public Orders(DataRow row)
    {
        this.OrderId = (int) row["OrderId"];
        this.CustomerId = (int) row["CustomerId"];
        this.StoreId = (int) row["StoreId"];
        //this.GetAPN = (int) row["GetAPN"];
        //this.Name= row["Name"].ToString() ?? "";
        this.OrderDate = (DateTime) row["OrderDate"];
        this.TotalQty = (int) row["Total"];
        this.TotalCost = (decimal) row["TotalCost"];
        this.OrderCompleted = (int) row["OrderCompleted"]; 
    }

    public int OrderId { get; set; } //[PK]
    public int CustomerId { get; set; } //What Customer this Order is for [FK] 
    public int StoreId { get; set; } //What Store this Order is for (this is passed to OrderItems, but here for simplicity) [FK] 
    //public int GetAPN { get; set; } //What Item APN this is [FK] 
    //public string Name { get; set; } //Month for now

    //public DateOnly OrderDate = DateOnly.FromDateTime(DateTime.Now); //Date of purchase
    public DateTime OrderDate = DateTime.Now; //Date of purchase
    // Console.WriteLine($"dateOnlyVar: {dateOfPurchase}");
    //public string DateOfPurchase { get; set; } //Month for now 
    //DateTime OrderDate = DateTime.Now; //some DateTime value, e.g. DateTime.Now;
    //DateOnly dateOfPurchase { get; set; } //some DateTime value, e.g. DateTime.Now;
    //public string? DateOfPurchase { get; set; } 
    public int TotalQty { get; set; }
    public Decimal TotalCost { get; set; }
    //public List<LineItems> OrderItems { get; set; }//List of all items ordered [FK]
    public int OrderCompleted { get; set; } //1/0 because SQL is fussing over bool

    /// <summary>
    /// Fill in columns in database row with this instance
    /// </summary>
    /// <param name="row"></param>
    public void ToDataRow(ref DataRow row)
    {
        //row["OrderId"] = this.OrderId;
        row["CustomerId"] = this.CustomerId;
        row["StoreId"] = this.StoreId; //1 for 1 with Inventory [FK]
        //row["GetAPN"] = this.GetAPN;
        row["OrderDate"] = this.OrderDate;
        row["Total"] = this.TotalQty;
        row["TotalCost"] = this.TotalCost;
        row["OrderCompleted"] = this.OrderCompleted;
    }

}
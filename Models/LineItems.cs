using System.Data;

namespace Models;

public class LineItems
{
    //Items the customer has in their cart, each line being a different item
    //item name   /  qty  /   individual item cost   /    total cost for line
    //...
    //total items   /  total qty  /                      total cost for all item before tax  / after tax
    public LineItems(){}

    /// <summary>
    /// Convert line item table data to data row
    /// </summary>
    /// <param name="row"></param>
    public LineItems(DataRow row)
    {
        this.Id = (int) row["Id"];
        //this.StoreId = (int) row["StoreId"];
        this.InvId = (int) row["InvId"];
        this.OrderId = (int) row["OrderId"];
        this.Qty = (int) row["Qty"];
        this.CostPerItem = (decimal) row["Cost"];
        this.SalesTax = (decimal) row["SalesTax"];
        this.PastOrder = (bool) row["PastOrder"];
    }

    public int Id { get; set; } //[PK]
    //public int Customer { get; set; } //[FK]
    //public int Store { get; set; } //[FK] 
    //public int StoreId { get; set; }//[FK]
    public int InvId { get; set; } //[FK] 
    public int OrderId { get; set; } //[FK]
    public int Qty { get; set; }
    public decimal CostPerItem { get; set; } //Just storing this, but result can be taken from base cost in carried and tax in store
    //Total for line with just be Qty*CostPerItem, 
    //total would not be here, but in the code working with the line item objects in the cart/checkout
    public decimal SalesTax { get; set; }//Just storing this, but result can be taken from tax in store
    public bool PastOrder { get; set; }

    /// <summary>
    /// Fill in columns in database row with this instance
    /// </summary>
    /// <param name="row"></param>
    public void ToDataRow(ref DataRow row)
    {
        //row["Id"] = this.Id;
        //row["StoreName"] = this.StoreId;
        row["InvId"] = this.InvId; //1 for 1 with Inventory [FK]
        row["OrderId"] = this.OrderId;
        row["Qty"] = this.Qty;
        row["Cost"] = this.CostPerItem;
        row["SalesTax"] = this.SalesTax;
        row["PastOrder"] = this.PastOrder;
    }

}
using System.Data;

namespace Models;

public class LineItems
{
    //Items the customer has in their cart, each line being a different item
    //item name   /  qty  /   individual item cost   /    total cost for line
    //...
    //total items   /  total qty  /                      total cost for all item before tax  / after tax

    public LineItems(DataRow row)
    {
        this.Id = (int) row["Id"];
        //this.StoreId = (int) row["StoreId"];
        this.InvId = (int) row["InvId"];
        this.CustomerId = (int) row["CustomerId"];
        this.Name = row["Name"].ToString() ?? "";
        this.Qty = (int) row["Qty"];
        this.CostPerItem = (decimal) row["Cost"];
        this.SalesTax = (decimal) row["SalesTax"];
    }

    public int Id { get; set; } //[PK]
    //public int Customer { get; set; } //[FK]
    //public int Store { get; set; } //[FK] 
    public int StoreId { get; set; }
    public int InvId { get; set; } //[FK] 
    public int CustomerId { get; set; } //[FK]
    public string? Name { get; set; }
    public int Qty { get; set; }
    public decimal CostPerItem { get; set; }
    //Total for line with just be Qty*CostPerItem, 
    //total would not be here, but in the code working with the line item objects in the cart/checkout
    public decimal SalesTax { get; set; }//Percent tax rate

}
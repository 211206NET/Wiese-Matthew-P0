namespace Models;

public class LineItems
{

    //Items the customer has in their cart, each line being a different item
    //item name   /  qty  /   individual item cost   /    total cost for line
    //...
    //total items   /  total qty  /                      total cost for all item before tax  / after tax
    public int APN { get; set; }
    public string? Name { get; set; }
    public int Qty { get; set; }
    public decimal CostPerItem { get; set; }
    //Total for line with just be Qty*CostPerItem, 
    //total would not be here, but in the code working with the line item objects in the cart/checkout
    public decimal SalesTax { get; set; }//Percent tax rate

}
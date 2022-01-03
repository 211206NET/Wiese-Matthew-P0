namespace Models;

public class Orders
{
    //Each order will make an Orders object, these are formed after the cutomer confirms their purchase
    public Orders()
    {
        this.OrderItems = new List<LineItems>();
    }
    public int OrderId { get; set; } //[PK]
    public int CustomerId { get; set; } //What Customer this Order is for [FK] 
    public int StoreId { get; set; } //What Store this Order is for (this is passed to OrderItems, but here for simplicity) [FK] 

    // DateOnly dateOfPurchase = DateOnly.FromDateTime(DateTime.Now); //Date of purchase
    // Console.WriteLine($"dateOnlyVar: {dateOfPurchase}");
    public string DateOfPurchase { get; set; } //Month for now
    public int TotalQty { get; set; }
    public Decimal TotalCost { get; set; }
    public List<LineItems> OrderItems { get; set; }//List of all items ordered [FK]

}
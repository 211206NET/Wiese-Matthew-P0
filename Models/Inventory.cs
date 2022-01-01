namespace Models;

public class Inventory
{
    //One Inventory object stores inventory for an entire store
    public Inventory()
    {
        this.Items = new List<ProdDetails>();
    }

    public int Id { get; set; } // 1 for 1 relationship with Store ID: redundant [PK]
    public int Store { get; set; } //What store this Inventory is for [FK] 

    public List<ProdDetails> Items { get; set; }//Details from object ProdDetails stored in here [FK]
}
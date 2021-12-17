namespace Models;

public class Clay
{
    public Clay(string name, int apn)
    {
        this.Name = name;
        this.APN = apn;
    }
    
    public Clay()
    {
        this.Details = new List<ProdDetails>();
    }


    public string GetName()
    {
        return Name;
    }

    public void SetName(string name)
    {
        this.Name = name;
    }
    public int GetAPN()
    {
        return APN;
    }

    public void SetAPN(int apn)
    {
        this.APN = apn;
    }

    public void ShowDesc()
    {
        //Console.WriteLine($"APN: {APN}");
        if(Details.Count >= APN)//Safeguard to prevent out of range array
        {
            Console.WriteLine($"Name: {Details[APN].Name}, Cost: {Details[APN].Cost}, "+
            $"Weight: {Details[APN].Weight}, Description: {Details[APN].Desc}");
        }
    }

    public void ClayDesc()
    {   
        //PLACE HOLDER to add item details
        ProdDetails newInv = new ProdDetails {
            Name = this.Name,
            Weight = 10,
            Cost = 16,
            Desc = "This green clay is firm yet malleable, making it"+
            " excellent for characters that need to move a lot while keeping their form."
        };
        ProdDetails newInv2 = new ProdDetails {
            Name = this.Name,
            Weight = 14,
            Cost = 20,
            Desc = "This Earth red clay resembles natural potters clay, but like all "+
            "of our plasticine clays, it never hardens. It is very firm, making it an "+
            "excellent choice for fine details."
        };
        ProdDetails newInv3 = new ProdDetails {
            Name = this.Name,
            Weight = 10,
            Cost = 8,
            Desc = "Our pacific blue clay is extremely malleable and has good color."
        };

        this.Details.Add(newInv);
        this.Details.Add(newInv2);
        this.Details.Add(newInv3);
    }

    public string Name { get; set; } 
    public int APN { get; set; } //Assigned Product Number

    
    public List<ProdDetails> Details { get; }
}
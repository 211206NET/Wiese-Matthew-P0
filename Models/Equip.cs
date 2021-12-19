namespace Models;

public class Equip
{
    public Equip(){}

    public Equip(string name)
    {
        this.Name = name;
    }
    public string GetName()
    {
        return Name;
    }

    public void SetName(string name)
    {
        this.Name = name;
    }

    public string EquipDesc()//Not currently used
    {   
        this.Name = "Good Equipment";
        //return = Console.WriteLine($"This is clay called {this.Name}");
        return $"This is clay called {this.Name}";
    }

    public string Name { get; set; } 
    public int ItemType = 2; //0 = clay, 1 = tools, 2 = equip
    public double Weight { get; set; }//How many pounds one unit of this product weigh
    public decimal Cost { get; set; }//Amount the store sells for
    public int APN { get; set; }
    public string Desc { get; set; }//Description of product


}
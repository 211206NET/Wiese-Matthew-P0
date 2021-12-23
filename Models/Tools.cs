namespace Models;

public class Tools
{
    public Tools(){}

    public Tools(string name)
    {
        this.Name = name;
    }
    public string GetName()
    {
        return Name ?? "";
    }

    public void SetName(string name)
    {
        this.Name = name;
    }

    public string ToolsDesc()//Not currently used
    {   
        this.Name = "Good Tools";
        //return = Console.WriteLine($"This is clay called {this.Name}");
        return $"This is clay called {this.Name}";
    }

    public string? Name { get; set; } 
    public int ItemType = 1; //0 = clay, 1 = tools, 2 = equip
    public double Weight { get; set; }//How many pounds one unit of this product weigh
    public decimal Cost { get; set; }//Amount the store sells for
    public int APN { get; set; }
    public string? Desc { get; set; }//Description of product


}
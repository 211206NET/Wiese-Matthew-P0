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


}
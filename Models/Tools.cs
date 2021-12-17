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
        return Name;
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

    public string Name { get; set; } 


}
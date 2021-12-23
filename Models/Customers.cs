namespace Models;

public class Customers
{

    public Customers(int cs, string userName, string pw)
    {
        this.CustNumb = cs;
        this.UserName = userName;
        this.Pass = pw;
    }
    
    public Customers(){}

    public int CustNumb { get; set; } //Customer Number (Unique Number)
    public string? UserName { get; set; } //User Name
    public string? Pass { get; set; } //Password

}
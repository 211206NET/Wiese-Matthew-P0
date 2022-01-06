using System.Data;

namespace Models;

public class Customers
{
    public Customers(){}
    //public Customers(Customers cust){return sasasas}

    // public Customers(int cn, string un, string ps, bool em)
    // {
    //     this.CustNumb = cn;
    //     this.UserName = un;
    //     this.Pass = ps;
    //     this.Employee = em;
    // }
    
    public Customers(DataRow row)
    {
        this.CustNumb = (int) row["CustNumb"];
        this.UserName = row["UserName"].ToString() ?? "";
        this.Pass = row["Pass"].ToString() ?? "";
        this.Employee = (bool) row["Employee"];
    }

    public int CustNumb { get; set; } //Customer Number (Unique Number)  [PK]
    public string? UserName { get; set; } //User Name
    public string? Pass { get; set; } //Password
    public bool Employee { get; set; }

    public void ToDataRow(ref DataRow row)
    {
        row["CustNumb"] = this.CustNumb;
        row["UserName"] = this.UserName;
        row["Pass"] = this.Pass;
        row["Employee"] = this.Employee;
    }

}
namespace Models;

public class ProdDetails
{
    //Product details database
    //This object stores detailed information on each product
    //This stores information for all types of products
    //Product Objects will reference this object to retrive detailed information
    public ProdDetails(){}

    public ProdDetails(string name, double weight, decimal cost, string desc)
    {
         this.Name = name;
         this.Weight = weight;
         this.Cost = cost;
         this.Desc = desc;
    }

    public string Name { get; set; }//Mirrored from product object
    public double Weight { get; set; }//How many pounds one unit of this product weigh
    public decimal Cost { get; set; }//Amount the store sells for

    public string Desc { get; set; }//Description of product

}
using Microsoft.Data.SqlClient;
using System.Data;

namespace DL;

public class DBRepo : IRepo
{

    private string _connectionString;
    public DBRepo(string connectionString)
    {
        _connectionString = connectionString;//File.ReadAllText("connectionString.txt");
        //Console.WriteLine(_connectionString);
        //???
    }
    
    //_____________________________ <> Store <> _____________________________\\
    public List<Store> GetAllStores()
    {
        List<Store> allStoreSQL = new List<Store>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string storeToSelect = "SELECT * FROM Store";
        DataSet CSSet = new DataSet();

        using SqlDataAdapter storeAdapter = new SqlDataAdapter(storeToSelect, connection);    
        storeAdapter.Fill(CSSet, "Store");
        DataTable? StoreTable = CSSet.Tables["Store"];
            
        if(StoreTable != null)
        { 
            foreach(DataRow row in StoreTable.Rows)
            {
                Store storo = new Store(row);
                allStoreSQL.Add(storo);
            }
        }
        return allStoreSQL;


        //Old not great design principle way to do it
        //throw new NotImplementedException();
        // List<Store> allStoreSQL = new List<Store>();
        
        // using(SqlConnection connection = new SqlConnection(_connectionString))
        // {
        //     connection.Open();
            
        //     string queryTxt = "SELECT * FROM Store";
        //     using(SqlCommand cmd = new SqlCommand(queryTxt, connection))
        //     {
        //         using(SqlDataReader reader = cmd.ExecuteReader())
        //         {
        //             while(reader.Read())//One of those design principles
        //             {
        //                 Store storeObj = new Store();
        //                 storeObj.StoreID = reader.GetInt32(0);
        //                 storeObj.StoreName = reader.GetString(1);
        //                 storeObj.City = reader.GetString(2);
        //                 storeObj.State = reader.GetString(3);
        //                 storeObj.SalesTax = reader.GetDecimal(4);
        //                 // Console.WriteLine(reader.GetInt32(0));
        //                 // Console.WriteLine(reader.GetString(1));
        //                 // Console.WriteLine(reader.GetString(2));
        //                 // Console.WriteLine(reader.GetString(3));
        //                 // Console.WriteLine(reader.GetDecimal(4));
        //             }
        //         }
        //     }

        //     connection.Close();
        // }
        // //Console.ReadLine(); //Stop compiler, code is broke beyond this point
        // return allStoreSQL;
    }

    /*Disconnected Architecture saves data in memory
    in Dataset, that persists outside of connection using DataAdapters
    Data Adapters also manage connection for us so we don't have to 
    connect/disconnect manually, Useful for complex manipulation.
    C,U,D ops (create, update, delete)
    */
    public void AddStore(Store storeToAdd)
    {
        DataSet storeSet = new DataSet(); //DataSet has multiple DataTables
        //Check for duplicate first?
        string selectedCmd = "SELECT * FROM Store";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {

            using(SqlDataAdapter dataAdapter = new SqlDataAdapter(selectedCmd, _connectionString))
            {
                
                //We can fill that DataSet using SqlDataAdapter.Fill method
                dataAdapter.Fill(storeSet, "Store");

                DataTable storeTable = storeSet.Tables["Store"];
                // foreach(DataRow row in storeTable.Rows)
                // {
                //     Console.WriteLine(row["StoreId"]);
                // }

                DataRow newRow = storeTable.NewRow();
                // newRow["StoreName"] = storeToAdd.StoreName ?? "";
                // newRow["City"] = storeToAdd.City ?? "";
                // newRow["State"] = storeToAdd.State ?? "";
                // newRow["SalesTax"] = storeToAdd.SalesTax;

                storeToAdd.ToDataRow(ref newRow);
                storeTable.Rows.Add(newRow);

                string insertCmd = $"INSERT INTO Store (StoreName, City, State) VALUES "+
                $"('{storeToAdd.StoreName}','{storeToAdd.City}','{storeToAdd.State}','{storeToAdd.SalesTax}'";

                //Vaguely understand
                //"INSERT INTO employee (FirstName, LastName, DateOfBirth /*etc*/) VALUES (@firstName, @lastName, @dateOfBirth /*etc*/)", con))
                // best practice - always specify the database data type of the column you are using
                // best practice - check for valid values in your code and/or use a database constraint, if inserting NULL then use System.DbNull.Value
                // sc.Parameters.Add(new SqlParameter("@firstName", SqlDbType.VarChar, 200){Value = newEmployee.FirstName ?? 
                // (object) System.DBNull.Value});
                // sc.Parameters.Add(new SqlParameter("@lastName", SqlDbType.VarChar, 200){Value = newEmployee.LastName ?? 
                // (object) System.DBNull.Value});

                //Use @values ^

                //dataAdapter.InsertCommand = new SqlCommand(insertCmd, connection);
                SqlCommandBuilder cmdBuilder= new SqlCommandBuilder(dataAdapter);
                dataAdapter.InsertCommand = cmdBuilder.GetInsertCommand();

                dataAdapter.Update(storeTable);
            }
        }
    }

    //Low Priority
    public void ChangeStoreInfo(int storeIndex, Store changeStoreInfo)//(int storeIndex, string name, string city, string state)
    {
        throw new NotImplementedException();
    }

    //Low Priority
    public void RemoveStore(int StoreToRemove)
    {
        throw new NotImplementedException();
    }


    //_____________________________ <> Inventory <> _____________________________\\
    public List<Inventory> GetAllInventory()
    {
        List<Inventory> allInvSQL = new List<Inventory>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string invToSelect = "SELECT * FROM Inventory";
        DataSet CSSet = new DataSet();

        using SqlDataAdapter invAdapter = new SqlDataAdapter(invToSelect, connection);       
        invAdapter.Fill(CSSet, "Inventory");
        DataTable? InvTable = CSSet.Tables["Inventory"];
            
        
        if(InvTable != null)
        { 
            foreach(DataRow row in InvTable.Rows)
            {
                Inventory invo = new Inventory(row);
                allInvSQL.Add(invo);
            }
        }
        return allInvSQL;
    }

    //Adds an inventory object when a store object is added
    public void AddInventory(Inventory invToAdd)
    {
        throw new NotImplementedException();        
        // DataSet invSet = new DataSet(); //DataSet has multiple DataTables
        // //Check for duplicate first?
        // string selectedCmd = "SELECT * FROM Inventory";
        // using(SqlConnection connection = new SqlConnection(_connectionString))
        // {

        //     using(SqlDataAdapter dataAdapter = new SqlDataAdapter(selectedCmd, _connectionString))
        //     {
                
        //         //We can fill that DataSet using SqlDataAdapter.Fill method
        //         dataAdapter.Fill(invSet, "Inventory");

        //         DataTable invTable = invSet.Tables["Inventory"];

        //         DataRow newRow = invTable.NewRow();

        //         invToAdd.ToDataRow(ref newRow);
        //         invTable.Rows.Add(newRow);

        //         string insertCmd = $"INSERT INTO Inventory (Id, Inventory) VALUES "+ //Wrong
        //         $"('{invToAdd.Id}','{invToAdd.Store}'";

        //         SqlCommandBuilder cmdBuilder= new SqlCommandBuilder(dataAdapter);
        //         dataAdapter.InsertCommand = cmdBuilder.GetInsertCommand();

        //         dataAdapter.Update(invTable);
        //     }
        // }
    }

    //Add item to list of carried items
    public void AddItem(int invIndex, ProdDetails invToAdd)//maybe first parameter becomes storeID
    {
        throw new NotImplementedException();  
        // using(SqlConnection connection = new SqlConnection(_connectionString))
        // {
        //     connection.Open();
        //     string sqlCmd = "INSERT INTO Carried (APN, Name, OnHand, ItemType, Weight, Cost, Descript) VALUES (@p1, @p2, @p3, @p4, @5, @6, @7)";

        //     using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
        //     {
        //         SqlParameter param = (new SqlParameter("@p1", invToAdd.APN));
        //         cmd.Parameters.Add(param);
        //         param = (new SqlParameter("@p2", invToAdd.Name));
        //         cmd.Parameters.Add(param);
        //         param = (new SqlParameter("@p3", invToAdd.OnHand));
        //         cmd.Parameters.Add(param);
        //         param = (new SqlParameter("@p4", invToAdd.ItemType));
        //         cmd.Parameters.Add(param);
        //         param = (new SqlParameter("@p4", invToAdd.Weight));
        //         cmd.Parameters.Add(param);
        //         param = (new SqlParameter("@p4", invToAdd.Cost));
        //         cmd.Parameters.Add(param);
        //         param = (new SqlParameter("@p4", invToAdd.Descr));
        //         cmd.Parameters.Add(param);
        //         //...

        //         cmd.ExecuteNonQuery();
        //     }
        //     connection.Close();
        // }
    }   

    //Remove item to list of carried items
    public void RemoveItem(int apnToRemove)
    {
        throw new NotImplementedException();
        // using(SqlConnection connection = new SqlConnection(_connectionString))
        // {
        //     connection.Open();
        //     string sqlCmd = "DELETE FROM Carried WHERE APN = @invId";
        //     using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
        //     {
        //         cmd.Parameters.AddWithValue("@invId", apnToRemove);

        //         int changed = cmd.ExecuteNonQuery();
        //         //Console.WriteLine($"changed: {changed}, invIndex: {apnToRemove}");
        //     }
        //     connection.Close();
        // }
    }

    //This is for changing the Qty of an item in inventory
    //Final Qty calculations are done prior to calling this and qtyToChange is final value
    public void ChangeInventory(int invId, int qtyToChange)//int storeIndex, Inventory changeInv)//int storeIndex, int apn, int qtyToAdjust
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "UPDATE Inventory SET Qty = @p0 WHERE Id = @p1";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                //SqlParameter param = (new SqlParameter("@p1", qtyToChange));
                cmd.Parameters.AddWithValue("@p0", qtyToChange);
                cmd.Parameters.AddWithValue("@p1", invId);
                //...

                int changed = cmd.ExecuteNonQuery();
                //Console.WriteLine($"changed: {changed}, invIndex: {invIndex}");
            }
            connection.Close();
        }
    }

    //The job of this method is to remove an entire row from inventory when qty equals 0
    public void RemoveInventory(int invId)
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "DELETE FROM Inventory WHERE Id = @invId";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                //SqlParameter param = (new SqlParameter("@p1", qtyToChange));
                cmd.Parameters.AddWithValue("@invId", invId);
                //...

                int changed = cmd.ExecuteNonQuery();
                Console.WriteLine($"changed: {changed}, invIndex: {invId}");
            }
            connection.Close();
        }
    }

    //_____________________________ <> Customers <> _____________________________\\

    public List<Customers> GetAllCustomers()
    {
        //throw new NotImplementedException();
        //string checkForUsername = "SELECT userName FROM Customer WHERE userName=' {s.userName}"; //ref
        List<Customers> allcusSQL = new List<Customers>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string cusToSelect = "SELECT * FROM Customers";
        DataSet CSSet = new DataSet();
        using SqlDataAdapter cusAdapter = new SqlDataAdapter(cusToSelect, connection);    
        cusAdapter.Fill(CSSet, "Customers");
        DataTable? cusTable = CSSet.Tables["Customers"];
            
        if(cusTable != null)
        { 
            foreach(DataRow row in cusTable.Rows)
            {
                Customers custo = new Customers(row);
                allcusSQL.Add(custo);
            }
        }
        //Console.WriteLine("Finished Get Customers");    
        return allcusSQL;
    }
    
    // //Return if this userID is in customer list
    // public Customers GetCurCustomerID(int userId){
    //     List<Customers> allUsers = GetAllCustomers();
    //     foreach(Customers user in allUsers){
    //         if(user.CustNumb == userId){return user;}
    //     }
    //     //If no result
    //     return new Customers();
    // }


    public void AddCustomer(Customers addCust)//int custNum, string userName, string pass
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO Inventory (CustNumb, UserName, Pass, Employee) VALUES (@p1, @p2, @p3, @p4)";

            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                SqlParameter param = (new SqlParameter("@p1", addCust.CustNumb));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p2", addCust.UserName));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p3", addCust.Pass));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p4", addCust.Employee));
                cmd.Parameters.Add(param);
                //...

                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    //_____________________________ <> Carried Items <> _____________________________\\
    public List<ProdDetails> GetAllCarried()
    {
        //throw new NotImplementedException();
        List<ProdDetails> allcarSQL = new List<ProdDetails>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string carToSelect = "SELECT * FROM Carried";
        DataSet CSSet = new DataSet();
        using SqlDataAdapter carAdapter = new SqlDataAdapter(carToSelect, connection);    
        carAdapter.Fill(CSSet, "Carried");
        DataTable? carTable = CSSet.Tables["Carried"];
            
        if(carTable != null)
        { 
            foreach(DataRow row in carTable.Rows)
            {
                ProdDetails carto = new ProdDetails(row);
                allcarSQL.Add(carto);
            }
        }
          
        return allcarSQL;
    }

    public void AddCarried(ProdDetails itemNew)
    {
        //throw new NotImplementedException();
        Console.WriteLine($"DL: Adding a new item to inventory: {itemNew}");

        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO Inventory (APN, Name, OnHand, ItemType, Weight, Cost, Desc) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7)";

            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                SqlParameter param = (new SqlParameter("@p1", itemNew.APN));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p2", itemNew.Name));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p3", itemNew.OnHand));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p4", itemNew.ItemType));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p5", itemNew.Weight));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p6", itemNew.Cost));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p7", itemNew.Descr));
                cmd.Parameters.Add(param);
                //...

                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    //Low Priority
    public void ChangeCarried(int itemNum, string itemName, int itemType, string itemDesc, decimal itemCost, double itemWeight)
    {
        throw new NotImplementedException();
    }

    //Low Priority   Obsolete?
    public void RemoveCarried(int carriedIndexToRemove)
    {
        throw new NotImplementedException();
    }

    //_____________________________ <> Line Items <> _____________________________\\

    public List<LineItems> GetAllLineItem()
    {
        //throw new NotImplementedException();
        List<LineItems> allLISQL = new List<LineItems>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string liToSelect = "SELECT * FROM LineItems";
        DataSet CSSet = new DataSet();

        using SqlDataAdapter liAdapter = new SqlDataAdapter(liToSelect, connection);    
        liAdapter.Fill(CSSet, "LineItems");
        DataTable? liTable = CSSet.Tables["LineItems"];
            
        if(liTable != null)
        { 
            foreach(DataRow row in liTable.Rows)
            {
                LineItems lio = new LineItems(row);
                allLISQL.Add(lio);
            }
        }
        //Console.WriteLine("Finished Get LineItems");    
        return allLISQL;
    }

    //End game
    public void AddLineItem(LineItems newLI)
    {
        throw new NotImplementedException();
    }

    public void RemoveLineItem(int lineItemIndexToRemove)
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "DELETE FROM Carried WHERE Id = @p0";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.AddWithValue("@p0", lineItemIndexToRemove);
                //...

                int changed = cmd.ExecuteNonQuery();
                Console.WriteLine($"changed: {changed}, invIndex: {lineItemIndexToRemove}");//DEBUGGING
            }
            connection.Close();
        }
    }

    //_____________________________ <> Orders <> _____________________________\\

    public List<Orders> GetAllOrders()
    {
        //throw new NotImplementedException();
        //throw new NotImplementedException();
        List<Orders> allOrdSQL = new List<Orders>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string ordToSelect = "SELECT * FROM Orders";
        DataSet CSSet = new DataSet();

        using SqlDataAdapter ordAdapter = new SqlDataAdapter(ordToSelect, connection);    
        ordAdapter.Fill(CSSet, "Orders");
        DataTable? ordTable = CSSet.Tables["Orders"];
            
        if(ordTable != null)
        { 
            foreach(DataRow row in ordTable.Rows)
            {
                Orders ordo = new Orders(row);
                allOrdSQL.Add(ordo);
            }
        }
        //Console.WriteLine("Finished Get Orders");    
        return allOrdSQL;
    }

    public void AddOrder(Orders orderItems)
    {
        //throw new NotImplementedException();
        /*Reference: DateTime dateTimeVariable = //some DateTime value, e.g. DateTime.Now;
        SqlCommand cmd = new SqlCommand("INSERT INTO <table> (<column>) VALUES (@value)", connection);
        cmd.Parameters.AddWithValue("@value", dateTimeVariable);*/
        //Console.WriteLine($"DL: Adding a new order");

        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO Orders (OrderId, CustomerId, StoreId, OrderDate, Total, TotalCost) VALUES (@p1, @p2, @p3, @p4, @p5, @p6)";

            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                SqlParameter param = (new SqlParameter("@p1", orderItems.OrderId));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p2", orderItems.CustomerId));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p3", orderItems.StoreId));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p4", orderItems.OrderDate));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p5", orderItems.TotalQty));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p6", orderItems.TotalCost));
                cmd.Parameters.Add(param);
                //...

                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
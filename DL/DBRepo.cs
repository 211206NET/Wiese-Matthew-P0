using Microsoft.Data.SqlClient;
using System.Data;

namespace DL;

/// <summary>
/// One massive repo that I wanted to split into smaller ones but ran of time.
/// This handles all my communication to the SQL server.
/// </summary>
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
    /// <summary>
    /// Returns a list of all stores
    /// </summary>
    /// <returns>allStoreSQL</returns>
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
    }

    /// <summary>
    /// Adds a store to the database
    /// </summary>
    /// <param name="storeToAdd"></param>
    public void AddStore(Store storeToAdd)
    {    
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO Store (StoreName, City, State, SalesTax) VALUES (@p1, @p2, @p3, @p4)";

            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                SqlParameter param = (new SqlParameter("@p1", storeToAdd.StoreName));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p2", storeToAdd.City));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p3", storeToAdd.State));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p4", storeToAdd.SalesTax));
                cmd.Parameters.Add(param);
                //...

                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    /// <summary>
    /// Allows the manager to make changes to store information
    /// </summary>
    /// <param name="changeStoreInfo"></param>
    public void ChangeStoreInfo(Store changeStoreInfo)//(int storeIndex, string name, string city, string state)
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "UPDATE Store SET StoreName = @p1, City = @p2, State = @p3, SalesTax = @p4 WHERE StoreId = @p0";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.AddWithValue("@p0", changeStoreInfo.StoreID);
                cmd.Parameters.AddWithValue("@p1", changeStoreInfo.StoreName);
                cmd.Parameters.AddWithValue("@p2", changeStoreInfo.City);
                cmd.Parameters.AddWithValue("@p3", changeStoreInfo.State);
                cmd.Parameters.AddWithValue("@p4", changeStoreInfo.SalesTax);
                //...

                int changed = cmd.ExecuteNonQuery();
                Console.WriteLine($"ChangeStoreInfo: changed: {changed}, invIndex: {changeStoreInfo.StoreID}");
            }
            connection.Close();
        }
    }

    /// <summary>
    /// Allows manager to remove a store, all inventory of store, and all orders etc. of that store
    /// The removal of associated inventory, orders, and line items are each handeled with their own methods
    /// </summary>
    /// <param name="StoreToRemove"></param>
    public void RemoveStore(int StoreToRemove)
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "DELETE FROM Store WHERE StoreId = @p0";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.AddWithValue("@p0", StoreToRemove);

                int changed = cmd.ExecuteNonQuery();
                Console.WriteLine($"RemoveStore: changed: {changed}, invIndex: {StoreToRemove}");
            }
            connection.Close();
        }
    }


    //_____________________________ <> Inventory <> _____________________________\\
    /// <summary>
    /// Returns a list od all inventory for all stores from the database
    /// </summary>
    /// <returns>allInvSQL</returns>
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

    /// <summary>
    /// Adds an inventory object to a store
    /// </summary>
    /// <param name="invToAdd"></param>
    public void AddInventory(Inventory invToAdd) //Id, Store, Item, Qty
    {
        //throw new NotImplementedException();     
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "SET IDENTITY_INSERT [dbo].[Inventory] ON  INSERT INTO Inventory (Id, Store, Item, Qty) VALUES (@p1, @p2, @p3, @p4) SET IDENTITY_INSERT [dbo].[Inventory] OFF";

            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                SqlParameter param = (new SqlParameter("@p1", invToAdd.Id));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p2", invToAdd.Store));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p3", invToAdd.Item));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p4", invToAdd.Qty));
                cmd.Parameters.Add(param);
                //...

                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    /// <summary>
    /// Add item to list of carried items
    /// </summary>
    /// <param name="invToAdd"></param>
    public void AddItem(ProdDetails invToAdd)//maybe first parameter becomes storeID  //int invIndex, 
    {
        //throw new NotImplementedException();  
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO Carried (APN, Name, ItemType, Weight, Cost, Descript) VALUES (@p1, @p2, @p4, @5, @6, @7)";

            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                SqlParameter param = (new SqlParameter("@p1", invToAdd.APN));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p2", invToAdd.Name));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p4", invToAdd.ItemType));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p5", invToAdd.Weight));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p6", invToAdd.Cost));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p7", invToAdd.Descr));
                cmd.Parameters.Add(param);
                //...

                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }   

    /// <summary>
    /// Remove item in list of carried items
    /// </summary>
    /// <param name="apnToRemove"></param>
    public void RemoveItem(int apnToRemove)
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "DELETE FROM Carried WHERE APN = @p0";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.AddWithValue("@p0", apnToRemove);

                int changed = cmd.ExecuteNonQuery();
                Console.WriteLine($"Removed item: changed: {changed}, invIndex: {apnToRemove}");
            }
            connection.Close();
        }
    }

    /// <summary>
    /// This is for changing the Qty of an item in inventory 
    /// Final Qty calculations are done prior to calling this and qtyToChange is final value
    /// </summary>
    /// <param name="invId"></param>
    /// <param name="qtyToChange"></param>
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

    /// <summary>
    /// The job of this method is to remove an entire row from inventory when qty equals 0
    /// </summary>
    /// <param name="invId"></param>
    public void RemoveInventory(int invId)
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "DELETE FROM Inventory WHERE Id = @p0"; //Where store is wrong?
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                //SqlParameter param = (new SqlParameter("@p1", qtyToChange));
                cmd.Parameters.AddWithValue("@p0", invId);
                //...

                int changed = cmd.ExecuteNonQuery();
                Console.WriteLine($"Remove Inventory: changed: {changed}, invId: {invId}");
            }
            connection.Close();
        }
    }

    /// <summary>
    /// The job of this method is to remove an entire row from inventory when store is deleted
    /// </summary>
    /// <param name="storePK"></param>
    public void RemoveOrphanInventory(int storePK)
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "DELETE FROM Inventory WHERE Store = @p0"; //Where store is wrong?
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                //SqlParameter param = (new SqlParameter("@p1", qtyToChange));
                cmd.Parameters.AddWithValue("@p0", storePK);
                //...

                int changed = cmd.ExecuteNonQuery();
                Console.WriteLine($"Remove Orphan Inventory: changed: {changed}, storePK: {storePK}");
            }
            connection.Close();
        }
    }

    //_____________________________ <> Customers <> _____________________________\\
    /// <summary>
    /// Returns a list from database of all customers
    /// </summary>
    /// <returns>allcusSQL</returns>
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
    
    /// <summary>
    /// Adds a customer to the database
    /// </summary>
    /// <param name="addCust"></param>
    public void AddCustomer(Customers addCust)//int custNum, string userName, string pass
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "SET IDENTITY_INSERT [dbo].[Customers] ON INSERT INTO Customers (CustNumb, UserName, Pass, Employee) VALUES (@p1, @p2, @p3, @p4) SET IDENTITY_INSERT [dbo].[Customers] OFF";

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
    /// <summary>
    /// Returns a list of all carried items from the database
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Adds a new item to list of current items in database
    /// </summary>
    /// <param name="itemNew"></param>
    public void AddCarried(ProdDetails itemNew)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "SET IDENTITY_INSERT [dbo].[Carried] ON INSERT INTO Carried (APN, Name, ItemType, Weight, Cost, Descript) VALUES (@p1, @p2, @p4, @p5, @p6, @p7) SET IDENTITY_INSERT [dbo].[Carried] OFF";

            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                SqlParameter param = (new SqlParameter("@p1", itemNew.APN));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p2", itemNew.Name));
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

    /// <summary>
    /// Allows manager to make changes to a carried item
    /// </summary>
    /// <param name="changeCarriedItem"></param>
    public void ChangeCarried(ProdDetails changeCarriedItem)//int itemNum, string itemName, int itemType, string itemDesc, decimal itemCost, double itemWeight)
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            Console.WriteLine($"changeCarriedItem.APN: {changeCarriedItem.APN}, changeCarriedItem.Cost: {changeCarriedItem.Cost}");
            connection.Open(); 
            string sqlCmd = "UPDATE Carried SET Name = @p1, ItemType = @p2, Weight = @p3, Cost = @p4, Descript = @p5 WHERE APN = @p0";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.AddWithValue("@p0", changeCarriedItem.APN);
                cmd.Parameters.AddWithValue("@p1", changeCarriedItem.Name);
                cmd.Parameters.AddWithValue("@p2", changeCarriedItem.ItemType);
                cmd.Parameters.AddWithValue("@p3", changeCarriedItem.Weight);
                cmd.Parameters.AddWithValue("@p4", changeCarriedItem.Cost);
                cmd.Parameters.AddWithValue("@p5", changeCarriedItem.Descr);
                //...

                int changed = cmd.ExecuteNonQuery();
                Console.WriteLine($"Change Carried: changed: {changed}, invIndex: {changeCarriedItem}");
            }
            connection.Close();
        }
    }

    //_____________________________ <> Line Items <> _____________________________\\
    /// <summary>
    /// Returns a list of all line items from the database
    /// </summary>
    /// <returns>List of line items</returns>
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
        return allLISQL;
    }

    /// <summary>
    /// Allows manager to add a new line item in the database
    /// </summary>
    /// <param name="newLI"></param>
    public void AddLineItem(LineItems newLI)
    {
        //throw new NotImplementedException();
        //Console.WriteLine($"DL: Adding a new item to lineitem,: {newLI}");//DEBUG

        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO LineItems (InvId, OrderId, Qty, Cost, SalesTax, PastOrder) VALUES (@p2, @p3, @p4, @p5, @p6, @p7)"; //Id, @p1, 

            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                SqlParameter  param = (new SqlParameter("@p2", newLI.InvId));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p3", newLI.OrderId));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p4", newLI.Qty));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p5", newLI.CostPerItem));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p6", newLI.SalesTax));
                cmd.Parameters.Add(param);
                param = (new SqlParameter("@p7", newLI.PastOrder));
                cmd.Parameters.Add(param);
                //...

                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    /// <summary>
    /// Allows manager to remove an item from line items in teh database
    /// </summary>
    /// <param name="lineItemIdToRemove"></param>
    public void RemoveLineItem(int lineItemIdToRemove)
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "DELETE FROM LineItems WHERE InvId = @p0";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.AddWithValue("@p0", lineItemIdToRemove);
                //...

                int changed = cmd.ExecuteNonQuery();
                Console.WriteLine($"RemoveLineItem: changed: {changed}, lineToRemove: {lineItemIdToRemove}");//DEBUGGING
            }
            connection.Close();
        }
    }

    /// <summary>
    /// Removes a line item from database when the associated store is also removed
    /// </summary>
    /// <param name="orderId"></param>
    public void RemoveOrphanLineItem(int orderId)
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "DELETE FROM LineItems WHERE OrderId = @p0";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.AddWithValue("@p0", orderId);
                //...

                int changed = cmd.ExecuteNonQuery();
                Console.WriteLine($"RemoveOrphanLineItem: changed: {changed}, orderId: {orderId}");//DEBUGGING
            }
            connection.Close();
        }
    }

    public void FinalizeLineItem(LineItems finalLineItem)
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "UPDATE LineItems SET PastOrder = @p0 WHERE OrderId = @p1";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.AddWithValue("@p0", finalLineItem.PastOrder);
                cmd.Parameters.AddWithValue("@p1", finalLineItem.OrderId);
                //...

                int changed = cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    
    //_____________________________ <> Orders <> _____________________________\\
    /// <summary>
    /// Returns a list of all orders from the database
    /// </summary>
    /// <returns>list of all orders</returns>
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

    /// <summary>
    /// Adds a new order to the database
    /// </summary>
    /// <param name="orderItems"></param>
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
            string sqlCmd = "SET IDENTITY_INSERT [dbo].[Orders] ON INSERT INTO Orders (OrderId, CustomerId, StoreId, OrderDate, Total, TotalCost, OrderCompleted) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7) SET IDENTITY_INSERT [dbo].[Orders] OFF"; //OrderId, @p1,

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
                param = (new SqlParameter("@p7", orderItems.OrderCompleted));
                cmd.Parameters.Add(param);
                //...

                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    /// <summary>
    /// Adds total quantity and cost to the order and makes the order as completed
    /// This is called after a purchase is confirmed
    /// </summary>
    /// <param name="orderIndex"></param>
    /// <param name="finalDetails"></param>
    public void FinalizeOrder(int orderIndex, Orders finalDetails)
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "UPDATE Orders SET Total = @p0, TotalCost = @p1, OrderCompleted = @p3, OrderDate = @p4 WHERE OrderId = @p2";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.AddWithValue("@p0", finalDetails.TotalQty);
                cmd.Parameters.AddWithValue("@p1", finalDetails.TotalCost);
                cmd.Parameters.AddWithValue("@p2", finalDetails.OrderId);
                cmd.Parameters.AddWithValue("@p3", finalDetails.OrderCompleted);
                cmd.Parameters.AddWithValue("@p4", finalDetails.OrderDate);
                //...

                int changed = cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    /// <summary>
    /// In the event a store is closed, all it's records are deleted
    /// </summary>
    /// <param name="ordersToDelete"></param>
    public void DeleteOrders(int ordersToDelete)
    {
        //throw new NotImplementedException();
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "DELETE FROM Orders WHERE StoreId = @p0";
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                cmd.Parameters.AddWithValue("@p0", ordersToDelete);
                //...

                int changed = cmd.ExecuteNonQuery();
                //Console.WriteLine($"changed: {changed}, ordersToDelete: {ordersToDelete}");//DEBUGGING
            }
            connection.Close();
        }
    }
}
using Xunit;
using Models;
using UI;

namespace UnitTests;
/*
    A test method must meet the following requirements:

    It's decorated with the [TestMethod] attribute.

    It returns void.

    It cannot have parameters.

    For code coverage:
    dotnet test --collect"XPlat Code Coverage"
*/

public class TestModels
{
    [Fact]
    public void TestCustomerCreate()
    {
        //Arrange
        Customers testCustomer = new Customers();
        int custNumb = 1;
        string userName = "testName";
        string pass = "testPass";
        bool employee = false; 

        //Act
        testCustomer.CustNumb = custNumb;
        testCustomer.UserName = userName;
        testCustomer.Pass = pass;
        testCustomer.Employee = employee;

        //Assert
        Assert.Equal(custNumb, testCustomer.CustNumb);
        Assert.Equal(userName, testCustomer.UserName);
        Assert.Equal(pass, testCustomer.Pass);
        Assert.Equal(employee, testCustomer.Employee);
    }

    [Fact]
    public void TestInventoryCreate()
    {
        //Arrange
        Inventory testInventory = new Inventory();
        int id = 1;
        int store = 2;
        int item = 3;
        int qty = 4; 

        //Act
        testInventory.Id = id;
        testInventory.Store = store;
        testInventory.Item = item;
        testInventory.Qty = qty;

        //Assert
        Assert.Equal(id, testInventory.Id);
        Assert.Equal(store, testInventory.Store);
        Assert.Equal(item, testInventory.Item);
        Assert.Equal(qty, testInventory.Qty);
    }
    
    [Fact]
    public void TestLineItemCreate()
    {
        //Arrange
        LineItems testLineItems = new LineItems();
        int id = 1;
        int invId = 2;
        int orderId = 3;
        int qty = 4; 
        decimal cost = 3.7m;
        decimal salesTax = 4.85m; 

        //Act
        testLineItems.Id = id;
        testLineItems.InvId = invId;
        testLineItems.OrderId = orderId;
        testLineItems.Qty = qty;
        testLineItems.CostPerItem = cost;
        testLineItems.SalesTax = salesTax;

        //Assert
        Assert.Equal(id, testLineItems.Id);
        Assert.Equal(invId, testLineItems.InvId);
        Assert.Equal(orderId, testLineItems.OrderId);
        Assert.Equal(qty, testLineItems.Qty);
        Assert.Equal(cost, testLineItems.CostPerItem);
        Assert.Equal(salesTax, testLineItems.SalesTax);
    }

    [Fact]
    public void TestOrdersCreate()
    {
        //Arrange
        Orders testOrders = new Orders();
        int orderId = 1;
        int customerId = 2;
        int storeId = 3;
        //DateTime orderDate = DateTime.Now; //Cannot compare in xunit
        int totalQty = 4;
        decimal totalCost = 8.85m; 
        int orderCompleted = 0; 

        //Act
        testOrders.OrderId = orderId;
        testOrders.CustomerId = customerId;
        testOrders.StoreId = storeId;
        testOrders.TotalQty = totalQty;
        testOrders.TotalCost = totalCost;
        testOrders.OrderCompleted = orderCompleted;

        //Assert
        Assert.Equal(orderId, testOrders.OrderId);
        Assert.Equal(customerId, testOrders.CustomerId);
        Assert.Equal(storeId, testOrders.StoreId);
        Assert.Equal(totalQty, testOrders.TotalQty);
        Assert.Equal(totalCost, testOrders.TotalCost);
        Assert.Equal(orderCompleted, testOrders.OrderCompleted);
    }

    [Fact]
    public void TestProdDetailsCreate()
    {
        //Arrange
        ProdDetails testProdDetails = new ProdDetails();
        int apn = 1;
        string name = "GoodStuff";
        int itemType = 0;
        double weight = 8.85f; 
        decimal cost = 7.8m; 
        string descr = "This stuff is good."; 

        //Act
        testProdDetails.APN = apn;
        testProdDetails.Name = name;
        testProdDetails.ItemType = itemType;
        testProdDetails.Weight = weight;
        testProdDetails.Cost = cost;
        testProdDetails.Descr = descr;

        //Assert
        Assert.Equal(apn, testProdDetails.APN);
        Assert.Equal(name, testProdDetails.Name);
        Assert.Equal(itemType, testProdDetails.ItemType);
        Assert.Equal(weight, testProdDetails.Weight);
        Assert.Equal(cost, testProdDetails.Cost);
        Assert.Equal(descr, testProdDetails.Descr);
    }

    [Fact]
    public void TestStoreCreate()
    {
        /*

        */
        //Arrange
        Store testStore = new Store();
        int storeID = 1;
        string storeName = "GoodStore";
        string city = "TestCity";
        string state = "TestState"; 
        decimal saleTax = 7.8m; 

        //Act
        testStore.StoreID = storeID;
        testStore.StoreName = storeName;
        testStore.City = city;
        testStore.State = state;
        testStore.SalesTax = saleTax;

        //Assert
        Assert.Equal(storeID, testStore.StoreID);
        Assert.Equal(storeName, testStore.StoreName);
        Assert.Equal(city, testStore.City);
        Assert.Equal(state, testStore.State);
        Assert.Equal(saleTax, testStore.SalesTax);
    }

    [Fact]
    public void TestMainMenuCust()
    {
        //Arrange
        

        //Act
        Customers sendCust = new Customers();

        //Assert
        Assert.NotNull(sendCust);
    }
    
    [Fact]
    public void TestMainMenuOrder()
    {
        //Arrange

        //Act
        Orders newOrd = new Orders();

        //Assert
        Assert.NotNull(newOrd);
    }
    
    [Fact]
    public void TestMainMenuLineItems()
    {
        //Arrange

        //Act
        LineItems newLI = new LineItems();

        //Assert
        Assert.NotNull(newLI);
    }
    
    
    [Fact]
    public void TestManagementStore()
    {
        //Arrange

        //Act
        Store storeNew = new Store();

        //Assert
        Assert.NotNull(storeNew);
    }
}
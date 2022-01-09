using DL;

namespace UI;
public static class MenuFactoryUser
{
    public static IMenuUser GetMenuUser(string menuString)
    {
        menuString = menuString.ToLower();
        //This is full dep injection
        // new RestaurantMenu(new RRBL(new FileRepo())).Start();
        
        //Here, I instantiated an implementation of IRepo (FileRepo)

        //We are changing only this line to swap out FileRepo to DBRepo
        //But before we do that, we need to read connection string from file first
        string connectionString = File.ReadAllText("connectionString.txt");
        IRepo repo = new DBRepo(connectionString);
        //next, I instantiated RRBL (an implementation of IBL) and then injected IRepo implementation for IBL/RRBL
        IBL bl = new CSBL(repo);
        //Finally, I instantiate RestaurantMenu that needs an instance that implements Business Logic class
        switch (menuString)
        {
            case "cart":
                return new Cart(bl);
            default:
                Console.WriteLine("User menu broken.");
                return new Cart(bl);
        }
    }
}
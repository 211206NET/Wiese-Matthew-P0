//For Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(@"..\DL\customerLogFile.txt")
    .CreateLogger();

//Start Main Menu
MenuFactory.GetMenu("main").Start();
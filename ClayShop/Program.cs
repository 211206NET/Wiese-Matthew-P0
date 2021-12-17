using UI;
using DL;

IRepo repo = new FileRepo();
CSBL bl = new CSBL(repo);
MainMenu menu = new MainMenu(bl);
menu.Start();

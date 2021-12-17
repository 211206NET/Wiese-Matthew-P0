//Business Library
namespace BL;
public class CSBL : IBL
{

    private IRepo _dl;

    public CSBL(IRepo repo)
    {
        _dl = repo;
    }

    public List<Store> GetAllStores()
    {
        return _dl.GetAllStores();
    }

    public void AddStore(Store storeToAdd)
    {
        _dl.AddStore(storeToAdd);
    }

    public void AddClay(int storeIndex, Clay clayToAdd)
    {
        _dl.AddClay(storeIndex, clayToAdd);
    }
    public void AddTools(int storeIndex, Tools toolsToAdd)
    {
        _dl.AddTools(storeIndex, toolsToAdd);
    }
    public void AddEquip(int storeIndex, Equip equipToAdd)
    {
        _dl.AddEquip(storeIndex, equipToAdd);
    }
}

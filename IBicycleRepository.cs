using BicycleApp_MVC.WebApp.Models;

namespace BicycleApp_MVC.WebApp.Repositories
{
    public interface IBicycleRepository
    {
        void AddBicycle(Bicycle bicycle);
        List<Bicycle> GetAllBicycles();
        Bicycle? GetBicycleById(int id);
        Bicycle? GetBicycleByName(string name);
        void EditBicycle(Bicycle bicycle);
        void DeleteBicycle(int id);
    }
}

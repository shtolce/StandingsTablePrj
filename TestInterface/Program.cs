using Ninject;
using Ninject.Modules;
using StandingTables.BLL;
using StandingTables.BLL.Services;
using StandingTables.BLL.ViewModels;
using StandingTables.DAL.Models;
using StandingTables.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            NinjectModule gameModule = new GameModule();
            NinjectModule serviceModule = new ServiceModule();
            var kernel = new StandardKernel(gameModule, serviceModule);
            var gameService = kernel.Get<IGameService>();
            var pl = new PlayerViewModel();
            pl.PlayerBornDate = DateTime.Now.Subtract(TimeSpan.FromDays(365 * 17));
            pl.PlayerFio = "Vasya";
            pl.PlayerGender = StandingTables.BLL.ViewModels.genderType.female;
            pl.PlayerWeight = 150;
            pl.PlayerHeight = 180;
            pl.City = "Town";
            pl.PlayerCategory = gameService.categoriesVM.FirstOrDefault();
            pl.Club = "de";
            gameService.InputCard(pl);
            foreach (var el in gameService.categoriesVM)
            {
                Console.WriteLine(@"{0}-{1}", el.CategoryValue, el.CategoryAge);
            }
            var pls = gameService.GetByPlayersName("Vasya");
            foreach (var el in pls)
            {
                Console.WriteLine(@"{0}-{1}-{2}-{3}-{4}-{5}-{6}", el.City, el.Club,el.PlayerFio,
                    el.PlayerGender,el.PlayerBornDate,el.PlayerAgeCategory,el.PlayerCategory.CategoryValue);
            }
        

        }
    }
}

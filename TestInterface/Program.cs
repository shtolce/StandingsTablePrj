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
            pl.PlayerWeight = 110;
            pl.PlayerHeight = 112;
            pl.City = "Town";
            pl.PlayerCategory = gameService.categoriesVM.FirstOrDefault();
            pl.Club = "de";
            gameService.InputCard(pl);

            pl = new PlayerViewModel();
            pl.PlayerBornDate = DateTime.Now.Subtract(TimeSpan.FromDays(365 * 17));
            pl.PlayerFio = "Vasya2";
            pl.PlayerGender = StandingTables.BLL.ViewModels.genderType.female;
            pl.PlayerWeight = 110;
            pl.PlayerHeight = 115;
            pl.City = "Town2";
            pl.PlayerCategory = gameService.categoriesVM.FirstOrDefault();
            pl.Club = "de2";
            gameService.InputCard(pl);

            pl = new PlayerViewModel();
            pl.PlayerBornDate = DateTime.Now.Subtract(TimeSpan.FromDays(365 * 17));
            pl.PlayerFio = "Vasya3";
            pl.PlayerGender = StandingTables.BLL.ViewModels.genderType.female;
            pl.PlayerWeight = 110;
            pl.PlayerHeight = 115;
            pl.City = "Town2";
            pl.PlayerCategory = gameService.categoriesVM.FirstOrDefault();
            pl.Club = "de3";
            gameService.InputCard(pl);
            //--------------------
            var players = gameService.GetAllPlayers();
            var selectedItems = from player in players group player by player.PlayerCategory;
            foreach (var el in selectedItems)
            {
                Console.WriteLine(el.Key.CategoryValue+"-"+el.Key.CategoryAge);
                foreach (var s in el)
                {
                    Console.WriteLine(s.PlayerAgeCategory + "-" + s.PlayerFio + "-" + s.PlayerCategory.CategoryValue);
                    gameService.AddStandingRawRecord(s, 1, 1);


                }


            }

            
            //gameService.GenerateStandingsRaw();

            //----------------------
        }
    }
}

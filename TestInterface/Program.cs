using Ninject;
using Ninject.Modules;
using StandingTables.BLL;
using StandingTables.BLL.Services;
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

            gameService.FillCategoriesIfNotExist();


        }
    }
}

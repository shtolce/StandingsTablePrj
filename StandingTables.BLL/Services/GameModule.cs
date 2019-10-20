using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingTables.BLL.Services
{
    class GameModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGameService>().To<GameService>();
        }
    }
}

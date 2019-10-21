using StandingTables.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingTables.BLL.Services
{
    public interface IGameService
    {
        List<CategoryViewModel> categoriesVM { get; set; }

        void FillCategoriesIfNotExist();
        void InitCategories();
        void InputCard(PlayerViewModel player);
        List<PlayerViewModel> GetAllPlayers();
        List<PlayerViewModel> GetByPlayersName(string fio);
        void GenerateStandingsRaw();
    }
}

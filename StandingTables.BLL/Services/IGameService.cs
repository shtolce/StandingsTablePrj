using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingTables.BLL.Services
{
    public interface IGameService
    {
        void FillCategoriesIfNotExist();
        void InitCategories();

    }
}

using StandingTables.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingTables.BLL.Models
{
    public class StandingsRawViewModel
    {
        public int StandingsRawId { get; set; }
        public PlayerViewModel Player { get; set; }
        public CategoryViewModel Category { get; set; }
        public int StandingsRawLevel { get; set; }
        public int StandingsRawPairNum { get; set; }
        public genderType StandingsRawGender { get; set; }
    }
}

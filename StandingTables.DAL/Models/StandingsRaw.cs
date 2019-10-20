using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingTables.DAL.Models
{
    public class StandingsRaw
    {
        public int StandingsRawId { get; set; }
        public Player Player { get; set; }
        public Category Category { get; set; }
        public int StandingsRawLevel { get; set; }
        public int StandingsRawPairNum { get; set; }
        public genderType StandingsRawGender { get; set; }
    }
}

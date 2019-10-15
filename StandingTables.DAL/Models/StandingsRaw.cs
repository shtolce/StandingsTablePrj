using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingTables.DAL.Models
{
    public class StandingsRaw
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public Category Category { get; set; }
        public int Level { get; set; }
        public int PairNum { get; set; }
        public genderType Gender { get; set; }
    }
}

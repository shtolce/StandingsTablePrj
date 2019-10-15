using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingTables.DAL.Models
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public City City { get; set; }
    }
}

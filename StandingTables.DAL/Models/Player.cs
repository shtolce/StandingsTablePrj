using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingTables.DAL.Models
{
    public enum genderType { male, female };
    public class Player
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public DateTime BornDate { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public genderType Gender { get; set; }
        public Club Club { get; set; }
        public int Units
        {
            get
            {
                return (int)Math.Floor(Height + Weight);
            }
        }


    }
}

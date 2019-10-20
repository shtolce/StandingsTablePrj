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
        public int PlayerId { get; set; }
        public string PlayerFio { get; set; }
        public DateTime PlayerBornDate { get; set; }
        public float PlayerHeight { get; set; }
        public float PlayerWeight { get; set; }
        public genderType PlayerGender { get; set; }
        public Club Club { get; set; }
        public int PlayerUnits
        {
            get
            {
                return (int)Math.Floor(PlayerHeight + PlayerWeight);
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingTables.BLL.ViewModels
{
    public enum genderType { male, female };
    public enum ageCategoryType { kid, junior, adult };

    public class PlayerViewModel
    {
        public int PlayerId { get; set; }
        public string PlayerFio { get; set; }
        public DateTime PlayerBornDate { get; set; }
        public float PlayerHeight { get; set; }
        public float PlayerWeight { get; set; }
        public genderType PlayerGender { get; set; }
        public string Club { get; set; }
        public string City { get; set; }
        public CategoryViewModel PlayerCategory { get; set; }

        public int PlayerUnits
        {
            get
            {
                return (int)Math.Floor(PlayerHeight + PlayerWeight);
            }
        }
        public ageCategoryType? PlayerAgeCategory
        {
            get
            {
                TimeSpan diff = DateTime.Now.Subtract(PlayerBornDate);
                int years = (int)Math.Floor(diff.Days / 365.0);
                ageCategoryType? cat = null;
                if (years >= 12 && years <= 15) cat = ageCategoryType.kid;
                if (years >= 16 && years <= 17) cat = ageCategoryType.junior;
                if (years >= 18) cat = ageCategoryType.adult;
                return cat;
            }

        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingTables.DAL.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public ageCategoryType CategoryAge { get; set; }
        public int CategoryValue { get; set; }
    }
}

using AutoMapper;
using StandingTables.BLL.Models;
using StandingTables.BLL.ViewModels;
using StandingTables.DAL.Models;
using StandingTables.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingTables.BLL.Services
{
    class GameService : IGameService
    {
        private IUnitOfWork repo;
        public List<CategoryViewModel> categoriesVM;
        public GameService(IUnitOfWork repo)
        {
            this.repo = repo;
            FillCategoriesIfNotExist();
            var cats = this.repo.Categories.getAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryViewModel>()).CreateMapper();
            categoriesVM = mapper.Map<IEnumerable<Category>, List<CategoryViewModel>>(cats);
        }
        public void InitCategories()
        {
        	int[] catListKid = new int[] { 190, 200, 210, 220, 230, 240, 250, 999 };
            int[] catListJunior = new int[] { 210, 220, 230, 240, 250, 260, 999 };
            int[] catListAdult = new int[] { 230, 240, 250, 260, 270, 999 };
            Category category;
            foreach (var item in catListKid)
            {
                category = new Category
                {
                    CategoryAge = DAL.Models.ageCategoryType.kid,
                    CategoryValue = item
                };
                repo.Categories.Create(category);
            }
            foreach (var item in catListJunior)
            {
                category = new Category
                {
                    CategoryAge = DAL.Models.ageCategoryType.junior,
                    CategoryValue = item
                };
                repo.Categories.Create(category);
            }
            foreach (var item in catListAdult)
            {
                category = new Category
                {
                    CategoryAge = DAL.Models.ageCategoryType.adult,
                    CategoryValue = item
                };
                repo.Categories.Create(category);
            }


        }


        public void FillCategoriesIfNotExist()
        {
            int numOfRecords = repo.Categories.getAll().Count();
            if (numOfRecords > 0) return;
            InitCategories();
        }

        public void InputCard(PlayerViewModel player)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PlayerViewModel, Player>()
            .ForMember("Club",opt=>opt.MapFrom(c=>repo.Clubes.FindByName(c.Club)))
            .ForMember("PlayerCategory", opt => opt.MapFrom(c => repo.Categories.getAll().FirstOrDefault(
                el=>(int)el.CategoryAge==(int)c.PlayerCategory.CategoryAge && el.CategoryValue==c.PlayerCategory.CategoryValue)
                )) ).CreateMapper();
            var playerRepo = mapper.Map<PlayerViewModel,Player>(player);

            //repo.Players


        }



    }
}

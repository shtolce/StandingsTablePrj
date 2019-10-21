using AutoMapper;
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
        public List<CategoryViewModel> categoriesVM { get; set; }


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
        public CategoryViewModel getCategoryVM(int playerUnits, DAL.Models.ageCategoryType? playerAgeCategory)
        {
            int catNum = this.categoriesVM.FindAll(c => c.CategoryAge == (int)playerAgeCategory && c.CategoryValue >= playerUnits)
                .Max(c=>c.CategoryValue);
            if (catNum==999)
                catNum = this.categoriesVM.FindAll(c => c.CategoryAge == (int)playerAgeCategory && c.CategoryValue != 999)
                    .Max(c=>c.CategoryValue);
            return this.categoriesVM.FindAll(c => c.CategoryAge == (int)playerAgeCategory && c.CategoryValue == catNum).FirstOrDefault();
        }

        public List<PlayerViewModel> GetAllPlayers()
        {
            var playersRepo = this.repo.Players.getAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Player, PlayerViewModel>()
            .ForMember("Club",opt=>opt.MapFrom(
                c=>c.Club.ClubName
            ))
            .ForMember("City", opt => opt.MapFrom(
                 c => c.Club.City.CityName
             ))
            .ForMember("PlayerCategory", opt => opt.MapFrom(
                 c => getCategoryVM(c.PlayerUnits,c.PlayerAgeCategory)
             ))
            ).CreateMapper();
            var players = mapper.Map<IEnumerable<Player>, List<PlayerViewModel>>(playersRepo);
            return players;
        }
        public List<PlayerViewModel> GetByPlayersName(string fio)
        {
            var playersRepo = this.repo.Players.FindByName(fio);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Player, PlayerViewModel>()
            .ForMember("Club", opt => opt.MapFrom(
                 c => c.Club.ClubName
             ))
            .ForMember("City", opt => opt.MapFrom(
                 c => c.Club.City.CityName
             ))
            .ForMember("PlayerCategory", opt => opt.MapFrom(
                 c => getCategoryVM(c.PlayerUnits, c.PlayerAgeCategory)
             ))

            ).CreateMapper();
            var players = mapper.Map<IEnumerable<Player>, List<PlayerViewModel>>(playersRepo);

            return players;
        }
        public void InputCard(PlayerViewModel player)
        {
            Player playerRepo = null;
            var tempClub = repo.Clubes.FindByName(player.Club).FirstOrDefault();
            if (tempClub == null)
            {
                City tempCity = repo.Cities.FindByName(player.City).FirstOrDefault();
                if (tempCity == null)
                {
                    tempCity = new City { CityName = player.City??"default" };
                    repo.Cities.Create(tempCity);
                }
                tempClub = new Club
                {
                    City = tempCity,
                    ClubName = player.Club
                };
                repo.Clubes.Create(tempClub);
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PlayerViewModel, Player>()
            .ForMember("Club", opt => opt.MapFrom(c => tempClub))
                ).CreateMapper();
            playerRepo = mapper.Map<PlayerViewModel, Player>(player);
            repo.Players.Create(playerRepo);
        }
        public void GenerateStandingsRaw()
        {
            var cat = this.categoriesVM.Select(c => c.CategoryValue == 180 && c.CategoryAge == 1).FirstOrDefault();
            

            var players = this.GetAllPlayers();



        }

    }
}

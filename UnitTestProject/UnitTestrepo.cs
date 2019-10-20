using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ninject.Modules;
using StandingTables.BLL;
using StandingTables.BLL.Services;
using StandingTables.DAL.Models;
using StandingTables.DAL.Repositories;

namespace UnitTestProject
{
    [TestClass]
    public class DapperRepositoryTest
    {
        [TestMethod]
        public void CityRepoMethods()
        {
            DapperCityRepository repoCity = new DapperCityRepository();
            City c = new City
            {
                CityName = "testNew York"
            };
            repoCity.Create(c);
            Assert.IsNotNull(c.CityId);
            c.CityName = "testNewYork";
            repoCity.Update(c);
            Assert.AreEqual("testNewYork", c.CityName);
            repoCity.get(c.CityId);
            Assert.AreEqual("testNewYork", c.CityName);
            int Id = c.CityId;
            City c1 = new City
            {
                CityName = "testNewYork"
            };
            repoCity.Create(c1);
            Assert.AreEqual(0,c1.CityId);
            IEnumerable<City> cities = repoCity.getAll();
            Assert.AreNotEqual(0, cities.Count());
            Assert.AreEqual("testNewYork", cities.Where(city => city.CityName == "testNewYork").FirstOrDefault().CityName);
            repoCity.Delete(Id);

        }
        [TestMethod]
        public void ClubRepoMethods()
        {
            DapperClubRepository repoClub = new DapperClubRepository();
            DapperCityRepository repoCity = new DapperCityRepository();
            City c = new City
            {
                CityName = "testNew York"
            };
            repoCity.Create(c);
            int cId = c.CityId;

            Club cl = new Club
            {
                ClubName = "testNew York Club",
                City = c
            };
            repoClub.Create(cl);

            Assert.IsNotNull(cl.ClubId);
            cl.ClubName = "testNewYorkClub";
            repoClub.Update(cl);
            Assert.AreEqual("testNewYorkClub", cl.ClubName);
            var getClub = repoClub.get(cl.ClubId);
            Assert.AreEqual("testNewYorkClub", getClub.ClubName);
            Assert.AreEqual("testNew York", getClub.City.CityName);
            int Id = cl.ClubId;
            Club cl1 = new Club
            {
                ClubName = "testNewYorkClub",
                City = c
            };
            repoClub.Create(cl1);
            Assert.AreEqual(0, cl1.ClubId);
            IEnumerable<Club> clubes = repoClub.getAll();
            Assert.AreNotEqual(0, clubes.Count());
            Assert.AreEqual("testNew York", clubes.Where(club => club.ClubName == "testNewYorkClub").FirstOrDefault().City.CityName);
            repoClub.Delete(Id);
            repoCity.Delete(cId);

        }
        [TestMethod]
        public void CategoryRepoMethods()
        {
            DapperCategoryRepository repoCategory = new DapperCategoryRepository();
            Category c = new Category
            {
                CategoryAge = ageCategoryType.junior,
                CategoryValue = 180
            };
            repoCategory.Create(c);
            Assert.IsNotNull(c.CategoryId);
            c.CategoryValue = 185;
            c.CategoryAge = ageCategoryType.junior;
            repoCategory.Update(c);
            Assert.AreEqual(185, c.CategoryValue);
            repoCategory.get(c.CategoryId);
            Assert.AreEqual(185, c.CategoryValue);
            int Id = c.CategoryId;
            Category c1 = new Category
            {
                CategoryAge = ageCategoryType.junior,
                CategoryValue = 185
            };
            repoCategory.Create(c1);
            Assert.AreEqual(0, c1.CategoryId);
            IEnumerable<Category> Categoryes = repoCategory.getAll();
            Assert.AreNotEqual(0, Categoryes.Count());
            Assert.AreEqual(ageCategoryType.junior, Categoryes.Where(Category => Category.CategoryAge == ageCategoryType.junior && Category.CategoryValue==185).FirstOrDefault().CategoryAge);
            repoCategory.Delete(Id);

        }

        [TestMethod]
        public void PlayerRepoMethods()
        {
            DapperPlayerRepository repoPlayer = new DapperPlayerRepository();
            DapperClubRepository repoClub = new DapperClubRepository();
            DapperCityRepository repoCity = new DapperCityRepository();
            City c = new City
            {
                CityName = "testNew York"
                
            };
            repoCity.Create(c);
            int cId = c.CityId;

            Club cl = new Club
            {
                ClubName = "testNew York Club",
                City = c
                

            };
            repoClub.Create(cl);
            var clId = cl.ClubId;


            Player pl = new Player
            {
                PlayerFio = "Vasya",
                PlayerBornDate = DateTime.Now,
                PlayerWeight = 100,
                PlayerGender = genderType.male,
                PlayerHeight = 160,
                Club = cl
            };
            repoPlayer.Create(pl);
            Assert.IsNotNull(pl.PlayerId);
            pl.PlayerFio = "Vasya2";
            repoPlayer.Update(pl);
            Assert.AreEqual("Vasya2", pl.PlayerFio);
            repoPlayer.get(pl.PlayerId);
            Assert.AreEqual("Vasya2", pl.PlayerFio);
            int Id = pl.PlayerId;
            IEnumerable<Player> Playeres = repoPlayer.getAll();
            Assert.AreNotEqual(0, Playeres.Count());
            Assert.AreEqual("Vasya2", Playeres.Where(Player => Player.PlayerFio == "Vasya2").FirstOrDefault().PlayerFio);
            repoPlayer.Delete(Id);
            repoClub.Delete(clId);
            repoCity.Delete(cId);

        }

        [TestMethod]
        public void StandingsRawRepoMethods()
        {
            DapperPlayerRepository repoPlayer = new DapperPlayerRepository();
            DapperClubRepository repoClub = new DapperClubRepository();
            DapperCityRepository repoCity = new DapperCityRepository();
            City city = new City
            {
                CityName = "testNew York"

            };
            repoCity.Create(city);
            int cId = city.CityId;

            Club cl = new Club
            {
                ClubName = "testNew York Club",
                City = city


            };
            repoClub.Create(cl);
            var clId = cl.ClubId;

            Player p = new Player
            {
                PlayerFio = "Vasya",
                PlayerBornDate = DateTime.Now,
                PlayerWeight = 100,
                PlayerGender = genderType.male,
                PlayerHeight = 160,
                Club = cl
            };
            repoPlayer.Create(p);
            int pId = p.PlayerId;
            DapperCategoryRepository repoCategory = new DapperCategoryRepository();
            Category cat = new Category
            {
                CategoryAge = ageCategoryType.junior,
                CategoryValue = 180
            };
            repoCategory.Create(cat);
            int catId = cat.CategoryId;

            DapperStandingsRawRepository repoStandingsRaw = new DapperStandingsRawRepository();
            StandingsRaw c = new StandingsRaw
            {
                Player = p,
                Category =cat,
                StandingsRawLevel = 1,
                StandingsRawPairNum = 1,
                StandingsRawGender = genderType.male
            };
            repoStandingsRaw.Create(c);
            Assert.IsNotNull(c.StandingsRawId);

            int Id = c.StandingsRawId;
            repoStandingsRaw.Delete(Id);
            repoPlayer.Delete(pId);
            repoClub.Delete(clId);
            repoCity.Delete(cId);
            repoCategory.Delete(catId);

        }
        [TestMethod]
        public void BLLtest()
        {
            NinjectModule gameModule = new GameModule();
            NinjectModule serviceModule = new ServiceModule();
            var kernel = new StandardKernel(gameModule, serviceModule);
            var gameService = kernel.Get<IGameService>();
            gameService.FillCategoriesIfNotExist();
            //gameService.InitCategories();



        }






    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2.Helpers
{
    public static class LukoilHelper
    {
        public static List<Entities.Fuel> GetFuels()
        {
            var fuels = new List<Entities.Fuel>()
            {
                new Entities.Fuel()
                {
                    Name = "AI92",
                    Price = 1.00,
                },
                new Entities.Fuel()
                {
                    Name = "AI95",
                    Price = 1.45,
                },
                new Entities.Fuel()
                {
                    Name = "Diesel",
                    Price = 0.90,
                },
            };
            return fuels;
        }

        public static List<Entities.Food> GetFoods()
        {
            var foods = new List<Entities.Food>()
            {
                new Entities.Food()
                {
                    Name = "Hot-Dog",
                    Price = 4,
                },
                new Entities.Food()
                {
                    Name = "Hamburger",
                    Price = 5.40,
                },
                new Entities.Food()
                {
                    Name = "Fries",
                    Price = 7.20,
                },
                new Entities.Food()
                {
                    Name = "Coca-Cola",
                    Price = 4.40,
                },
            };

            return foods;
        }
    }
}


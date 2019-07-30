using System;
using GameStore.Application.Interfaces;
using GameStore.Domain.Models;

namespace GameStore.Persistence.Extensions
{
    public static class GameStoreSeedExtensions
    {
        public static void SeedData(this IGameStoreDbContext context)
        {
            var game1 = new Game
            {
                AddedDateTime = new DateTime(2019, 4, 10),
                Description = "A new third-person action-adventure Star Wars™ " +
                              "title from Respawn Entertainment",
                Name = "Star Wars Jedi Fallen Order",
                Price = 88
            };
            var game2 = new Game
            {
                AddedDateTime = new DateTime(2019, 5, 11),
                Description = "Team with up to three other players in cooperative" +
                              " adventures that reward both combined effort and" +
                              " individual skill. Each player’s choice of javelin" +
                              " exosuit will shape their contribution and strategic role." +
                              " As you explore, you will discover a gripping story filled" +
                              " with unique and memorable characters. Seamless and " +
                              "intelligent matchmaking will ensure you can quickly and" +
                              " easily find other players to adventure alongside.",
                Name = "Anthem",
                Price = 103
            };
            var game3 = new Game
            {
                AddedDateTime = new DateTime(2019, 4, 16),
                Description = "Show ‘em what you’re made of in Apex Legends, " +
                              "a Battle Royale game where contenders " +
                              "from across the Frontier team up to battle for glory," +
                              " fame, and fortune.",
                Name = "Apex Legends",
                Price = 30
            };
            var game4 = new Game
            {
                AddedDateTime = new DateTime(2019, 7, 12),
                Description = "Enter mankind’s greatest conflict with Battlefield™ V as" +
                              " the series goes back to its roots with a never-before-" +
                              "seen portrayal of World War 2. Lead your squad to victory " +
                              "in all-new multiplayer experiences like the multi-map" +
                              " Grand Operations. Fight across the globe in the single" +
                              "-player War Stories campaign. Assemble your Company of" +
                              " customized soldiers, weapons, and vehicles – then take" +
                              " them on an expanding journey through Tides of War. This " +
                              "is the most intense, immersive, and innovative Battlefield" +
                              " yet. You will never be the same.",
                Name = "Battlefield V",
                Price = 108
            };
            var game5 = new Game
            {
                AddedDateTime = new DateTime(2019, 6, 8),
                Description = "Stand out from the crowd with exclusive customization" +
                              " items and receive in-game discounts, Rep bonuses, and" +
                              " five shipments to get your adventure started.Also " +
                              "includes the upcoming Story Mission Pack and the Need for" +
                              " Speed™ Payback Platinum Car Pack including exclusive " +
                              "Platinum Blue Underglow.",
                Name = "Need for Speed payback",
                Price = 22
            };
            var game6 = new Game
            {
                AddedDateTime = new DateTime(2019, 2, 8),
                Description = "To be Most Wanted, you’ll need to outrun the cops," +
                              " outdrive your friends, and outsmart your rivals.",
                Name = "Need for Speed Most Wanted",
                Price = 9
            };
            var game7 = new Game
            {
                AddedDateTime = new DateTime(2019, 1, 8),
                Description = "EXPLORE MASS EFFECT: ANDROMEDA IN THE VAULT",
                Name = "Mass Effect Andromeda",
                Price = 25
            };
            var game8 = new Game
            {
                AddedDateTime = new DateTime(2019, 3, 19),
                Description = "After saving the world of Thedas by closing the Breach," +
                              " your next mission determines the future of the Inquisi" +
                              "tion itself. Win a race against time to defeat a great e" +
                              "vil that could devastate Thedas. In this DLC, playable " +
                              "after the events of Dragon Age: Inquisition, embark on a " +
                              "last adventure to confront the one who started it all.",
                Name = "Dragon Age: Inquisition - Trespasser",
                Price = 14
            };
            var game9 = new Game
            {
                AddedDateTime = new DateTime(2019, 3, 22),
                Description = "In Mass Effect 3, you play as Alliance Marine Commander" +
                              " Shepard and your only hope for saving mankind is to " +
                              "rally the civilizations of the galaxy.",
                Name = "Mass Effect 3",
                Price = 6
            };
            var game10 = new Game
            {
                AddedDateTime = new DateTime(2019, 4, 4),
                Description = "The Star Wars™ Battlefront™ Ultimate Edition has everything" +
                              " fans need to live out their Star Wars™ battle fantasies.",
                Name = "Star Wars™ Battlefront™",
                Price = 21
            };
            var game11 = new Game
            {
                AddedDateTime = new DateTime(2019, 5, 14),
                Description = "In Plants vs. Zombies™ Garden Warfare 2, the zombies have " +
                              "conquered, and the plants are on the attack for the first" +
                              " time in this hilarious, action-packed shooter.",
                Name = "Plants vs Zombies Garden Warfare 2",
                Price = 4
            };
            var game12 = new Game
            {
                AddedDateTime = new DateTime(2019, 5, 18),
                Description = "Live out your fantasy of being a cop and criminal in" +
                              " Battlefield Hardline, EA and Visceral Games new FPS" +
                              " series with TV crime drama inspired singleplayer and" +
                              " multiplayer.",
                Name = "Battlefield Hardline",
                Price = 4
            };

            var category1 = new Category
            {
                Name = "RPG",
                AddedDateTime = new DateTime(2019, 1, 5)
            };
            var category2 = new Category
            {
                Name = "Action",
                AddedDateTime = new DateTime(2019, 1, 12)

            };
            var category3 = new Category
            {
                Name = "Strategy",
                AddedDateTime = new DateTime(2019, 1, 7)
            };
            var category4 = new Category
            {
                Name = "Race",
                AddedDateTime = new DateTime(2019, 1, 4)
            };
            var category5 = new Category
            {
                Name = "Shooting",
                AddedDateTime = new DateTime(2019, 1, 2)
            };
            var category6 = new Category
            {
                Name = "Adventure",
                AddedDateTime = new DateTime(2019, 1, 1)
            };

            context.Games.AddRange(game1, game2, game3, game4, game5,
                game6, game7, game8, game9, game10, game11, game12);
            context.Categories.AddRange(category1, category2, category3,
                category4, category5, category6);

            context.GameCategories.AddRange(
                new GameCategory { Game = game1, Category = category1 },
                new GameCategory { Game = game1, Category = category2 },
                new GameCategory { Game = game1, Category = category6 },
                new GameCategory { Game = game2, Category = category2 },
                new GameCategory { Game = game2, Category = category3 },
                new GameCategory { Game = game3, Category = category2 },
                new GameCategory { Game = game3, Category = category3 },
                new GameCategory { Game = game3, Category = category5 },
                new GameCategory { Game = game4, Category = category3 },
                new GameCategory { Game = game4, Category = category5 },
                new GameCategory { Game = game5, Category = category2 },
                new GameCategory { Game = game5, Category = category4 },
                new GameCategory { Game = game6, Category = category2 },
                new GameCategory { Game = game6, Category = category4 },
                new GameCategory { Game = game7, Category = category1 },
                new GameCategory { Game = game7, Category = category2 },
                new GameCategory { Game = game7, Category = category3 },
                new GameCategory { Game = game7, Category = category5 },
                new GameCategory { Game = game7, Category = category6 },
                new GameCategory { Game = game8, Category = category6 },
                new GameCategory { Game = game8, Category = category1 },
                new GameCategory { Game = game9, Category = category1 },
                new GameCategory { Game = game9, Category = category2 },
                new GameCategory { Game = game9, Category = category3 },
                new GameCategory { Game = game9, Category = category5 },
                new GameCategory { Game = game10, Category = category1 },
                new GameCategory { Game = game10, Category = category6 },
                new GameCategory { Game = game11, Category = category1 },
                new GameCategory { Game = game11, Category = category3 },
                new GameCategory { Game = game12, Category = category5 },
                new GameCategory { Game = game12, Category = category6 }
            );
        }
    }
}

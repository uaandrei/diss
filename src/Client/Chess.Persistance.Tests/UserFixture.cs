using System;
using System.Collections;
using System.Linq;
using Xunit;

namespace Chess.Persistance.Tests
{
    public class UserFixture
    {
        public UserFixture()
        {
            PersistanceManager.Database = "test";
            var user = User.GetUser("user1");
            if (user == null)
            {
                user = User.NewUser("user1", "123");
                user.SaveGame(new SavedGameInfo
                {
                    Id = "g1",
                    LastSaved = new DateTime(2011, 01, 01, 12, 13, 13),
                    Comment = "Kappa",
                    Fen = "r/r/r"
                });
                user.SaveGame(new SavedGameInfo
                {
                    Id = "g2",
                    LastSaved = new DateTime(2022, 01, 11, 22, 22, 22),
                    Comment = "Kappa1",
                    Fen = "rr/nnkr/r"
                });
            }
        }

        [Fact]
        public void Should_Get_TestUser()
        {
            // act
            var user = User.GetUser("user1");

            // assert
            Assert.Equal("123", user.Password);
        }

        [Fact]
        public void Should_Get_SavedGames()
        {
            // act
            var savedGames = User.GetUser("user1").SavedGames;

            // assert
            var g1 = savedGames[0];
            var g2 = savedGames[1];
            Assert.Equal("g1", g1.Id);
            Assert.Equal(new DateTime(2011, 01, 01, 12, 13, 13), g1.LastSaved);
            Assert.Equal("Kappa", g1.Comment);
            Assert.Equal("r/r/r", g1.Fen);
            Assert.Equal("g2", g2.Id);
            Assert.Equal(new DateTime(2022, 01, 11, 22, 22, 22), g2.LastSaved);
            Assert.Equal("Kappa1", g2.Comment);
            Assert.Equal("rr/nnkr/r", g2.Fen);
        }

        [Fact]
        public void Should_AddGame()
        {
            // arrange
            var gameInfo = new SavedGameInfo
            {
                Id = "g66",
                Comment = "EleGiggle",
                Fen = "r/r12/p",
                LastSaved = DateTime.Now
            };
            var user = User.GetUser("user1");
            if (user.SavedGames.Any(g => g.Id == "g66"))
                user.DeleteGame(gameInfo);

            // act
            user.SaveGame(gameInfo);
            user.ForceReloadSavedGames();
            var savedGame = user.SavedGames.First(p => p.Id == "g66");

            // assert
            Assert.Equal(savedGame.Id, gameInfo.Id);
            Assert.Equal(savedGame.Fen, gameInfo.Fen);
            Assert.Equal(savedGame.Comment, gameInfo.Comment);
            Assert.Equal(savedGame.LastSaved.Value.ToShortDateString(), gameInfo.LastSaved.Value.ToShortDateString());
            Assert.Equal(savedGame.LastSaved.Value.ToShortTimeString(), gameInfo.LastSaved.Value.ToShortTimeString());
        }

        [Fact]
        public void Should_UpdateGame()
        {
            // arrange
            var gameInfo = new SavedGameInfo
            {
                Id = "g123",
                Comment = "KappaPride",
                Fen = "8/8/8/k",
                LastSaved = DateTime.Now
            };
            var oldSavedDate = gameInfo.LastSaved;
            var user = User.GetUser("user1");
            user.SaveGame(gameInfo);

            gameInfo.LastSaved = DateTime.Now.AddDays(35);

            // act
            user.SaveGame(gameInfo);
            user.ForceReloadSavedGames();
            var updatedGame = user.SavedGames.First(g => g.Id == gameInfo.Id);

            // assert
            Assert.DoesNotContain(user.SavedGames,
                p => p.LastSaved.Value.ToShortDateString() == oldSavedDate.Value.ToShortDateString() &&
                     p.LastSaved.Value.ToShortTimeString() == oldSavedDate.Value.ToShortTimeString()
            );
            Assert.Equal(gameInfo.Id, updatedGame.Id);
            Assert.Equal(gameInfo.Fen, updatedGame.Fen);
            Assert.Equal(gameInfo.Comment, updatedGame.Comment);
            Assert.Equal(gameInfo.LastSaved.Value.ToShortTimeString(), updatedGame.LastSaved.Value.ToShortTimeString());
            Assert.Equal(gameInfo.LastSaved.Value.ToShortDateString(), updatedGame.LastSaved.Value.ToShortDateString());
        }

        [Fact]
        public void Should_DeleteGame()
        {
            // arrange
            var gameInfo = new SavedGameInfo
            {
                Id = "g908",
                Comment = "KappaPride",
                Fen = "8/8/8/k",
                LastSaved = DateTime.Now
            };
            var user = User.GetUser("user1");
            user.SaveGame(gameInfo);

            // act
            user.DeleteGame(gameInfo);
            user.ForceReloadSavedGames();

            // assert
            Assert.DoesNotContain(user.SavedGames, g => g.Id == gameInfo.Id);
        }
    }
}

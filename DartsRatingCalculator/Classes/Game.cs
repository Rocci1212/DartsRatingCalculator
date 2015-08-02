using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moserware.Skills;

namespace DartsRatingCalculator
{
    public enum GameType
    {
        singles301 = 1,
        doubles301 = 2,
        singles501 = 5,
        doubles501 = 6,
        triples601 = 7,
        singlesCricket = 9,
        doublesCricket = 10
    }

    public class Game
    {
        public GameType _GameType;
        public Squad AwaySquad, HomeSquad;

        public Game(GameType gameType)
        {
            _GameType = gameType;
        }

        public static void InsertNewGame()
        {

        }
    }
}

using Stupide_Vautour.game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stupide_Vautour.players
{
    abstract public class Player
    {
        protected static int numeroPlayer = 0;
        protected int score = 0;

        protected Stack myCards;

        protected Player()
        {
            numeroPlayer++;
            score = 0;
            myCards = new Stack(true);
        }


        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public void addScore(int scoreToAdd){
            score += scoreToAdd;
        }
        public abstract Card play();
    }
}

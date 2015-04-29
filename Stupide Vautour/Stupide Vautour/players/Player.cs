using Stupide_Vautour.game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stupide_Vautour.players
{
    public class Player : ICloneable
    {
        protected static int lastNumeroPlayer = 0;
        protected int score = 0;
        protected int numeroPlayer;
        protected Stack myCards;

        protected Player()
        {
            
            numeroPlayer = lastNumeroPlayer++;
            score = 0;
            myCards = new Stack(true);
            
        }

        public Player(Player p)
        {
            
        score = p.score;
        numeroPlayer = p.numeroPlayer;
        myCards = new Stack(p.myCards);
         
        }


        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public void addScore(int scoreToAdd){
            score += scoreToAdd;
        }
        public virtual Card play(Turn lastTurn, Board board) { return null; }

        public Stack getHand()
        {
            return myCards;
        }

        public int getNumeroPlayer()
        {
            return numeroPlayer;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        internal Player duplicate()
        {
           return new Player(this);
        }
    }
}

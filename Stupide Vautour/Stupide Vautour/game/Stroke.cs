using Stupide_Vautour.players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stupide_Vautour.game
{
    public class Stroke
    {
        Player player;
        Card playerCard;
        Card animalCard;

        public Card AnimalCard
        {
            get { return animalCard; }
            set { animalCard = value; }
        }

        public Stroke(Player p,Card playerCard ,Card animalCard)
        {
            this.player=p;
            this.playerCard = playerCard;
            this.animalCard = animalCard;
        }

        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        public Card PlayerCard
        {
            get { return playerCard; }
            set { playerCard = value; }
        }
    }
}

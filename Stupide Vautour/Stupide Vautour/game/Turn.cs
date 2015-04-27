using Stupide_Vautour.players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stupide_Vautour.game
{
    public class Turn
    {
        Card animalCarte;
        List<Player> players;

        public Turn(List<Player> players, Card animal)
        {
            this.players = players;
            this.animalCarte = animal;
        }

        public Card AnimalCarte
        {
            get { return animalCarte; }
            set { animalCarte = value; }
        }

        public List<Player> Players
        {
            get { return players; }
            set { players = value; }
        }
    }
}

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
        Stack pioche;

        public Turn(List<Player> players, Card animal, Stack pioche)
        {
            this.players = players;
            this.animalCarte = animal;
            this.pioche = pioche;
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

        public Stack Pioche
        {
            get { return pioche; }
            set { pioche = value; }
        }
    }
}

using Stupide_Vautour.players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stupide_Vautour.game
{
    class Turn
    {
        Card animal;
        List<Player> players;

        public Turn(List<Player> players, Card animal)
        {
            this.players = players;
            this.animal = animal;
        }

    }
}

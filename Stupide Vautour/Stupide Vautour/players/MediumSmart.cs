using Stupide_Vautour.game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stupide_Vautour.players
{
    class MediumSmart : Stupid
    {
        public MediumSmart() : base()
        {

        }

        public override Card play(Card animal)
        {

            
            return myCards.getRandomCard();
        }

        public override Card play(Card cardAnimal, Stack pioche)
        {
            int valeurPioche = getValeurCartePioche(cardAnimal, pioche) * 10;

            //return myCards.getRandomCard();
        }
    }
}

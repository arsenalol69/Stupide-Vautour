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

        public override Card play(Card animal, Turn lastTurn)
        {
            if (lastTurn != null)
            {
                double valeurPioche = getValeurCartePioche(animal, lastTurn.Pioche);
                int indIdealCard = (int)(lastTurn.Pioche.getSize() * valeurPioche);
                int indCardToPlay = getRandomGaussian(indIdealCard);

                return myCards.pickCard(indCardToPlay);
            }
            else //le premier Tour lastTurn sera nul 
            {
                return base.play(animal, lastTurn);
            }
            
        }

        public int getRandomGaussian(int nb)
        {
            Random rand = new Random(); //reuse this if you are generating many
            double u1 = rand.NextDouble(); //these are uniform(0,1) random doubles
            double u2 = rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            int stdDev = 1;
            double randNormal =
                         nb + stdDev * randStdNormal; //random normal(mean,stdDev^2)
            return (int)randNormal;
        }
    }
}

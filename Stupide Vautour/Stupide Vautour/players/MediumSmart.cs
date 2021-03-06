﻿using Stupide_Vautour.game;
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

        public override Card play(Turn lastTurn, Board board)
        {

                double valeurPioche = getValeurCartePioche(lastTurn.AnimalCarte, lastTurn.Pioche);
                int indIdealCard = (int)(myCards.getSize() * valeurPioche);
                int indCardToPlay = getRandomGaussian(indIdealCard);
                if (myCards.getSize() <= indCardToPlay)
                {
                    indCardToPlay=myCards.getSize()-1;
                }
                else if (indCardToPlay < 0)
                {
                    indCardToPlay = 0;
                }
                return myCards.pickCard(indCardToPlay);
           
            
        }

        public int getRandomGaussian(int nb)
        {
            Random rand = new Random(); //reuse this if you are generating many
            double u1 = rand.NextDouble(); //these are uniform(0,1) random doubles
            double u2 = rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double stdDev = 1;
            double randNormal =
                         nb + stdDev * randStdNormal; //random normal(mean,stdDev^2)
            return (int)Math.Round(randNormal, 0);
        }
    }
}

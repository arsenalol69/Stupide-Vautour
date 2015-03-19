using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stupide_Vautour.game
{
    class Stack
    {
        public const int NB_CARD = 15;

        ArrayList cardStack;
        static Random random;

        public Stack(int nbCard)
        {
            cardStack = new ArrayList(nbCard);
        }

        public void initializeStack(Boolean isPlayer)
        {
            if (isPlayer)
            {
                for (int i=0; i<NB_CARD; i++)
                {
                    cardStack.Add(new Card(Card.PLAYER, i + 1));
                }
            }
            else
            {
                for(int i=-6; i<15; i++)
                {
                    if (i!=0) cardStack.Add(new Card(Card.ANIMAL, i + 1));
                }
            }
        }

        public Card getRandomCard()
        {
            int indice = random.Next(cardStack.Count);
            Card c = (Card)cardStack[indice];
            cardStack.RemoveAt(indice);
            return c;
        }


    }
}

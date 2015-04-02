﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stupide_Vautour.game
{
    public class Stack
    {
        public const int NB_CARD = 15;

        List<Card> cardStack;
        static Random random;

        public Stack(Boolean isPlayer)
        {
            cardStack = new List<Card>(NB_CARD);
            initializeStack(isPlayer);
            if (random == null)
            {
                random = new Random();
            }
        }

        public void initializeStack(Boolean isPlayer)
        {
            cardStack.Clear();
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

        public List<Card> getCards()
        {
            return cardStack;
        }



        internal int getSize()
        {
            return cardStack.Count;
        }
    }
}

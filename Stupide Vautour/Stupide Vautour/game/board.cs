using Stupide_Vautour.players;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stupide_Vautour.game
{
    class Board
    {

        List<Turn> history;
        Stack stack;
        List<Player> players;

        public Board(List<Player> listPlayers)
        {
            history = new List<Turn>();
            stack = new Stack(false);
            players = listPlayers;  
        }

        public void play(List<Card> cardsPlayed, Card animal)
        {

                  


            if (animal.Force > 0)
            {
                int max = maxCard(new List<Card>(cardsPlayed));
                if (max>=0)
                    players[max].addScore(animal.Force);
            }
            else
            {
                int min = minCard(new List<Card>(cardsPlayed));
                if (min >= 0)
                    players[min].addScore(animal.Force);
            }

            history.Add(new Turn(new List<Player>(players), animal));
                

        }

        private int minCard(List<Card> cards){

            deleteDoublons(cards);

            if (cards.Count <= 0)
                return -1;


            int min = 0, val_min = cards[0].Force;

            for (int i = 1; i < cards.Count; i++)
            {

                if (cards[i].Force < val_min)
                {
                    min = i;
                    val_min = cards[i].Force;
                }

            }

            return min;

         }

        private int maxCard(List<Card> cards)
        {

            deleteDoublons(cards);

            if (cards.Count <= 0)
                return -1;


            int max = 0, val_max = cards[0].Force;

            for (int i = 1; i < cards.Count; i++)
            {

                if (cards[i].Force > val_max)
                {
                    max = i;
                    val_max = cards[i].Force;
                }

            }

            return max;

        }

        private void deleteDoublons(List<Card> cards) {

            int[] listCards = new int[Stack.NB_CARD];

            List<int> canceledCard =new List<int>(); 

            for (int i = 0; i < Stack.NB_CARD; i++)
            {
                listCards[i] = 0;
            }

            foreach (Card c in cards)
            {
                listCards[c.Force - 1]++;
            }

            for (int i = 0; i < Stack.NB_CARD; i++)
            {
                if (listCards[i] > 1)
                    cancelCards(cards, i+1);
            }
        
        }

        private void cancelCards(List<Card> cards, int val)
        {
            foreach (Card c in cards)
            {
                if (c.Force == val)
                {
                    cards.Remove(c);
                    
                }
                    
            }
        }


    }
}

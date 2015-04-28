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
        Stack pioche;
        List<Player> players;

        public Board(List<Player> listPlayers, Stack pioche)
        {
            history = new List<Turn>();
            this.pioche = pioche;
            players = listPlayers;
            reset();
        }

        public void play(List<Card> cardsPlayed, Card animal)
        {

            int winner = getWinner(new List<Card>(cardsPlayed), animal);     


            for (int i = 0; i < players.Count; i++)
            {
                if (cardsPlayed[i].Force == winner)
                    players[i].addScore(animal.Force);
            }
           

            history.Add(new Turn(new List<Player>(players), animal, pioche));
                

        }

        /// <summary>
        /// Retourne le gagnant pour les cartes jouées données en paramètre 
        /// </summary>
        /// <param name="cardsPlayed">Les cartes jouées</param>
        /// <param name="animal">La carte animal tirée</param>
        /// <returns>Le valeur de la carte gagnante et -1 s'il n'y a pas de gagnant</returns>
        public int getWinner(List<Card> cardsPlayed, Card animal)
        {

            int winner = -1;

            if (animal.Force > 0)
            {
                int max = maxCard(new List<Card>(cardsPlayed));
                if (max >= 0)
                {
                    for (int i = 0; i < players.Count; i++)
                    {
                        if (cardsPlayed[i].Force == max)
                            return max;
                    }
                }

            }
            else
            {
                int min = minCard(new List<Card>(cardsPlayed));
                if (min >= 0)
                {
                    for (int i = 0; i < players.Count; i++)
                    {
                        if (cardsPlayed[i].Force == min)
                            return min;
                    }
                }

            }

            return winner;
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

            return val_min;

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

            return val_max;

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
            bool eltRemoved = false;
            foreach (Card c in cards)
            {
                if (c.Force == val)
                {
                    cards.Remove(c);
                    eltRemoved = true;
                    break;
                }
                    
            }
            if (eltRemoved)
                cancelCards(cards, val);
        }

        public Turn getLastTurn()
        {
            try
            {
                return history[history.Count - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur ! Bord.getLastTurn()");
                return null;
            }
            
        }



        public void reset()
        {
            history.Clear();
            history.Add(new Turn(players, null, pioche));
        }
    }
}

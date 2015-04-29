﻿using Stupide_Vautour.game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stupide_Vautour.players
{
    public class Stupid : Player
    {


        public Stupid()
            : base()
        {

        }

        public override Card play(Turn t, Board board)
        {
            return bestCard(t, board);
        }

        protected double[] bestCard2(Turn t, Board board, Player p, Card[] playedCard, double[] proba)
        {
            
            if(playedCard[(numeroPlayer-1)%playedCard.Length]!=null)
            {
                return calculProbaCoups(playedCard, t, board, proba);
            }
            else
            {
                
                for (int i =0; i<p.getHand().getSize(); i++)
                {
                    playedCard[p.getNumeroPlayer()]=(p.getHand().getCard(i));
                    proba = bestCard2(t, board, t.Players[(p.getNumeroPlayer()+1)%t.Players.Count], playedCard, proba);
                }
                return proba;
            }
        }
        public Card bestCard(Turn t, Board board)
        {
            double[] probMax = new double[2];
            int indBestCard = 0;
            probMax[0] = 0; //Somme des probabilité de victoire
            probMax[1] = -1; //Somme de toutes les probabilités
            for (int i = 0; i < getHand().getSize(); i++)
            {
                Card[] playerCard = new Card[t.Players.Count];
                double[] prob = new double[2];
                prob[0] = 0; 
                prob[1] = 0; 
                playerCard[numeroPlayer] = getHand().getCard(i);
                double[] p = bestCard2(t, board, this, playerCard,prob);
                if (prob[1]!=0 && prob[0] / prob[1] > probMax[0] / probMax[1])
                {
                    probMax[0] = prob[0];
                    probMax[1] = prob[1];
                    indBestCard = i;
                }
            }
            return getHand().getCard(indBestCard);
        }

        protected double[] calculProbaCoups(Card[] playerCards, Turn t, Board board, double[] prob)
        {
            int winnerCard = board.getWinner(new List<Card>(playerCards), t.AnimalCarte);
            int indWinner = -1;
            double pCoups = 1;
            double[] p = new double[2];
            for (int i = 0; i < playerCards.Length; i++) if (playerCards[i].Force == winnerCard) indWinner = i;
                for (int i = 0; i < t.Players.Count; i++ )
                {
                    if (i!=numeroPlayer)
                    {
                        pCoups *= chanceDetreUtilise(t, new Stroke(t.Players[i], playerCards[i], t.AnimalCarte));
                    }
                }

                if (indWinner == numeroPlayer)
                {
                    p[0] = pCoups+prob[0];
                }
                p[1] = prob[1] + pCoups;
                if (p[1]<0)
                {
                    int a =1;
                }
                return p;
            
        }

        /// <summary>
        /// Calcule les chances qu'un coup soit utilisé par un joueur
        /// </summary>
        /// <param name="coups">Objet de type Stroke contenant la carte joué et le joueur qui la joue et la pioche</param>
        /// <returns>Entier compris entre 0 et 1 qui reprèsent la probabilité que le coups soit joué</returns>
        protected double chanceDetreUtilise(Turn t, Stroke coups)
        {
            double valeurCarte = getValeurCarte(coups);
            double valeurPioche = getValeurCartePioche(coups.AnimalCard, t.Pioche);
            double valeurJoueur = getPositionJoueur(t, coups.Player); //plus le joueur a peu de points, plus son comportement est offensive
            double proximiteCoups = -Math.Abs(valeurCarte - valeurPioche); //Plus la var est grande plus le coups est proche de la carte Animal
            return proximiteCoups*(1-valeurJoueur);
            
        }

        protected double getPositionJoueur(Turn t, Player P)
        {
            int scoreMax = t.Players[0].Score;
            for (int i =0; i<t.Players.Count; i++)
            {
                if (t.Players[i].Score > scoreMax) scoreMax = t.Players[0].Score;
            }
            return scoreMax==0 ? 0.001 : P.Score/scoreMax;
        }

        protected bool isFirst(List<Player> pList, Player player)
        {
            int maxScore = pList[0].Score;
            for(int i = 1; i<pList.Count; i++)
            {
                if (pList[i].Score > maxScore) maxScore = pList[i].Score;
            }
            if (maxScore==player.Score) return true;
            else return false;
        }

        protected bool isLast(List<Player> pList, Player player)
        {
            int min = pList[0].Score;
            for (int i = 1; i < pList.Count; i++)
            {
                if (pList[i].Score <min) min = pList[i].Score;
            }
            if (min == player.Score) return true;
            else return false;
        }

        /// <summary>
        /// Calcule la valeur de la carte en fonction de la main du joueur
        /// </summary>
        /// <param name="coups">Objet de type Turn contenant la carte joué et le joueur qui la joue et la carte piochée</param>
        /// <returns>Entier représentant la valeur de la carte</returns>
        protected double getValeurCarte(Stroke coup)
        {
            double force = coup.PlayerCard.Force; //Force de la carte
            //refaire la fonction findPositionCard
            int posMain = coup.Player.getHand().findPositionCard(coup.PlayerCard)+1; //Position de la carte dans la main du joueur
            force = (force * posMain) / coup.Player.getHand().getSize(); //Force recalculée par rapport par rapport à la position
            return force;
        }

        /// <summary>
        /// Calcule la valeur de la carte pioché
        /// </summary>
        /// <param name="coups">Objet de type Turn contenant la carte joué et le joueur qui la joue et la carte piochée</param>
        /// <returns>Entier représentant la valeur de la carte</returns>
        protected double getValeurCartePioche(Card carte, Stack pioche)
        {
            double force=0;
            double valeurCarte= carte.Force < 0 ? carte.Force * -2 :  carte.Force;
            List<Double> listPiocheValeur = new List<Double>();
            for (int i=0; i<pioche.getCards().Count();i++){
                force = pioche.getCards()[i].Force;
                if (force < 0){
                    force = Math.Abs(force) * 2;
                }
                listPiocheValeur.Add(force);
            }
            listPiocheValeur.Sort();

            int posPioche = listPiocheValeur.IndexOf(valeurCarte)+1;
            
             // correspond à la postition partant de -5 à 10
            force = force * ((double)posPioche / (double)listPiocheValeur.Count()); //utile pour faire par exemple posPioche=15 et pioche.getSize()=15 !
            return force/getForceMax(pioche);
        }

        private int getForceMax(Stack pioche)
        {
            int valeur = pioche.getCard(0).Force < 0 ? pioche.getCard(0).Force * -2 : pioche.getCard(0).Force;
            int forceMax = valeur;
            for (int i=1; i<pioche.getSize(); i++)
            {
                valeur = pioche.getCard(i).Force < 0 ? pioche.getCard(i).Force * -2 : pioche.getCard(i).Force;
                if (valeur > forceMax) forceMax = valeur ;
            }
            return forceMax;
        }
    }
}

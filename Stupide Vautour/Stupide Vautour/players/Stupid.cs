using Stupide_Vautour.game;
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

        protected double[] bestCard2(Turn t, Board board, Player p, List<Card> playedCard, double[] proba)
        {
            
            if(playedCard[t.Players.Count-1]!=null)
            {
                return calculProbaCoups(playedCard, t, board, proba);
            }
            else
            {
                
                for (int i =0; i<p.getHand().getSize(); i++)
                {
                    playedCard[i]=(p.getHand().getCard(i));
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
                List<Card> playerCard = new List<Card>(t.Players.Count);
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

        protected double[] calculProbaCoups(List<Card> playerCards, Turn t, Board board, double[] prob)
        {
            int winnerCard = board.getWinner(playerCards, t.AnimalCarte);
            int indWinner = 0;
            double pCoups = 1;
            double[] p = new double[2];
            for (int i = 0; i < playerCards.Count; i++) if (playerCards[i].Force == winnerCard) indWinner = i;
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
            double proximiteCoups = 1-Math.Abs(valeurCarte - valeurPioche) / valeurPioche; //Plus la var est grande plus le coups est proche de la carte Animal
            return proximiteCoups*(1-valeurJoueur);
            
        }

        protected double getPositionJoueur(Turn t, Player P)
        {
            int scoreMax = t.Players[0].Score;
            for (int i =0; i<t.Players.Count; i++)
            {
                if (t.Players[i].Score > scoreMax) scoreMax = t.Players[0].Score;
            }
            return P.Score/scoreMax;
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
            int force = coup.PlayerCard.Force; //Force de la carte
            int posMain = coup.Player.getHand().findPositionCard(coup.PlayerCard); //Position de la carte dans la main du joueur
            force = force * (posMain / coup.Player.getHand().getSize()); //Force recalculée par rapport par rapport à la position
            return force;
        }

        /// <summary>
        /// Calcule la valeur de la carte pioché
        /// </summary>
        /// <param name="coups">Objet de type Turn contenant la carte joué et le joueur qui la joue et la carte piochée</param>
        /// <returns>Entier représentant la valeur de la carte</returns>
        protected double getValeurCartePioche(Card carte, Stack pioche)
        {
            int force;
            if (carte.Force > 0) force = carte.Force;
            else force = Math.Abs(carte.Force)*2;
            int posPioche = pioche.findPositionCard(carte);
            force = force * (posPioche / pioche.getSize());
            return force/getForceMax(pioche);
        }

        private int getForceMax(Stack pioche)
        {
            int forceMax = pioche.getCard(0).Force;
            for (int i=1; i<pioche.getSize(); i++)
            {
                if (pioche.getCard(i).Force > forceMax) forceMax = pioche.getCard(i).Force ;
            }
            return forceMax;
        }
    }
}

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

        public override Card play(Card animal, Turn t)
        {
            return myCards.getRandomCard();
        } 

        public int carteGagnee(Card c, Turn t)
        {
            for (int i = 0; i < t.Players[0].getHand().getSize(); i++)
            {
                for (int j=0; j<t.Players[1].getHand().getSize(); j++)
                {
                    if (t.Players.Count >= 3)
                    {
                        for (int k = 0; k < t.Players[2].getHand().getSize(); k++)
                        {
                            if (t.Players.Count >= 4)
                            {
                                for (int l = 0; l < t.Players[2].getHand().getSize(); l++)
                                {
                                    //test de gagne (à metttre dans les else aussi...
                                }
                            }
                        }
                    }           
                }
            }
            return 0;
        }

        /// <summary>
        /// Calcule les chances qu'un coup soit utilisé par un joueur
        /// </summary>
        /// <param name="coups">Objet de type Stroke contenant la carte joué et le joueur qui la joue et la pioche</param>
        /// <returns>Entier compris entre 0 et 1 qui reprèsent la probabilité que le coups soit joué</returns>
        protected double chanceDetreUtilise(Turn t, Stroke coups, Stack pioche)
        {
            double valeurCarte = getValeurCarte(coups);
            double valeurPioche = getValeurCartePioche(coups.AnimalCard, pioche);
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
            int forceMax = pioche.pickCard(0).Force;
            for (int i=1; i<pioche.getSize(); i++)
            {
                if (pioche.pickCard(i).Force > forceMax) forceMax = pioche.pickCard(i).Force ;
            }
            return forceMax;
        }
    }
}

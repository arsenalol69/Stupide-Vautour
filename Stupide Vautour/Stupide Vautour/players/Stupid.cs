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

        public int carteGagnee(int probabilite, Turn t)
        {
            for (int i = 0; i < myCards.getSize(); i++)
            {

            }

                return 0;
        }

        /// <summary>
        /// Calcule les chances qu'un coup soit utilisé par un joueur
        /// </summary>
        /// <param name="coups">Objet de type Stroke contenant la carte joué et le joueur qui la joue et la pioche</param>
        /// <returns>Entier compris entre 0 et 1 qui reprèsent la probabilité que le coups soit joué</returns>
        protected int chanceDetreUtilise(Turn t, Stroke coups, Stack pioche)
        {
            int valeurCarte = getValeurCarte(coups);
            int valeurPioche = getValeurCartePioche(coups.AnimalCard, pioche);
            int valeurJoueur = getPositionJoueur(t, coups.Player); //plus le joueur a peu de points, plus son comportement est offensive
            int proximiteCoups = 1-Math.Abs(valeurCarte - valeurPioche) / valeurPioche; //Plus la var est grande plus le coups est proche de la carte Animal
            return proximiteCoups*(1-valeurJoueur);
            
        }

        protected int getPositionJoueur(Turn t, Player P)
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
        protected int getValeurCarte(Stroke coup)
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
        protected int getValeurCartePioche(Card carte, Stack pioche)
        {
            int force;
            if (carte.Force > 0) force = carte.Force;
            else force = Math.Abs(carte.Force)*2;
            int posPioche = pioche.findPositionCard(carte);
            force = force * (posPioche / pioche.getSize());
            return force/10;
        }
    }
}

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

        /// <summary>
        /// Constructeur d'un joueur Stupide
        /// </summary>
        public Stupid()
            : base()
        {

        }

        /// <summary>
        /// Methode permettant de jouer une carte : l'IA stupide fera tout à chaque fois pour avoir la carte !
        /// </summary>
        /// <param name="t"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public override Card play(Turn t, Board board)
        {
            return bestCard(t, board,1);
        }

        /// <summary>
        /// Méthode récursive permettant de tester les combinaisons et retourne la probilité de victoire de la combinaison
        /// </summary>
        /// <param name="t">Tour en jeu</param>
        /// <param name="board">Plateau de jeu</param>
        /// <param name="p">Joueur qui joue</param>
        /// <param name="playedCard">Cartes jouées</param>
        /// <param name="proba">Probabilité que le coups gagne</param>
        /// <param name="nbCard">Nombre de carte déterminée</param>
        /// <returns></returns>
        protected double[] testerCombinaison(Turn t, Board board, Player p, Card[] playedCard, double[] proba, int nbCard)
        {
            //Condition d'arrêt de la boucle récursive
            if(nbCard==playedCard.Length)
            {
                //On calcule la proba de la combinaison
                double[] resultat = calculProbaCoups(playedCard, t, board, proba);
                return resultat;
            }
            else
            {
                 Card[] nouvCombinaison = new Card[playedCard.Length];
                    for (int j=0; j<nouvCombinaison.Length; j++)
                    {
                        nouvCombinaison[j] = playedCard[j];
                    }
                for (int i =0; i<p.getHand().getSize(); i++)
                {
                   //On teste les combinaisons restantes
                    nouvCombinaison[p.getNumeroPlayer()]=(p.getHand().getCard(i));
                    proba = testerCombinaison(t, board, t.Players[(p.getNumeroPlayer() + 1) % t.Players.Count], nouvCombinaison, proba, nbCard + 1);
                }
                //On retourne la proba calculée
                return proba;
            }
        }

        /// <summary>
        /// Permet d'obtenir la meilleur carte avec une probabilité la plus proche d'une variable mis en paramètre
        /// </summary>
        /// <param name="t">Tour en jeu</param>
        /// <param name="board">Le plateau de jeu</param>
        /// <param name="volonte">Force de volonté de remporter la carte (entre 0 et 1)</param>
        /// <returns></returns>
        public Card bestCard(Turn t, Board board, double volonte)
        {
            double[] probMax = new double[2];
            int indBestCard = 0;
            probMax[0] = 0; //Somme des probabilité de victoire
            probMax[1] = -1; //Somme de toutes les probabilités
            for (int i = 0; i < getHand().getSize(); i++)
            {
                Card[] playerCard = new Card[t.Players.Count];
                double[] p = new double[2];
                double[] prob = new double[2];
                p[0] = 0; 
                p[1] = 0; 
                //On récupére la carte du joueur
                playerCard[numeroPlayer] = getHand().getCard(i);
                //On test toutes les combinaisons possibles avec cette carte
                prob = testerCombinaison(t, board, t.Players[(numeroPlayer + 1) % t.Players.Count], playerCard, p, 1);
                double probFinal = prob[0] / prob[1];
                //On vérifie que la proba finale est la plus proche de la volonté
                if (prob[1]!=0 && Math.Abs(probFinal-volonte) < Math.Abs(probMax[0]/probMax[1]-volonte))
                {
                    probMax[0] = prob[0];
                    probMax[1] = prob[1];
                    indBestCard = i;
                }
            }
            //On retourne alors la meilleure carte
            return getHand().getCard(indBestCard);
        }

        /// <summary>
        /// Cacul la probabilité qu'un coup soit joué par les opposants
        /// </summary>
        /// <param name="playerCards">Cartes joués par les joueurs</param>
        /// <param name="t">Tour en jeu</param>
        /// <param name="board">Plateau de jeu</param>
        /// <param name="prob">Probabilité de départ : p[0] numérateur, p[1] dénominateur</param>
        /// <returns></returns>
        protected double[] calculProbaCoups(Card[] playerCards, Turn t, Board board, double[] prob)
        {
            //On calcule le gagnant de la carte sur le tour
            int winnerCard = board.getWinner(new List<Card>(playerCards), t.AnimalCarte);
            int indWinner = -1;
            double pCoups = 1;
            double[] p = new double[2];
            //On cherche l'indice joueur ayant gagné
            for (int i = 0; i < playerCards.Length; i++) if (playerCards[i].Force == winnerCard) indWinner = i;
            //On calcule les différentes probabilité pour chaque joueur
            for (int i = 0; i < t.Players.Count; i++ )
            {
                if (i!=numeroPlayer)
                {
                    pCoups += chanceDetreUtilise(t, new Stroke(t.Players[i], playerCards[i], t.AnimalCarte));
                }
            }
            p[0] = prob[0];
            //Si on est gagnant, on incrément le numérateur
            if ((indWinner == numeroPlayer && t.AnimalCarte.Force > 0) || (indWinner != numeroPlayer && t.AnimalCarte.Force < 0))
            {
                p[0] += pCoups;
            }
            p[1] = prob[1] + pCoups;
           //On obtient le nombre de cas favorable sur le nombre de cas possibles
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
            return proximiteCoups*(valeurJoueur);
            
        }


        /// <summary>
        /// Obtient la position du joueur en fonction des autres
        /// </summary>
        /// <param name="t"></param>
        /// <param name="P"></param>
        /// <returns></returns>
        protected double getPositionJoueur(Turn t, Player P)
        {
            int scoreMax = 0;
            for (int i = 0; i < t.Players.Count; i++)
            {
                if (t.Players[i].Score > scoreMax && i != P.getNumeroPlayer()) scoreMax = t.Players[i].Score;
            }
            return scoreMax == 0 ? 1 : 1 + (scoreMax - Score) / scoreMax;
        }


       


        /// <summary>
        /// Calcule la valeur de la carte en fonction de la main du joueur
        /// </summary>
        /// <param name="coups">Objet de type Turn contenant la carte joué et le joueur qui la joue et la carte piochée</param>
        /// <returns>Entier représentant la valeur de la carte</returns>
        protected double getValeurCarte(Stroke coup)
        {
            double force = coup.PlayerCard.Force; //Force de la carte
            int posMain = coup.Player.getHand().findPositionCard(coup.PlayerCard)+1; //Position de la carte dans la main du joueur
            force = (force * posMain) / coup.Player.getHand().getSize(); //Force recalculée par rapport par rapport à la position
            return force/getForceMax(coup.Player.getHand());
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
            //Calcule la nouvelle valeur des cartes
            for (int i=0; i<pioche.getCards().Count();i++){
                force = pioche.getCards()[i].Force;
                //Si la carte est négatif, on multiplie par 2
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

        /// <summary>
        /// On  calcule la force maximum qu'on peut avoir avec la pioche
        /// </summary>
        /// <param name="pioche"></param>
        /// <returns></returns>
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

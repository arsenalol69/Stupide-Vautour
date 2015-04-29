using Stupide_Vautour.game;
using Stupide_Vautour.players;
using Stupide_Vautour.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stupide_Vautour
{
    public partial class MainBoard : Form
    {
        Board board;
        List<Player> players;
        Stack piocheAnimal;
        Card animalCard;
        List<Card> choiceRoundCard;
        List<Label> labelScore;
        List<PictureBox> panelChoice;
        const int CARD_HEIGHT = 505;
         const int CARD_WIDTH = 364;
         int numHuman;

         private List<System.Windows.Forms.PictureBox> handCard;

        public MainBoard()
        {
            InitializeComponent();

            //Ajout des joueurs
            players = new List<Player>();
            //players.Add(new Human());
            players.Add(new VerySmart());
            players.Add(new Stupid());
            //players.Add(new Human());
            
            //Recherche de l'humain :
            numHuman = -1;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] is Human)
                    numHuman = i;
            }


            //Label des scores
            labelScore = new List<Label>();
            labelScore.Add(labelScore1);
            labelScore.Add(labelScore2);
            labelScore.Add(labelScore3);
            labelScore.Add(labelScore4);
            piocheAnimal = new Stack(false);

            //Création du board
            board = new Board(players, piocheAnimal);

            //Carte de jeu
            panelChoice = new List<PictureBox>();
            panelChoice.Add(panelCarteJoue1);
            panelChoice.Add(panelCarteJoue2);
            panelChoice.Add(panelCarteJoue3);
            panelChoice.Add(panelCarteJoue4);
            for (int i = 0; i < panelChoice.Count; i++)
            {
                panelChoice[i].BackgroundImage = global::Stupide_Vautour.Properties.Resources.carte;
                panelChoice[i].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                panelChoice[i].Size = new System.Drawing.Size(CARD_WIDTH/3, CARD_HEIGHT/3);
            }

            //Carte de la main du joueur
            handCard = new List<PictureBox>();
            handCard.Add(pictureBox1);
            handCard.Add(pictureBox2);
            handCard.Add(pictureBox3);
            handCard.Add(pictureBox4);
            handCard.Add(pictureBox5);
            handCard.Add(pictureBox6);
            handCard.Add(pictureBox7);
            handCard.Add(pictureBox8);
            handCard.Add(pictureBox9);
            handCard.Add(pictureBox10);
            handCard.Add(pictureBox11);
            handCard.Add(pictureBox12);
            handCard.Add(pictureBox13);
            handCard.Add(pictureBox14);
            handCard.Add(pictureBox15);
            for (int i = 0; i < Stack.NB_CARD && numHuman!=-1; i++)
            {
                handCard[i].BackgroundImage = global::Stupide_Vautour.Properties.Resources.carte;
                handCard[i].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                handCard[i].Image = Image.FromFile("Resources/carteJoueur"+ (numHuman+1) + (i+1) + ".png");
                handCard[i].Click += new System.EventHandler(this.choiceCard);
                handCard[i].Tag = i + 1;
            
            }

            //Si l'humain ne joue pas, on n'affiche pas les cartes
            if (numHuman == -1)
            {
                showHandCards(false);
            }

        }

        /// <summary>
        /// Est appelé lorsque le joueur clique sur une carte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void choiceCard(object sender, EventArgs e)
        {
            if (!choiceHuman)
                return;

            choiceHuman = false;
            PictureBox pictureCard = (PictureBox)sender;
            int choixHumain = (int)pictureCard.Tag;
            jouerTour(choixHumain);

            
            choiceHuman = true;
        }

        /// <summary>
        /// Permet de jouer le tour
        /// </summary>
        /// <param name="choixHumain">Correspond au choix de l'humain. Cette valeur est ignorée si aucun humain ne joue</param>
        private void jouerTour(int choixHumain)
        {
            choiceRoundCard = new List<Card>(players.Count);
            List<Player> playersBefore = Board.duplicatePlayers(players);

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] is Human)
                {

                    handCard[choixHumain - 1].Hide();
                    choiceRoundCard.Add(players[i].getHand().pickCard(players[i].getHand().findPositionCard(new Card(Card.PLAYER, choixHumain))));
                }
                else
                {
                    Turn t = new Turn(playersBefore, animalCard, piocheAnimal);
                    choiceRoundCard.Add(players[i].play(t, board));

                }
            }
            for (int i = 0; i < players.Count; i++)
            {
                panelChoice[i].Image = Image.FromFile("Resources/carteJoueur" + ((int)(i + 1)) + choiceRoundCard[i].Force.ToString() + ".png");
            }
            this.Refresh();
            board.play(choiceRoundCard, animalCard);
            System.Threading.Thread.Sleep(5000);
            updateViewPlayers();
            piocheAnimal.pickCard(piocheAnimal.findPositionCard(animalCard));
            //On enlève les cartes jouées
            for (int i = 0; i < players.Count; i++)
            {
                panelChoice[i].Image = Image.FromFile("Resources/carte.png");

            }
            if (piocheAnimal.getCards().Count > 0)
            {
                piocher();
                if (numHuman == -1)
                    jouerTour(-1);
            }
                
        }



        /// <summary>
        /// Mets à jour les scores des joueurs dans la vue
        /// </summary>
        private void updateViewPlayers()
        {
            for (int i =0 ; i<players.Count; i++)
            {
                labelScore[i].Text = players[i].Score.ToString();
            }
            
        }


        /// <summary>
        /// Permet de lancer une nouvelle partie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lancerPartie(object sender, EventArgs e)
        {
            piocheAnimal.initializeStack(false);
            for(int i=0; i<players.Count; i++)
            {
                players[i].getHand().initializeStack(true);
                players[i].Score = 0;
            }
            updateViewPlayers();
            for (int i = 0; i < players.Count; i++)
            {
                panelChoice[i].Image = Image.FromFile("Resources/carte.png");

            }
            piocher();
            board.reset();
            if (numHuman != -1)
                showHandCards(true);
            else
                jouerTour(-1);

            choiceHuman = true;
           
        }
        
        /// <summary>
        /// Permet d'afficher ou non la main du joueur Humain
        /// </summary>
        /// <param name="show"></param>
        private void showHandCards(bool show)
        {
            for (int i = 0; i < Stack.NB_CARD; i++)
            {
                if (show)
                    handCard[i].Show();
                else
                    handCard[i].Hide();
            }
        }


        /// <summary>
        /// Permet de piocher une carte animal aléatoirement et l'affiche sur le board
        /// </summary>
        private void piocher()
        {
            
            animalCard = piocheAnimal.getRandomCard();
            panelCardAnimal.Image = Image.FromFile("Resources/cartePioche" + animalCard.Force +".png");

        }

        private void labelScore4_Click(object sender, EventArgs e)
        {

        }


        public bool choiceHuman { get; set; }
    }
}

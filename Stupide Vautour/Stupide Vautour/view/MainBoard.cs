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
            players.Add(new Human());
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
            for (int i = 0; i < Stack.NB_CARD; i++)
            {
                handCard[i].BackgroundImage = global::Stupide_Vautour.Properties.Resources.carte;
                handCard[i].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                handCard[i].Image = Image.FromFile("Resources/carteJoueur"+ (numHuman+1) + (i+1) + ".png");
                handCard[i].Click += new System.EventHandler(this.choiceCard);
                handCard[i].Tag = i + 1;
            
        }
            if (numHuman == -1)
            {
                showHandCards(false);
            }

        }

        void choiceCard(object sender, EventArgs e)
        {
            if (!choiceHuman)
                return;

            choiceHuman = false;
            PictureBox pictureCard = (PictureBox)sender;


            choiceRoundCard = new List<Card>(players.Count);
            List<Player> playersBefore = Board.duplicatePlayers(players);

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] is Human)
                {
                    int choix = (int)pictureCard.Tag;
                    handCard[choix-1].Hide();
                    choiceRoundCard.Add(players[i].getHand().pickCard(players[i].getHand().findPositionCard(new Card(Card.PLAYER, choix))));
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
            piocher();
            choiceHuman = true;
        }


        private void buttonPlay_Click(object sender, EventArgs e)
        {
            
        }

        private void updateViewPlayers()
        {
            for (int i =0 ; i<players.Count; i++)
            {
                labelScore[i].Text = players[i].Score.ToString();
            }
            
        }

        private int getChoice(Player p)
        {

            HandChoiceForm form = new HandChoiceForm(p);
            form.ShowDialog();
            return form.choixCarte;
        }

        private void lancerPartie(object sender, EventArgs e)
        {
            piocheAnimal.initializeStack(false);
            for(int i=0; i<players.Count; i++)
            {
                players[i].getHand().initializeStack(true);
                players[i].Score = 0;
            }
            updateViewPlayers();
            piocher();
            buttonPlay.Enabled = true;
            board.reset();
            if( numHuman !=-1)
                showHandCards(true);

            choiceHuman = true;
           
        }
           
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

        private void piocher()
        {
            for (int i = 0; i < players.Count; i++)
            {
               panelChoice[i].Image = Image.FromFile("Resources/carte.png");
    
            }
            animalCard = piocheAnimal.getRandomCard();
            panelCardAnimal.Image = Image.FromFile("Resources/cartePioche" + animalCard.Force +".png");

        }

        private void labelScore4_Click(object sender, EventArgs e)
        {

        }


        public bool choiceHuman { get; set; }
    }
}

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

        public MainBoard()
        {
            InitializeComponent();
            //Ajout des joueurs
            players = new List<Player>();
            players.Add(new Human());
            players.Add(new MediumSmart());
            players.Add(new Human());
            players.Add(new Human());
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
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            choiceRoundCard = new List<Card>(players.Count);
            for (int i=0; i<players.Count; i++)
            {
                if (players[i] is Human )
                {
                    int choix = getChoice(players[i]);
                    choiceRoundCard.Add(players[i].getHand().pickCard(choix));
                }
                else
                {
                    
                     choiceRoundCard.Add(players[i].play(animalCard, board.getLastTurn()));
                    
                }
            }
            for (int i =0; i<players.Count;i++)
            {
                panelChoice[i].Image = Image.FromFile("Resources/carteJoueur" + ((int)(i+1)) + choiceRoundCard[i].Force.ToString() + ".png");
            }
            this.Refresh();
            board.play(choiceRoundCard, animalCard);
            System.Threading.Thread.Sleep(5000);
            updateViewPlayers();
            piocher();
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

    }
}

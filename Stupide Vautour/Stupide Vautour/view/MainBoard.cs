using Stupide_Vautour.game;
using Stupide_Vautour.players;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stupide_Vautour
{
    public partial class MainBoard : Form
    {
        Board board;
        int nbPlayers;
        List<Player> players;
        Stack piocheAnimal;

        public MainBoard()
        {
            InitializeComponent();
            nbPlayers = 4;
            List<Player> players = new List<Player>();
            players.Add(new Human());
            players.Add(new Human());
            players.Add(new Human());
            players.Add(new Human());
            board = new Board(players);
            piocheAnimal = new Stack(false);
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            
            for (int i=0; i<nbPlayers; i++)
            {
                if (players[i] is Human )
                {
                    getChoice(i);
                }
            }
        }

        private void getChoice(int numPlayer)
        {
            throw new NotImplementedException();
        }



    }
}

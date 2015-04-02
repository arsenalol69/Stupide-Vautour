﻿using Stupide_Vautour.game;
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

        public MainBoard()
        {
            InitializeComponent();
            nbPlayers = 2;
            List<Player> players = new List<Player>();
            players.Add(new Human());
            players.Add(new Human());
            board = new Board(players);
            board.play(new Card(Card.ANIMAL, 5));

        }

        private void labelJoueur1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}

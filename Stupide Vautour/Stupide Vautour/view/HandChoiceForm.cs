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

namespace Stupide_Vautour.view
{
    public partial class HandChoiceForm : Form
    {
        List<Button> listeBoutton;
        public int choixCarte = 0;
        public HandChoiceForm(Player p)
        {
            listeBoutton = new List<Button>();
            InitializeComponent();
            this.Text = Text + "du joueur " + p.getNumeroPlayer();
            //Ajout des boutons dans la liste :
            listeBoutton.Add(button1);
            listeBoutton.Add(button2);
            listeBoutton.Add(button3);
            listeBoutton.Add(button4);
            listeBoutton.Add(button5);
            listeBoutton.Add(button6);
            listeBoutton.Add(button7);
            listeBoutton.Add(button8);
            listeBoutton.Add(button9);
            listeBoutton.Add(button10);
            listeBoutton.Add(button11);
            listeBoutton.Add(button12);
            listeBoutton.Add(button13);
            listeBoutton.Add(button14);
            listeBoutton.Add(button15);
            List<Card> cards = p.getHand().getCards();
            int i=0;
            foreach (Card c in cards)
            {
                int num = c.Force;
                listeBoutton[num-1].Enabled = true;
                listeBoutton[num-1].Tag = i;
                listeBoutton[num - 1].Click += new System.EventHandler(this.choiceCard);
                i++;
            }

        }


        void choiceCard(object sender, EventArgs e)
        {
            Button b = (Button) sender;
            choixCarte =(int) b.Tag;
            this.Close();
        }

    }

}

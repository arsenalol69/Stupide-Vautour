using Stupide_Vautour.game;
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
        public HandChoiceForm(Stack hand)
        {
            InitializeComponent();
            List<Card> cards = hand.getCards();
            foreach (Card c in cards)
            {
                int num = c.Force;
                //il faut actvier le bouton n°num... un tableau ??
            }

        }
    }
}

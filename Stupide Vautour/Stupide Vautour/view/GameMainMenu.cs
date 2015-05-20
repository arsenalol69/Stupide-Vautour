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
using System.Runtime.InteropServices;

namespace Stupide_Vautour.view
{
    public partial class GameMainMenu : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        //Liste contenant les combobox
        List<ComboBox> comboBoxes;

        //position de l'humain
        int humanPos;


        public GameMainMenu()
        {
            InitializeComponent();

            CenterToScreen();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            BackColor = Color.White;
            TransparencyKey = Color.White;

            FormBorderStyle = FormBorderStyle.None;

            comboBoxes = new List<ComboBox>();
            comboBoxes.Add(comboBox1);
            comboBoxes.Add(comboBox2);
            comboBoxes.Add(comboBox3);
            comboBoxes.Add(comboBox4);

            foreach(ComboBox cb in comboBoxes){

               
                cb.Items.Add("Facile");
                cb.Items.Add("Moyen");
                cb.Items.Add("Difficile");

                
            }

            //Le premier et deuxième joueur jouent obligatoirement
            comboBoxes[2].Items.Add("Aucun");
            comboBoxes[3].Items.Add("Aucun");

            comboBoxes[2].Items.Add("Humain");
            humanPos = 2;
            comboBoxes[0].SelectedItem = "Facile";
            comboBoxes[1].SelectedItem = "Difficile";
            comboBoxes[2].SelectedItem = "Humain";
            comboBoxes[3].SelectedItem = "Aucun";


        }


        public void onClickStart(object sender, EventArgs e)
        {
            List<Player> players = new List<Player>();

            for (int i = 0; i < 4 && !comboBoxes[i].SelectedItem.Equals("Aucun"); i++)
            {
                Player p = null;
                switch (comboBoxes[i].SelectedItem.ToString())
                {
                    case "Facile":
                        p = new Stupid();
                        break;
                    case "Moyen":
                        p = new MediumSmart();
                        break;
                    case "Difficile":
                        p = new VerySmart();
                        break;
                    case "Humain":
                        p = new Human();
                        break;
                }
                players.Add(p);

            }
            Console.WriteLine("" + players.Count);
            MainBoard mainBoard = new MainBoard(players);
            mainBoard.ShowDialog();

        }

        public void onClickExit(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void Form1_MouseDown(object sender,
        System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }


        /// <summary>
        /// Vérifie que 2 humain ne sont pas sélectionner simultanément
        /// </summary>
        private void checkHuman(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedItem.Equals("Humain"))
            {
                humanPos = comboBoxes.IndexOf(cb);
                foreach (ComboBox c in comboBoxes)
                {
                    if (!c.Equals(cb))
                        c.Items.Remove("Humain");
                }
            }
            else if (comboBoxes.IndexOf(cb) == humanPos)
            {
                humanPos = -1;
                foreach (ComboBox c in comboBoxes)
                {
                    if (!c.Equals(cb))
                        c.Items.Add("Humain");
                }
            }



        }

        /// <summary>
        /// Vérifie que le joueur 4 ne puisse pas jouer si le joueur 3 ne joue pas.
        /// </summary>
        private void checknbJoueur(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem.Equals("Aucun"))
            {
                comboBox4.Enabled = false;
                comboBox4.SelectedItem = "Aucun";
            }
            else
                comboBox4.Enabled = true;

            checkHuman(sender, e);

        }
    }
}

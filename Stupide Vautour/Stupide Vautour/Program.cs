using Stupide_Vautour.game;
using Stupide_Vautour.players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stupide_Vautour
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new MainBoard());
            Player p1 = new Stupid();
            Card c = p1.play(new Card(Card.ANIMAL, 5));
            Console.WriteLine("hello");
            Console.WriteLine(c.toString());
            Console.ReadLine();
        }
    }
}

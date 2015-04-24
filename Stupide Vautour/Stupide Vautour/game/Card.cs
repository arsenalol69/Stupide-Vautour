using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stupide_Vautour.game
{
    public class Card
    {
        public static int ANIMAL = 1;
        public static int PLAYER = 2;

        int type;
        int force;


        public Card(int type, int force)
        {
            this.type = type;
            this.force = force;
        }

        public int Force
        {
            get { return force; }
        }

        public int Type
        {
            get { return type; }
        }



        public override string ToString()
        {
            return "Carte " + force + " de type " + (type==ANIMAL ? "Animal" : "Joueur");
        }
    }
}

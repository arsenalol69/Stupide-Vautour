using Stupide_Vautour.game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stupide_Vautour.players
{
    class VerySmart : Stupid
    {
        public VerySmart() : base()
        {

        }

        public override Card play(Turn t, Board board)
        {
            double p = 0;
            double valeurPioche = getValeurCartePioche(t.AnimalCarte, t.Pioche);
            double valeurJoueur = getPositionJoueur(t, this);
            p = valeurJoueur * valeurPioche;
            Console.WriteLine("Valeur pioche : " + valeurPioche);
            Console.WriteLine("Valeur Joueur : " + valeurJoueur);
            Console.WriteLine("Volonté : " + p);
            return bestCard(t, board, p);
                    
        }

    }
}

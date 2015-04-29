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
            Console.Write("Valeur volonté : " + p);
            return bestCard(t, board, p);
                    
        }

    }
}

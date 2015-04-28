using Stupide_Vautour.game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stupide_Vautour.players
{
    public class Human : Player
    {
         public Human() :  base() 
        {
            
        }

         public override Card play(Card animal, Turn t, Board board) { return null; }
         
    }
   
}

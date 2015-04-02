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

         public override Card play(Card animal)
         {
             //Afficher cartes
             int cardToPlay;
             Console.WriteLine("Which card do you want to play ?");
             String txt = " ";
             txt = Console.In.ReadLine();
             cardToPlay = Convert.ToInt32(txt);
             return myCards.getCards()[cardToPlay];
         } 
    }
   
}

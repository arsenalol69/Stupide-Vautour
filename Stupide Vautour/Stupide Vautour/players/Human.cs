using Stupide_Vautour.game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stupide_Vautour.players
{
    class Human : Player
    {
         public Human() :  base() 
        {
            
        }

         protected override Card play(Card animal)
         {
             //Afficher cartes
             int cardToPlay;
             Console.WriteLine("Which card do you want to play ?");
             cardToPlay = int.Parse(Console.In.ReadLine());
             return myCards.getCards()[cardToPlay];
         } 
    }
   
}

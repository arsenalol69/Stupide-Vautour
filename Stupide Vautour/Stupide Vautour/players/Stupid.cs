﻿using Stupide_Vautour.game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stupide_Vautour.players
{
    class Stupid : Player
    {


        public Stupid()
            : base()
        {

        }

        public override Card play(Card animal)
        {
            return myCards.getRandomCard();
        } 
    }
}

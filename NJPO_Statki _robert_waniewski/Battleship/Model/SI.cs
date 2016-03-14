using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitwaMorskoLadowa.Model
{
    class SI: Gracz
    {
        public void TakeTurn(Gracz otherPlayer)
        {
            TakeTurnAutomated(otherPlayer);
        }
    }
}

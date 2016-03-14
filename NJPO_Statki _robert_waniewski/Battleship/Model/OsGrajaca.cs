using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitwaMorskoLadowa.Model
{
    class OsGrajaca: Gracz
    {
        public void TakeTurn(int row, int col, Gracz otherPlayer)
        {
            Fire(row, col, otherPlayer);
        }
    }
}

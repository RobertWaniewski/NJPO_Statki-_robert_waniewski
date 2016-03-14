using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitwaMorskoLadowa.Model
{
    internal enum TypStatku
    {
        StateekBojowy,
        Niszczyciel1,
        Niszczyciel2,
        Desantowa1,
        Desantowa2,
        desantowa3,
        Zwiad1,
        zwiad2,
        zwiad3,
        zwiad4,

    };

    internal class Statki
    {
        private int _health;

        private readonly TypStatku _type;

        private static readonly Dictionary<TypStatku, int> shipLengths =
            new Dictionary<TypStatku, int>()
            {
                {TypStatku.StateekBojowy, 4},
                {TypStatku.Niszczyciel1, 3},
                {TypStatku.Niszczyciel2, 3},
                {TypStatku.Desantowa1, 2},
                {TypStatku.Desantowa2, 2},
                {TypStatku.desantowa3, 2},
                {TypStatku.Zwiad1, 1},
                {TypStatku.zwiad2, 1},
                {TypStatku.zwiad3, 1},
                {TypStatku.zwiad4, 1}
            };

        public Statki(TypStatku type)
        {
            _type = type;
            Reincarnate();
        }

        public void Reincarnate()
        {
            _health = shipLengths[_type];
        }

        public int Length
        {
            get { return shipLengths[_type]; }
        }

        public bool IsDestroyed
        {
            get { return _health == 0 ? true : false; }
        }

        public bool FiredAt()
        {
            _health--;
            return IsDestroyed;
        }
    }
}

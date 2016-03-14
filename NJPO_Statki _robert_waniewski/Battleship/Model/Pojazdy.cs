using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitwaMorskoLadowa.Model
{
    internal enum VehicleType
    {
        Czolg,
        Mozdziez2,
        Mozdizez2,
        Dzialo1,
        Dzialo2,
        Dzialo3

    };

    internal class Pojazdy
    {
        private int _health;

        private readonly VehicleType _type;

        private static readonly Dictionary<VehicleType, int> vehicleLengths =
            new Dictionary<VehicleType, int>()
            {
                {VehicleType.Czolg, 4},
                {VehicleType.Mozdziez2, 3},
                {VehicleType.Mozdizez2, 3},
                {VehicleType.Dzialo1, 2},
                {VehicleType.Dzialo2, 2},
                {VehicleType.Dzialo3, 2}
            };

        public Pojazdy(VehicleType type)
        {
            _type = type;
            Reincarnate();
        }

        public void Reincarnate()
        {
            _health = vehicleLengths[_type];
        }

        public int Length
        {
            get { return vehicleLengths[_type]; }
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

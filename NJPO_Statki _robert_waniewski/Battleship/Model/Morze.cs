using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BitwaMorskoLadowa.Model
{
    enum SquareType { Poczatkowy, Woda, NieTrafiony, Trafiony, Zniszczony, Trawa, TrawaTraf, WodTraf }

    enum SquareObject { Poczatkowy, Statek, Pojazd, Plansza, Zniszczony }

    class Morze : DependencyObject
    {
        public int Row { get; private set;  }
        public int Col { get; private set; }
        public int SquareIndex { get; set; }


        public SquareType Type
        {
            get { return (SquareType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register("Type", typeof(SquareType), typeof(Morze), null);



        public SquareObject ObjectType
        {
            get { return (SquareObject)GetValue(ObjectProperty); }
            set { SetValue(ObjectProperty, value); }
        }
        public static readonly DependencyProperty ObjectProperty =
       DependencyProperty.Register("ObjectType", typeof(SquareObject), typeof(Morze), null);


        public Morze(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public void Reset(SquareType type, SquareObject objectType)
        {
            ObjectType = objectType;
            Type = type;
            SquareIndex = -1;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BitwaMorskoLadowa.Model;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;

namespace BitwaMorskoLadowa.ViewModel
{
    class GridKomputeraVm : BazowyGridVM
    {
        public GridKomputeraVm(OsGrajaca osgrajaca, SI si)
            : base(osgrajaca, si)
        {
        }

        public override List<List<Morze>> MyGrid
        {
            get
            {
                return _osgrajaca.EnemyGrid;
            }
        }

        //returns true if game is over
        public override bool Clicked(Morze square, bool automated)
        {
            if (automated)
                _osgrajaca.TakeTurnAutomated(_SI);
            else
            {
                if (square.Type != SquareType.Poczatkowy)
                {
                    MessageBox.Show("Please choose a new square");
                    return false;
                }

                _osgrajaca.TakeTurn(square.Row, square.Col, _SI);
            }

            if (_SI.NoShipsSadFace() && _SI.NoVehiclesSadFace() && _SI.NoPlanesSadFace())
            {
                MessageBox.Show("You win!");
                return true;
            }
            else
            {
                _SI.TakeTurn(_osgrajaca);
                if (_osgrajaca.NoShipsSadFace() && _osgrajaca.NoVehiclesSadFace() && _osgrajaca.NoPlanesSadFace())
                {
                    MessageBox.Show("You lose :(");
                    return true;
                }
            }

            return false;
        }
    }
}

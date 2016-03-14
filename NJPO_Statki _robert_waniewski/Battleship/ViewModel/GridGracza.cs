using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BitwaMorskoLadowa.Model;
using System.Windows.Data;
using System.ComponentModel;

namespace BitwaMorskoLadowa.ViewModel
{
    class GridGraczaVm: BazowyGridVM
    {
        public GridGraczaVm(OsGrajaca osgrajaca, SI si)
            : base(osgrajaca, si)
        {
        }

        //for design mode only
        public GridGraczaVm()
            : base(null, null)
        {
            _osgrajaca = new OsGrajaca();
        }

        public override List<List<Morze>> MyGrid
        {
            get
            {
                return _osgrajaca.MyGrid;
            }
        }

        public override bool Clicked(Morze content, bool automated)
        {
            return false;
        }
    }
}

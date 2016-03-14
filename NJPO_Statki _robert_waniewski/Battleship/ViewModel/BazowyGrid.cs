using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BitwaMorskoLadowa.Model;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Input;

namespace BitwaMorskoLadowa.ViewModel
{
    abstract class BazowyGridVM
    {
        protected OsGrajaca _osgrajaca;
        protected SI _SI;

        public BazowyGridVM(OsGrajaca osgrajaca, SI si)
        {
            _osgrajaca = osgrajaca;
            _SI = si;
        }

        public abstract bool Clicked(Morze content, bool automated=false);
        public abstract List<List<Morze>> MyGrid { get; }
    }
}

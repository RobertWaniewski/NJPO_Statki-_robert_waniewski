using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BitwaMorskoLadowa.ViewModel;
using BitwaMorskoLadowa.Model;

namespace BitwaMorskoLadowa.View
{
    /// <summary>
    /// Interaction logic for ComputerGrid.xaml
    /// </summary>
    public partial class SeaGrid : UserControl
    {
        public SeaGrid()
        {
            InitializeComponent();
        }

        private void Item_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BazowyGridVM vm = this.DataContext as BazowyGridVM;
            ListBoxItem item = sender as ListBoxItem;
            Morze content = item.Content as Morze;

            //XXX sometimes if you click really fast you can end up clicking on what the debugger says is a "ListBoxItem {DisconnectedItem}
            //hunting down the exact cause would take ages, and might even be a bug in WPF or something
            if (content == null)
                return;

            vm.Clicked(content);            
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

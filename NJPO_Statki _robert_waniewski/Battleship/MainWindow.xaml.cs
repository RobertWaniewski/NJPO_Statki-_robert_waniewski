using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using BitwaMorskoLadowa.ViewModel;
using BitwaMorskoLadowa.Model;

namespace BitwaMorskoLadowa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OsGrajaca _humanPlayer;
        SI _computerPlayer;

        GridGraczaVm _humanGrid;
        GridKomputeraVm _computerGrid;

        public static MainWindow _mainWindow;


        public MainWindow()
        {
            _humanPlayer = new OsGrajaca();
            _computerPlayer = new SI();
            _humanGrid = new GridGraczaVm(_humanPlayer, _computerPlayer);
            _computerGrid = new GridKomputeraVm(_humanPlayer, _computerPlayer);

            InitializeComponent();
            _mainWindow = this;
            humanGrid.DataContext = _humanGrid;
            computerGrid.DataContext = _computerGrid;
            UpdateTb(0,19);
        }

        public void UpdateTb(int shots, int enemyUnits)
        {
            infoTB.Text = "Shots: " + shots + Environment.NewLine + "Enemy Units: " + enemyUnits;
        }

        private void ExecutedNewGame(object sender, ExecutedRoutedEventArgs e)
        {
            _humanPlayer.Reset();
            _computerPlayer.Reset();            
        }

        private void ExecutedAutomatedGame(object sender, ExecutedRoutedEventArgs e)
        {
            ExecutedNewGame(sender, e);
            while (!_computerGrid.Clicked(null, true))
            { }
        }

        private void ExecutedExit(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
    }
}

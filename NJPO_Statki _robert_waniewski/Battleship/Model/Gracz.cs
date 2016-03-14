using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace BitwaMorskoLadowa.Model
{
    internal class Gracz
    {
        protected const int GRID_SIZE_X = 14;
        protected const int GRID_SIZE_X_PLANES = 12;
        protected const int GRID_SIZE_Y_SHIPS = 11;
        protected const int GRID_SIZE_Y_VEHICLES = 11;
        protected const int GRID_SIZE_Y = 22;


        protected static Random rnd = new Random();

        public List<List<Morze>> MyGrid { get; set; }
        public List<List<Morze>> EnemyGrid { get; set; }

        private List<Statki> _mojestatki = new List<Statki>();
        private List<Statki> _enemyShips = new List<Statki>();

        private List<Pojazdy> _mojepojazdy = new List<Pojazdy>();
        private List<Pojazdy> _enemyVehicles = new List<Pojazdy>();

        private List<Plansza> mojesamo = new List<Plansza>();
        private List<Plansza> _enemyPlanes = new List<Plansza>();
        private int _shots;
        private int _enemyUnits;

        public Gracz()
        {
            _shots = 0;
            _enemyUnits = 19;

            MyGrid = new List<List<Morze>>();
            EnemyGrid = new List<List<Morze>>();

            for (int i = 0; i != GRID_SIZE_Y; ++i)
            {
                MyGrid.Add(new List<Morze>());
                EnemyGrid.Add(new List<Morze>());

                for (int j = 0; j != GRID_SIZE_X; ++j)
                {
                    MyGrid[i].Add(new Morze(i, j));
                    EnemyGrid[i].Add(new Morze(i, j));
                }
            }
            
            foreach (TypStatku type in Enum.GetValues(typeof (TypStatku)))
            {
                _mojestatki.Add(new Statki(type));
                _enemyShips.Add(new Statki(type));
            }

            foreach (VehicleType type in Enum.GetValues(typeof (VehicleType)))
            {
                _mojepojazdy.Add(new Pojazdy(type));
                _enemyVehicles.Add(new Pojazdy(type));
            }

    

            foreach (PlaneType type in Enum.GetValues(typeof (PlaneType)))
            {
                mojesamo.Add(new Plansza(type));
                _enemyPlanes.Add(new Plansza(type));
            }


            Reset();
        }

        public void Reset()
        {
            _shots = 0;
            _enemyUnits = 19;

            for (int i = 0; i != GRID_SIZE_Y; ++i)
            {
                if (i < 11)
                {
                    for (int j = 0; j != GRID_SIZE_X; ++j)
                    {
                        MyGrid[i][j].Reset(SquareType.Woda, SquareObject.Poczatkowy);
                        EnemyGrid[i][j].Reset(SquareType.Poczatkowy, SquareObject.Poczatkowy);
                    }
                }
                else
                {
                    for (int j = 0; j != GRID_SIZE_X; ++j)
                    {
                        MyGrid[i][j].Reset(SquareType.Trawa, SquareObject.Poczatkowy);
                        EnemyGrid[i][j].Reset(SquareType.Poczatkowy, SquareObject.Poczatkowy);
                    }
                }
            }

            _mojestatki.ForEach(s => s.Reincarnate());
            _enemyShips.ForEach(s => s.Reincarnate());
            _mojepojazdy.ForEach(s => s.Reincarnate());
            _enemyVehicles.ForEach(s => s.Reincarnate());
            mojesamo.ForEach(s => s.Reincarnate());
            _enemyPlanes.ForEach(s => s.Reincarnate());
            PlaceShips();
            PlaceVehicles();
            PlacePlanes();
        }

        private bool SquareFree(int row, int col)
        {
            return (MyGrid[row][col].SquareIndex == -1) ? true : false;
        }

        private bool CheckAdjacentSquaresForShips(int row, int col)
        {
            if (row - 1 > -1 && col - 1 > -1)
            {
                if (MyGrid[row - 1][col - 1].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (row - 1 > -1)
            {
                if (MyGrid[row - 1][col].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (row - 1 > -1 && col + 1 < 14)
            {
                if (MyGrid[row - 1][col + 1].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (col - 1 > -1)
            {
                if (MyGrid[row][col - 1].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (col + 1 < 14)
            {
                if (MyGrid[row][col + 1].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (row + 1 < 12 && col - 1 > -1)
            {
                if (MyGrid[row + 1][col - 1].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (row + 1 < 12)
            {
                if (MyGrid[row + 1][col].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (row + 1 < 12 && col + 1 < 14)
            {
                if (MyGrid[row + 1][col + 1].SquareIndex != -1)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckAdjacentSquaresForVehicles(int row, int col)
        {
            if (row - 1 > 1 && col - 1 > -1)
            {
                if (MyGrid[row - 1][col - 1].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (row - 1 > 11)
            {
                if (MyGrid[row - 1][col].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (row - 1 > 11 && col + 1 < 14)
            {
                if (MyGrid[row - 1][col + 1].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (col - 1 > -1)
            {
                if (MyGrid[row][col - 1].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (col + 1 < 14)
            {
                if (MyGrid[row][col + 1].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (row + 1 < 22 && col - 1 > -1)
            {
                if (MyGrid[row + 1][col - 1].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (row + 1 < 22)
            {
                if (MyGrid[row + 1][col].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (row + 1 < 22 && col + 1 < 14)
            {
                if (MyGrid[row + 1][col + 1].SquareIndex != -1)
                {
                    return false;
                }
            }
            return true;
        }


        private bool CheckAdjacentSquaresForPlanes(int row, int col)
        {
            if (row - 1 > -1 && col - 1 > -1)
            {
                if (MyGrid[row - 1][col - 1].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (row - 1 > -1)
            {
                if (MyGrid[row - 1][col].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (row - 1 > -1 && col + 1 < 14)
            {
                if (MyGrid[row - 1][col + 1].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (col - 1 > -1)
            {
                if (MyGrid[row][col - 1].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (col + 1 < 14)
            {
                if (MyGrid[row][col + 1].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (row + 1 < 22 && col - 1 > -1)
            {
                if (MyGrid[row + 1][col - 1].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (row + 1 < 22)
            {
                if (MyGrid[row + 1][col].SquareIndex != -1)
                {
                    return false;
                }
            }
            if (row + 1 < 22 && col + 1 < 14)
            {
                if (MyGrid[row + 1][col + 1].SquareIndex != -1)
                {
                    return false;
                }
            }
            return true;
        }

        private bool PlaceVerticalShip(int shipIndex, int remainingLength)
        {
            int startPosRow = rnd.Next(GRID_SIZE_Y_SHIPS - remainingLength);
            int startPosCol = rnd.Next(GRID_SIZE_X);

            Func<bool> PlacementPossible = () =>
            {
                int tmp = remainingLength;
                for (int row = startPosRow; tmp != 0; ++row)
                {
                    if (!SquareFree(row, startPosCol))
                        return false;
                    if (!CheckAdjacentSquaresForShips(row, startPosCol))
                        return false;
                    --tmp;
                }
                return true;
            };

            if (PlacementPossible())
            {
                for (int row = startPosRow; remainingLength != 0; ++row)
                {
                    MyGrid[row][startPosCol].Type = SquareType.NieTrafiony;
                    MyGrid[row][startPosCol].ObjectType = SquareObject.Statek;
                    MyGrid[row][startPosCol].SquareIndex = shipIndex;
                    --remainingLength;
                }
                return true;
            }

            return false;
        }

        private bool PlaceHorizontalShip(int shipIndex, int remainingLength)
        {
            int startPosRow = rnd.Next(GRID_SIZE_Y_SHIPS);
            int startPosCol = rnd.Next(GRID_SIZE_X - remainingLength);

            Func<bool> PlacementPossible = () =>
            {
                int tmp = remainingLength;
                for (int col = startPosCol; tmp != 0; ++col)
                {
                    if (!SquareFree(startPosRow, col))
                        return false;
                    if (!CheckAdjacentSquaresForShips(startPosRow, col))
                        return false;
                    --tmp;
                }
                return true;
            };

            if (PlacementPossible())
            {
                for (int col = startPosCol; remainingLength != 0; ++col)
                {
                    MyGrid[startPosRow][col].Type = SquareType.NieTrafiony;
                    MyGrid[startPosRow][col].ObjectType = SquareObject.Statek;
                    MyGrid[startPosRow][col].SquareIndex = shipIndex;
                    --remainingLength;
                }
                return true;
            }

            return false;
        }

        private bool PlaceVerticalVehicle(int vehicleIndex, int remainingLength)
        {
            int startPosRow = rnd.Next(GRID_SIZE_Y_VEHICLES - remainingLength) + 11;
            int startPosCol = rnd.Next(GRID_SIZE_X);

            Func<bool> PlacementPossible = () =>
            {
                int tmp = remainingLength;
                for (int row = startPosRow; tmp != 0; ++row)
                {
                    if (!SquareFree(row, startPosCol))
                        return false;
                    if (!CheckAdjacentSquaresForVehicles(row, startPosCol))
                        return false;
                    --tmp;
                }
                return true;
            };

            if (PlacementPossible())
            {
                for (int row = startPosRow; remainingLength != 0; ++row)
                {
                    MyGrid[row][startPosCol].Type = SquareType.NieTrafiony;
                    MyGrid[row][startPosCol].ObjectType = SquareObject.Pojazd;
                    MyGrid[row][startPosCol].SquareIndex = vehicleIndex;
                    --remainingLength;
                }
                return true;
            }

            return false;
        }


        private bool PlaceHorizontalVehicle(int vehicleIndex, int remainingLength)
        {
            int startPosRow = rnd.Next(GRID_SIZE_Y_VEHICLES) + 11;
            int startPosCol = rnd.Next(GRID_SIZE_X - remainingLength);

            Func<bool> PlacementPossible = () =>
            {
                int tmp = remainingLength;
                for (int col = startPosCol; tmp != 0; ++col)
                {
                    if (!SquareFree(startPosRow, col))
                        return false;
                    if (!CheckAdjacentSquaresForVehicles(startPosRow, col))
                        return false;
                    --tmp;
                }
                return true;
            };

            if (PlacementPossible())
            {
                for (int col = startPosCol; remainingLength != 0; ++col)
                {
                    MyGrid[startPosRow][col].Type = SquareType.NieTrafiony;
                    MyGrid[startPosRow][col].ObjectType = SquareObject.Pojazd;
                    MyGrid[startPosRow][col].SquareIndex = vehicleIndex;
                    --remainingLength;
                }
                return true;
            }

            return false;
        }


        private bool PlacePlaneUp(int planeIndex, int remainingLength)
        {
            int startPosRow = rnd.Next(GRID_SIZE_Y - remainingLength);
            int startPosCol = rnd.Next(GRID_SIZE_X_PLANES);

            Func<bool> PlacementPossible = () =>
            {
                int tmp = remainingLength;
                for (int col = startPosCol; tmp != 0; ++col)
                {
                    if (col > startPosCol + 2)
                    {
                        if (!SquareFree(startPosRow + 1, startPosCol + 1))
                            return false;
                        if (!CheckAdjacentSquaresForPlanes(startPosRow + 1, startPosCol + 1))
                            return false;
                    }
                    else
                    {
                        if (!SquareFree(startPosRow, col))
                            return false;
                        if (!CheckAdjacentSquaresForPlanes(startPosRow, col))
                            return false;
                    }
                    --tmp;
                }
                return true;
            };

            if (PlacementPossible())
            {
                for (int col = startPosCol; remainingLength != 0; ++col)
                {
                    if (col > startPosCol + 2)
                    {
                        MyGrid[startPosRow + 1][startPosCol + 1].Type = SquareType.NieTrafiony;
                        MyGrid[startPosRow + 1][startPosCol + 1].ObjectType = SquareObject.Plansza;
                        MyGrid[startPosRow + 1][startPosCol + 1].SquareIndex = planeIndex;
                    }
                    else
                    {
                        MyGrid[startPosRow][col].Type = SquareType.NieTrafiony;
                        MyGrid[startPosRow][col].ObjectType = SquareObject.Plansza;
                        MyGrid[startPosRow][col].SquareIndex = planeIndex;
                    }

                    --remainingLength;
                }
                return true;
            }

            return false;
        }


        private bool PlacePlaneDown(int planeIndex, int remainingLength)
        {
            int startPosRow = rnd.Next(GRID_SIZE_Y - remainingLength - 1) + 1;
            int startPosCol = rnd.Next(GRID_SIZE_X_PLANES);

            Func<bool> PlacementPossible = () =>
            {
                int tmp = remainingLength;
                for (int col = startPosCol; tmp != 0; ++col)
                {
                    if (col > startPosCol + 2)
                    {
                        if (!SquareFree(startPosRow - 1, startPosCol + 1))
                            return false;
                        if (!CheckAdjacentSquaresForPlanes(startPosRow - 1, startPosCol + 1))
                            return false;
                    }
                    else
                    {
                        if (!SquareFree(startPosRow, col))
                            return false;
                        if (!CheckAdjacentSquaresForPlanes(startPosRow, col))
                            return false;
                    }
                    --tmp;
                }
                return true;
            };

            if (PlacementPossible())
            {
                for (int col = startPosCol; remainingLength != 0; ++col)
                {
                    if (col > startPosCol + 2)
                    {
                        MyGrid[startPosRow - 1][startPosCol + 1].Type = SquareType.NieTrafiony;
                        MyGrid[startPosRow - 1][startPosCol + 1].ObjectType = SquareObject.Plansza;
                        MyGrid[startPosRow - 1][startPosCol + 1].SquareIndex = planeIndex;
                    }
                    else
                    {
                        MyGrid[startPosRow][col].Type = SquareType.NieTrafiony;
                        MyGrid[startPosRow][col].ObjectType = SquareObject.Plansza;
                        MyGrid[startPosRow][col].SquareIndex = planeIndex;
                    }

                    --remainingLength;
                }
                return true;
            }

            return false;
        }

        private bool PlacePlaneRight(int planeIndex, int remainingLength)
        {
            int startPosRow = rnd.Next(GRID_SIZE_Y - remainingLength);
            int startPosCol = rnd.Next(GRID_SIZE_X_PLANES) + 1;

            Func<bool> PlacementPossible = () =>
            {
                int tmp = remainingLength;
                for (int row = startPosRow; tmp != 0; ++row)
                {
                    if (row > startPosRow + 2)
                    {
                        if (!SquareFree(startPosRow + 1, startPosCol - 1))
                            return false;
                        if (!CheckAdjacentSquaresForPlanes(row, startPosCol))
                            return false;
                        --tmp;
                    }
                    else
                    {
                        if (!SquareFree(row, startPosCol))
                            return false;
                        if (!CheckAdjacentSquaresForPlanes(row, startPosCol))
                            return false;
                        --tmp;
                    }
                }
                return true;
            };

            if (PlacementPossible())
            {
                for (int row = startPosRow; remainingLength != 0; ++row)
                {
                    if (row > startPosRow + 2)
                    {
                        MyGrid[startPosRow+1][startPosCol-1].Type = SquareType.NieTrafiony;
                        MyGrid[startPosRow+1][startPosCol-1].ObjectType = SquareObject.Plansza;
                        MyGrid[startPosRow+1][startPosCol-1].SquareIndex = planeIndex;
                    }
                   else
                    {
                        MyGrid[row][startPosCol].Type = SquareType.NieTrafiony;
                        MyGrid[row][startPosCol].ObjectType = SquareObject.Plansza;
                        MyGrid[row][startPosCol].SquareIndex = planeIndex;
                    }

                    --remainingLength;
                }
                return true;
            }

            return false;
        }

        private bool PlacePlaneLeft(int planeIndex, int remainingLength)
        {
            int startPosRow = rnd.Next(GRID_SIZE_Y - remainingLength);
            int startPosCol = rnd.Next(GRID_SIZE_X_PLANES) + 1;

            Func<bool> PlacementPossible = () =>
            {
                int tmp = remainingLength;
                for (int row = startPosRow; tmp != 0; ++row)
                {
                    if (row > startPosRow + 2)
                    {
                        if (!SquareFree(startPosRow + 1, startPosCol + 1))
                            return false;
                        if (!CheckAdjacentSquaresForPlanes(row, startPosCol))
                            return false;
                        --tmp;
                    }
                    else
                    {
                        if (!SquareFree(row, startPosCol))
                            return false;
                        if (!CheckAdjacentSquaresForPlanes(row, startPosCol))
                            return false;
                        --tmp;
                    }
                }
                return true;
            };

            if (PlacementPossible())
            {
                for (int row = startPosRow; remainingLength != 0; ++row)
                {
                    if (row > startPosRow + 2)
                    {
                        MyGrid[startPosRow + 1][startPosCol + 1].Type = SquareType.NieTrafiony;
                        MyGrid[startPosRow + 1][startPosCol + 1].ObjectType = SquareObject.Plansza;
                        MyGrid[startPosRow + 1][startPosCol + 1].SquareIndex = planeIndex;
                    }
                    else
                    {
                        MyGrid[row][startPosCol].Type = SquareType.NieTrafiony;
                        MyGrid[row][startPosCol].ObjectType = SquareObject.Plansza;
                        MyGrid[row][startPosCol].SquareIndex = planeIndex;
                    }

                    --remainingLength;
                }
                return true;
            }

            return false;
        }

        private void PlaceShips()
        {
            bool startAgain = false;

            for (int i = 0; i != _mojestatki.Count && !startAgain; ++i)
            {
                bool vertical = Convert.ToBoolean(rnd.Next(2));
                bool placed = false;

                int loopCounter = 0;
                for (; !placed && loopCounter != 10000; ++loopCounter)
                {
                    int remainingLength = _mojestatki[i].Length;

                    if (vertical)
                        placed = PlaceVerticalShip(i + 10, remainingLength);
                    else
                        placed = PlaceHorizontalShip(i + 10, remainingLength);
                }

                if (loopCounter == 10000)
                    startAgain = true;
            }

            if (startAgain)
                PlaceShips();
        }

        private void PlaceVehicles()
        {
            bool startAgain = false;

            for (int i = 0; i != _mojepojazdy.Count && !startAgain; ++i)
            {
                bool vertical = Convert.ToBoolean(rnd.Next(2));
                bool placed = false;

                int loopCounter = 0;
                for (; !placed && loopCounter != 10000; ++loopCounter)
                {
                    int remainingLength = _mojepojazdy[i].Length;

                    if (vertical)
                        placed = PlaceVerticalVehicle(i + 30, remainingLength);
                    else
                        placed = PlaceHorizontalVehicle(i + 30, remainingLength);
                }

                if (loopCounter == 10000)
                    startAgain = true;
            }

            if (startAgain)
                PlaceVehicles();
        }

        private void PlacePlanes()
        {
            bool startAgain = false;

            for (int i = 0; i != mojesamo.Count && !startAgain; ++i)
            {
                bool placed = false;
                int direction =  rnd.Next(4);

                int loopCounter = 0;
                for (; !placed && loopCounter != 10000; ++loopCounter)
                {
                    int remainingLength = mojesamo[i].Length;


                    if(direction==0)
                        placed = PlacePlaneUp(i + 50, remainingLength);
                    if (direction == 1)
                        placed = PlacePlaneDown(i + 50, remainingLength);
                    if (direction == 2)
                        placed = PlacePlaneLeft(i + 50, remainingLength);
                    if (direction == 3)
                        placed = PlacePlaneRight(i + 50, remainingLength);
                }

                if (loopCounter == 10000)
                    startAgain = true;
            }

            if (startAgain)
                PlacePlanes();
        }


        private void DestroyItem(int i, List<List<Morze>> grid)
        {
            foreach (var row in grid)
            {
                foreach (var square in row)
                {
                    if (square.SquareIndex == i)
                    {
                        square.Type = SquareType.Zniszczony;
                        square.ObjectType = SquareObject.Zniszczony;
                    }
                }
            }
        }

        private void MineDestroyed(int i)
        {
            DestroyItem(i, MyGrid);
            _enemyUnits--;
        }

        public void EnemyDestroyed(int i)
        {
            DestroyItem(i, EnemyGrid);
        }


        protected void Fire(int row, int col, Gracz otherPlayer)
        {
            //incrase number of shots and set number of enemy units
            _shots++;
            MainWindow._mainWindow.UpdateTb(_shots, _enemyUnits);

            int damagedIndex;
            bool isDestroyed;
            SquareType newType = otherPlayer.FiredAt(row, col, out damagedIndex, out isDestroyed);
            EnemyGrid[row][col].SquareIndex = damagedIndex;

            if (isDestroyed)
                EnemyDestroyed(damagedIndex);
            else
                EnemyGrid[row][col].Type = newType;
        }

        public SquareType FiredAt(int row, int col, out int damagedIndex, out bool isDestroyed)
        {
            isDestroyed = false;
            damagedIndex = -1;

            switch (MyGrid[row][col].Type)
            {
                case SquareType.Woda:
                    return SquareType.WodTraf;
                case SquareType.Trawa:
                    return SquareType.TrawaTraf;
                case SquareType.NieTrafiony:
                    var square = MyGrid[row][col];
                    damagedIndex = square.SquareIndex;

                    if (square.ObjectType == SquareObject.Statek)
                    {
                        if (_mojestatki[damagedIndex - 10].FiredAt())
                        {
                            MineDestroyed(square.SquareIndex + 10);
                            isDestroyed = true;
                        }
                        else
                        {
                            square.Type = SquareType.Trafiony;
                        }
                    }

                    if (square.ObjectType == SquareObject.Pojazd)
                    {
                        if (_mojepojazdy[damagedIndex - 30].FiredAt())
                        {
                            MineDestroyed(square.SquareIndex + 30);
                            isDestroyed = true;
                        }
                        else
                        {
                            square.Type = SquareType.Trafiony;
                        }
                    }

                    if (square.ObjectType == SquareObject.Plansza)
                    {
                        if (mojesamo[damagedIndex - 50].FiredAt())
                        {
                            MineDestroyed(square.SquareIndex + 50);
                            isDestroyed = true;
                        }
                        else
                        {
                            square.Type = SquareType.Trafiony;
                        }
                    }


                    return square.Type;
                case SquareType.Trafiony:
                    goto default;
                case SquareType.Poczatkowy:
                    goto default;
                case SquareType.Zniszczony:
                    goto default;
                case SquareType.WodTraf:
                    goto default;
                case SquareType.TrawaTraf:
                    goto default;
                default:
                    throw new Exception("fail");
            }
        }

        public bool NoShipsSadFace()
        {
            return _mojestatki.All(ship => ship.IsDestroyed);
        }

        public bool NoVehiclesSadFace()
        {
            return _mojepojazdy.All(vehicle => vehicle.IsDestroyed);
        }

        public bool NoPlanesSadFace()
        {
            return mojesamo.All(plane => plane.IsDestroyed);
        }

        public void TakeTurnAutomated(Gracz otherPlayer)
        {
            bool takenShot = false;
            while (!takenShot)
            {
                int row = rnd.Next(GRID_SIZE_Y);
                int col = rnd.Next(GRID_SIZE_X);

                if (EnemyGrid[row][col].Type == SquareType.Poczatkowy &&
                    EnemyGrid[row][col].ObjectType != SquareObject.Zniszczony)
                {
                    Fire(row, col, otherPlayer);
                    takenShot = true;
                }
            }
        }
    }
}

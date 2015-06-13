using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Game2048.Model;

namespace Game2048
{
    public class Game : INotifyPropertyChanged
    {
        private readonly int _boardSize;
        private bool _positionChanged;
        private int _score;

        public event PropertyChangedEventHandler PropertyChanged;
        public Tile[,] Board { get; set; }
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                OnPropertyChanged("Score");
            }
        }

        public Game(int boardSize)
        {
            _boardSize = boardSize;
            Board = new Tile[_boardSize, _boardSize];

            StartNewGame();
        }

        public void StartNewGame()
        {
            Score = 0;

            for (int i = 0; i < _boardSize; i++)
            {
                for (int j = 0; j < _boardSize; j++)
                {
                    if (Board[i, j] == null)
                    {
                        Board[i, j] = new Tile();
                    }
                    Board[i, j].Value = 0;
                }
            }
            PlaceNewElementOnTheBoard();
            PlaceNewElementOnTheBoard();
        }

        public void Move(Keys key)
        {
            if (IsGameOver())
            {
                var lostForm = new GameOverForm(Score);
                lostForm.ShowDialog();
                StartNewGame();
                return;
            }

            _positionChanged = false;
            switch (key)
            {
                case Keys.Right:
                    MoveRight();
                    break;
                case Keys.Left:
                    MoveLeft();
                    break;
                case Keys.Down:
                    MoveDown();
                    break;
                case Keys.Up:
                    MoveUp();
                    break;
            }

            if (_positionChanged)
            {
                PlaceNewElementOnTheBoard();
            }
        }

        private void PlaceNewElementOnTheBoard()
        {
            var emptyTiles = GetAllEmptyTiles();

            if (emptyTiles.Count > 0)
            {
                var randomEmptyTile = emptyTiles.OrderBy(n => Guid.NewGuid()).First();
                randomEmptyTile.Value = 2;
            }
        }

        private List<Tile> GetAllEmptyTiles()
        {
            var emptyTiles = new List<Tile>();
            for (int i = 0; i < _boardSize; i++)
            {
                for (int j = 0; j < _boardSize; j++)
                {
                    if (Board[i, j].Value == 0)
                    {
                        emptyTiles.Add(Board[i, j]);
                    }
                }
            }
            return emptyTiles;
        }

        private void MoveUp()
        {
            for (int col = 0; col < _boardSize; col++)
            {
                for (int row = 0; row < _boardSize; row++)
                {
                    var currentTile = Board[row, col];
                    if (currentTile.Value == 0)
                    {
                        continue;
                    }

                    int tmpRow = row;
                    while (tmpRow > 0 && GetTileOnTheAbove(tmpRow, col).Value == 0)
                    {
                        var nextTile = GetTileOnTheAbove(tmpRow--, col);
                        MoveTile(currentTile, nextTile);
                        currentTile = nextTile;
                    }

                    if (tmpRow > 0)
                    {
                        var tileAbove = GetTileOnTheAbove(tmpRow, col);
                        if (tileAbove.Value == currentTile.Value)
                        {
                            JoinTiles(currentTile, tileAbove);
                        }
                    }
                }
            }
        }
        private void MoveDown()
        {
            for (int col = 0; col < _boardSize; col++)
            {
                for (int row = _boardSize - 2; row >= 0; row--)
                {
                    var currentTile = Board[row, col];
                    if (currentTile.Value == 0)
                    {
                        continue;
                    }

                    int tmpRow = row;
                    while (tmpRow < _boardSize - 1 && GetTileOnTheBelow(tmpRow, col).Value == 0)
                    {
                        var nextTile = GetTileOnTheBelow(tmpRow++, col);
                        MoveTile(currentTile, nextTile);
                        currentTile = nextTile;
                    }

                    if (tmpRow < _boardSize - 1)
                    {
                        var tileBelow = GetTileOnTheBelow(tmpRow, col);
                        if (tileBelow.Value == currentTile.Value)
                        {
                            JoinTiles(currentTile, tileBelow);
                        }
                    }
                }
            }
        }
        private void MoveLeft()
        {
            for (int row = 0; row < _boardSize; row++)
            {
                for (int col = 0; col < _boardSize; col++)
                {
                    var currentTile = Board[row, col];
                    if (currentTile.Value == 0)
                    {
                        continue;
                    }

                    int tmpCol = col;
                    while (tmpCol > 0 && GetTileOnTheLeft(row, tmpCol).Value == 0)
                    {
                        var nextTile = GetTileOnTheLeft(row, tmpCol--);
                        MoveTile(currentTile, nextTile);
                        currentTile = nextTile;
                    }

                    if (tmpCol > 0)
                    {
                        var tileOnTheLeft = GetTileOnTheLeft(row, tmpCol);
                        if (tileOnTheLeft.Value == currentTile.Value)
                        {
                            JoinTiles(currentTile, tileOnTheLeft);
                        }
                    }
                }
            }
        }
        private void MoveRight()
        {
            for (int row = 0; row < _boardSize; row++)
            {
                for (int col = _boardSize - 2; col >= 0; col--)
                {
                    var currentTile = Board[row, col];
                    if (currentTile.Value == 0)
                    {
                        continue;
                    }

                    int tmpCol = col;
                    while (tmpCol < _boardSize - 1 && GetTileOnTheRight(row, tmpCol).Value == 0)
                    {
                        var nextTile = GetTileOnTheRight(row, tmpCol++);
                        MoveTile(currentTile, nextTile);
                        currentTile = nextTile;
                    }

                    if (tmpCol < _boardSize - 1)
                    {
                        var tileOnTheRight = GetTileOnTheRight(row, tmpCol);
                        if (tileOnTheRight.Value == currentTile.Value)
                        {
                            JoinTiles(currentTile, tileOnTheRight);
                        }
                    }
                }
            }
        }

        private Tile GetTileOnTheAbove(int row, int col)
        {
            return Board[row - 1, col];
        }
        private Tile GetTileOnTheBelow(int row, int col)
        {
            return Board[row + 1, col];
        }
        private Tile GetTileOnTheLeft(int row, int col)
        {
            return Board[row, col - 1];
        }
        private Tile GetTileOnTheRight(int row, int col)
        {
            return Board[row, col + 1];
        }

        private void JoinTiles(Tile currentTile, Tile secondTile)
        {
            Score += currentTile.Value * 2;
            secondTile.Value *= 2;
            currentTile.Value = 0;
            _positionChanged = true;
        }

        private void MoveTile(Tile currentTile, Tile secondTile)
        {
            secondTile.Value = currentTile.Value;
            currentTile.Value = 0;
            _positionChanged = true;
        }

        private bool IsGameOver()
        {
            for (int row = 0; row < _boardSize; row++)
            {
                for (int col = 0; col < _boardSize; col++)
                {
                    if (Board[row, col].Value == 0 || IsPossibleMove(row, col))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool IsPossibleMove(int row, int col)
        {
            var tile = Board[row, col];

            bool left = col != 0 && Board[row, col - 1].Value == tile.Value;
            bool right = col != _boardSize - 1 && Board[row, col + 1].Value == tile.Value;
            bool down = row != _boardSize - 1 && Board[row + 1, col].Value == tile.Value;
            bool up = row != 0 && Board[row - 1, col].Value == tile.Value;

            return left || right || down || up;
        }

        public void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
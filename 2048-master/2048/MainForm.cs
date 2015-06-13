using System;
using System.Linq;
using System.Windows.Forms;
using Game2048.Controls;
using Game2048.DAL;

namespace Game2048
{
    public partial class MainForm : Form
    {
        private Game _game;
        private GameService _gameService;

        public MainForm()
        {
            InitializeComponent();
            _game = new Game(4);
            _gameService = new GameService();

            BindElements();
            SetHighScore();
        }

        private void BindElements()
        {
            var buttons = flpContainer.Controls.Cast<TileButton>().ToList();

            int i = 0;
            foreach (var item in _game.Board)
            {
                var button = buttons[i];
                button.DataBindings.Add("BackgroundImage", item, "Image");

                i++;
            }
            lblScore.DataBindings.Add("Text", _game, "Score", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void OnFormKeyDown(object sender, KeyEventArgs e)
        {
            _game.Move(e.KeyCode);
        }

        private void ButtonScoresClick(object sender, EventArgs e)
        {
            var form = new HighScoresForm();
            form.ShowDialog();
        }

        private void ButtonNewGameClick(object sender, EventArgs e)
        {
            _game.StartNewGame();
            SetHighScore();
        }

        private void ButtonCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void SetHighScore()
        {
            var scores = _gameService.GetAllScores();
            lblBest.Text = scores.Any() ? scores.Max(n => n.Points).ToString() : "0";
        }
    }
}

using System;
using System.Windows.Forms;
using Game2048.DAL;
using Game2048.Model;

namespace Game2048
{
    public partial class GameOverForm : Form
    {
        private readonly GameService _gameService;
        private readonly int _points;

        public string UserName { get; set; }

        public GameOverForm(int points)
        {
            InitializeComponent();

            _points = points;
            _gameService = new GameService();

            txtUserName.DataBindings.Add(new Binding("Text", this, "UserName"));
            lblPoints.Text = string.Format(lblPoints.Text, _points);
        }

        private void ButtonSaveClick(object sender, EventArgs e)
        {
            var score = new Score(UserName, _points);
            _gameService.Add(score);
        }
    }
}

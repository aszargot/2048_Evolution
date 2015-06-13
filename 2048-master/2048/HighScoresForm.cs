using System.Linq;
using System.Windows.Forms;
using Game2048.DAL;

namespace Game2048
{
    public partial class HighScoresForm : Form
    {
        private GameService _gameService;

        public HighScoresForm()
        {
            InitializeComponent();

            _gameService = new GameService();
            var scores = _gameService.GetAllScores();

            dataGridView1.DataSource = scores.OrderByDescending(n => n.Points).ToList();
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}

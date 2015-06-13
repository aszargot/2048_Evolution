using System.ComponentModel;

namespace Game2048.Model
{
    public class Score
    {
        [Browsable(false)]
        public int Id { get; set; }
        public string Username { get; set; }
        public int Points { get; set; }

        public Score()
        {
        }

        public Score(string userName, int points)
        {
            Username = userName;
            Points = points;
        }
    }
}

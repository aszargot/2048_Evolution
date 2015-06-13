using System.Data.Entity;
using Game2048.Model;

namespace Game2048.DAL
{
    public class Context : DbContext
    {
        public Context() : base("2048ConnStr")
        {
        }

        public DbSet<Score> Scores { get; set; }
    }
}

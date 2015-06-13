using Game2048.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Game2048.DAL
{
    public class GameService
    {
        public void Add(Score score)
        {
            using (var ctx = new Context())
            {
                ctx.Scores.Add(score);
                ctx.SaveChanges();
            }
        }

        public void Update(Score score)
        {
            using (var ctx = new Context())
            {
                ctx.Entry(score).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void Delete(Score score)
        {
            using (var ctx = new Context())
            {
                ctx.Entry(score).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }

        public List<Score> GetAllScores()
        {
            using (var ctx = new Context())
            {
                return ctx.Scores.ToList();
            }
        }
    }
}

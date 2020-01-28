using SumApp.API.Models;
using SumApp.API.Repositories.Context;

namespace SumApp.API.Repositories
{
    public class TeamRepository : BaseRepository<Team>
    {
        public TeamRepository(SumAppContext db) : base(db) { }
    }
}

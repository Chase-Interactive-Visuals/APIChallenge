using APIChallenge.Models;
using APIChallenge.Interfaces;
using APIChallenge.Data;

namespace APIChallenge.Repository
{
    public class AircraftRepository : IAircraftRepository
    {
        private readonly ApplicationDBContext _context;

        public AircraftRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Aircraft>> GetAllAircraft()
        {
            throw new NotImplementedException();
        }

        public async Task<Aircraft> GetAircraftById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Add(Aircraft aircraft)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAircraft(Aircraft aircraft)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAircraft(Aircraft aircraft)
        {
            throw new NotImplementedException();
        }

        public bool SaveAircraft()
        {
            throw new NotImplementedException();
        }
    }
}

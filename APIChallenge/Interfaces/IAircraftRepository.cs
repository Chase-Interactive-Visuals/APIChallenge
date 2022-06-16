using APIChallenge.Models;

namespace APIChallenge.Interfaces
{
    public interface IAircraftRepository
    {
        Task<IEnumerable<Aircraft>> GetAllAircraft();
        Task<Aircraft> GetAircraftById(int id);
        bool Add(Aircraft aircraft);
        bool UpdateAircraft(Aircraft aircraft);
        bool DeleteAircraft(Aircraft aircraft);
        bool SaveAircraft();
    }
}

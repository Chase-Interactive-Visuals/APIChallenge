using APIChallenge.Models;

namespace APIChallenge.MockDB
{
    /// <summary>
    /// Mock Aircraft DB
    /// </summary>
    public class MockAircraftDB
    {
        Dictionary<int, Aircraft> aircraftDictionary = new Dictionary<int, Aircraft>
        {
            { 1, new Aircraft(1, 0.7, 550) },
            { 2, new Aircraft(2, 1.1, 200) }
        };
        /// <summary>
        /// Mock DB Request
        /// </summary>
        /// <param name="AircraftID">Key for Aircraft in Dictionary</param>
        /// <returns>Aircraft Object</returns>
        public Aircraft GetAircraftFromMockDB(int AircraftID)
        {
            return aircraftDictionary[AircraftID];
        }
    }
}

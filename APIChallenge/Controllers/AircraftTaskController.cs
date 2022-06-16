using APIChallenge.MockDB;
using APIChallenge.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftTaskController : ControllerBase
    {
        private  Aircraft? _aircraft;
        MockAircraftDB mockAircraftDB = new MockAircraftDB();
        DateTime today = new DateTime(2018, 06, 19);

        /// <summary>
        /// Post Method Used to create new Aircraft Object and Calculate Task Next Due Date
        /// </summary>
        /// <param name="aircraftId">The Aircraft ID for Database Query</param>
        /// <param name="tasks">JSON containing Tasks</param>
        /// <returns>Sorted List of AircraftTask; 200</returns>
        [HttpPost("/aircraft/{aircraftId}/duelist")]
        public async Task<ActionResult<List<AircraftTasks>>> PostAircraft(int aircraftId, AircraftTasks? tasks)
        {
            AircraftTasks? myTasks = new AircraftTasks();
            myTasks = tasks;
            
            await CreateAircraft(aircraftId, myTasks);
            await CalculateNextDueDate(_aircraft);

            return Ok(await SortListASC(myTasks.allTasks));
        }

        /// <summary>
        /// Sorts the List of AircraftTask ASC by NextDue then Description properties
        /// </summary>
        /// <param name="listToSort">ist of AircraftTask</param>
        /// <returns>Sorted List of AircratTask</returns>
        private async Task<List<AircraftTask>> SortListASC(List<AircraftTask> listToSort)
        {
            List<AircraftTask> orderedList = new List<AircraftTask> (listToSort
                .OrderByDescending(i => i.NextDue.HasValue)
                .ThenBy(i => i.NextDue)
                .ThenBy(i => i.Description));

            return orderedList;
        }
        
        /// <summary>
        /// Calculates DateTime NextDue property of AircraftTask
        /// </summary>
        /// <param name="aircraft">Aircraft Object Instance</param>
        /// <returns>DateTime NextDue</returns>
        private async Task CalculateNextDueDate(Aircraft aircraft)
        {
            DateTime? IntervalMonthsNextDueDate;
            double? DaysRemainingByHoursInterval;
            DateTime? IntervalHoursNextDueDate;
            DateTime? NextDueDate;

            foreach (AircraftTask t in _aircraft.aircraftTasks.allTasks)
            {
                if (t.IntervalMonths.HasValue && t.LogDate != null)
                {
                    IntervalMonthsNextDueDate = t.LogDate.AddMonths(t.IntervalMonths.Value);
                }
                else
                {
                    IntervalMonthsNextDueDate = null;
                }
                DaysRemainingByHoursInterval = ((t.LogHours + t.IntervalHours) - aircraft.CurrentHours) / aircraft.DailyHours;

                if (DaysRemainingByHoursInterval.HasValue)
                {
                    IntervalHoursNextDueDate = today.Date.AddDays(DaysRemainingByHoursInterval.Value);
                }
                else
                {
                    IntervalHoursNextDueDate = null;
                }

                
                if (IntervalMonthsNextDueDate.HasValue && IntervalHoursNextDueDate.HasValue)
                {
                    int value = DateTime.Compare(IntervalMonthsNextDueDate.Value, IntervalHoursNextDueDate.Value);
                    if (value > 0)
                    {
                        t.NextDue = IntervalHoursNextDueDate;
                    }
                    else if (value <= 0)
                    {
                        t.NextDue = IntervalMonthsNextDueDate;
                    }
                }
                else if (IntervalMonthsNextDueDate.HasValue && IntervalHoursNextDueDate == null)
                {
                    t.NextDue = IntervalMonthsNextDueDate.Value;
                }
                else if (IntervalMonthsNextDueDate == null && IntervalHoursNextDueDate.HasValue)
                {
                    t.NextDue = IntervalHoursNextDueDate.Value;
                }
                else
                {
                    t.NextDue = null;
                }
            }
            return;
        }

        /// <summary>
        /// Creates a new Aircraft
        /// </summary>
        /// <param name="aircraftID">Key for database query</param>
        /// <param name="aircraftTasks">ist of AircraftTask</param>
        /// <returns></returns>
        /// 

        /*
         * TODO: Move database communication to a service to remove database communication from Controller
         */
        private async Task<Aircraft> CreateAircraft(int aircraftID, AircraftTasks aircraftTasks)
        {
            _aircraft = mockAircraftDB.GetAircraftFromMockDB(aircraftID);
            _aircraft.aircraftTasks = aircraftTasks;
            return _aircraft;
        }
    }
}

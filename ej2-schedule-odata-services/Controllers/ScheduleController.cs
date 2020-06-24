using System;
using System.Linq;
using ej2_schedule_odata_services.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ej2_schedule_odata_services.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class ScheduleController : ODataController
    {
        private ScheduleContext _context;

        public ScheduleController(ScheduleContext context)
        {
            _context = context;

            if (_context.EventDatas.Count() == 0)
            {
                DateTime YearStart = new DateTime(2020, 1, 1);
                DateTime YearEnd = new DateTime(2020, 12, 31);
                string[] EventSubjects = new string[] {
                "Bering Sea Gold", "Technology", "Maintenance", "Meeting", "Travelling", "Annual Conference", "Birthday Celebration", "Farewell Celebration",
                "Wedding Aniversary", "Alaska: The Last Frontier", "Deadest Catch", "Sports Day", "MoonShiners", "Close Encounters", "HighWay Thru Hell",
                "Daily Planet", "Cash Cab", "Basketball Practice", "Rugby Match", "Guitar Class", "Music Lessons", "Doctor checkup", "Brazil - Mexico",
                "Opening ceremony", "Final presentation"
            };
                for (int a = 0, id = 1; a < 1000; a++)
                {
                    Random random = new Random();
                    int Month = random.Next(1, 12);
                    int Date = random.Next(1, 28);
                    int Hour = random.Next(1, 24);
                    int Minutes = random.Next(1, 60);
                    DateTime start = new DateTime(YearStart.Year, Month, Date, Hour, Minutes, 0);
                    DateTime end = new DateTime(start.Ticks);
                    end = end.AddHours(2);
                    ScheduleData eventDatas = new ScheduleData()
                    {
                        Id = id,
                        Subject = EventSubjects[random.Next(1, 25)],
                        StartTime = new DateTime(start.Ticks),
                        EndTime = new DateTime(end.Ticks),
                        IsAllDay = (a % 10 == 0) ? true : false,
                        IsBlock = false,
                        IsReadonly = (a % 15 == 0) ? true : false,
                        RoomId = (a % 3) + 1,
                        OwnerId = (a % 6) + 1
                    };
                    _context.EventDatas.Add(eventDatas);
                    id++;
                }
                _context.SaveChanges();
            }
        }

        // GET: odata/<ScheduleController>
        [EnableQuery]
        [AcceptVerbs("GET")]
        public IQueryable<ScheduleData> GetEvents()
        {
            return _context.EventDatas.AsQueryable();
        }

        // POST odata/<ScheduleController>
        [AcceptVerbs("POST", "OPTIONS")]
        public void AddEvent([FromBody] ScheduleData eventData)
        {
            try
            {
                ScheduleData insertData = new ScheduleData();
                insertData.Id = (_context.EventDatas.ToList().Count > 0 ? _context.EventDatas.ToList().Max(p => p.Id) : 1) + 1;
                insertData.Subject = eventData.Subject;
                insertData.StartTime = Convert.ToDateTime(eventData.StartTime);
                insertData.EndTime = Convert.ToDateTime(eventData.EndTime);
                insertData.StartTimezone = eventData.StartTimezone;
                insertData.EndTimezone = eventData.EndTimezone;
                insertData.Location = eventData.Location;
                insertData.Description = eventData.Description;
                insertData.IsAllDay = eventData.IsAllDay;
                insertData.IsBlock = eventData.IsBlock;
                insertData.IsReadonly = eventData.IsReadonly;
                insertData.FollowingID = eventData.FollowingID;
                insertData.RecurrenceID = eventData.RecurrenceID;
                insertData.RecurrenceRule = eventData.RecurrenceRule;
                insertData.RecurrenceException = eventData.RecurrenceException;
                insertData.RoomId = eventData.RoomId;
                insertData.OwnerId = eventData.OwnerId;
                _context.EventDatas.Add(insertData);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }

        // PUT odata/<ScheduleController>/5
        [AcceptVerbs("PUT", "PATCH", "MERGE", "OPTIONS")]
        public void UpdateEvent([FromBody] ScheduleData eventData)
        {
            try
            {
                ScheduleData updateData = _context.EventDatas.Find(eventData.Id);
                if (updateData != null)
                {
                    updateData.Subject = eventData.Subject;
                    updateData.StartTime = Convert.ToDateTime(eventData.StartTime);
                    updateData.EndTime = Convert.ToDateTime(eventData.EndTime);
                    updateData.StartTimezone = eventData.StartTimezone;
                    updateData.EndTimezone = eventData.EndTimezone;
                    updateData.Location = eventData.Location;
                    updateData.Description = eventData.Description;
                    updateData.IsAllDay = eventData.IsAllDay;
                    updateData.IsBlock = eventData.IsBlock;
                    updateData.IsReadonly = eventData.IsReadonly;
                    updateData.FollowingID = eventData.FollowingID;
                    updateData.RecurrenceID = eventData.RecurrenceID;
                    updateData.RecurrenceRule = eventData.RecurrenceRule;
                    updateData.RecurrenceException = eventData.RecurrenceException;
                    updateData.RoomId = eventData.RoomId;
                    updateData.OwnerId = eventData.OwnerId;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }

        // DELETE odata/<ScheduleController>/5
        [AcceptVerbs("DELETE", "OPTIONS")]
        public void DeleteEvent([FromODataUri] int key)
        {
            try
            {
                ScheduleData removeData = _context.EventDatas.Find(key);
                if (removeData != null)
                {
                    _context.EventDatas.Remove(removeData);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }
    }
}

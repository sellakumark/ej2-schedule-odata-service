using Microsoft.EntityFrameworkCore;
using System;

namespace ej2_schedule_odata_services.Models
{
    public class ScheduleContext : DbContext
    {
        public ScheduleContext(DbContextOptions<ScheduleContext> options)
          : base(options)
        { }

        public DbSet<ScheduleData> EventDatas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScheduleData>().ToTable("ScheduleData");
        }
    }

    public partial class ScheduleData
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string StartTimezone { get; set; }
        public string EndTimezone { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public bool IsAllDay { get; set; }
        public bool IsBlock { get; set; }
        public bool IsReadonly { get; set; }
        public int? FollowingID { get; set; }
        public int? RecurrenceID { get; set; }
        public string RecurrenceRule { get; set; }
        public string RecurrenceException { get; set; }
        public int? RoomId { get; set; }
        public int? OwnerId { get; set; }

    }

}

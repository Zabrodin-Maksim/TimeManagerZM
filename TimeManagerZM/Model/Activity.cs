using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagerZM.Model
{
    public class Activity
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public int ActivityTypeId { get; set; }
        public ActivityType ActivityType { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}

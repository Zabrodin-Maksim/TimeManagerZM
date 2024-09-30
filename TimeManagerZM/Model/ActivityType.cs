using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagerZM.Model
{
    public class ActivityType
    {
        public int Id { get; set; }
        public string TypeName {  get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<Activity> Activities { get; set; }
    }
}

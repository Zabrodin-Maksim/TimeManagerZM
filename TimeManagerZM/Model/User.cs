using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagerZM.Model
{
    public class User
    {
        public int Id { get; set; } 
        public string UserName { get; set; }
        public string Password { get; set; }

        public List<ActivityType> ActivityTypes { get; set; }

        public List<Activity> Activities { get; set; }
    }
}
